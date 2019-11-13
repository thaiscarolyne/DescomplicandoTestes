using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DescomplicandoTestes.PDF;
using System.IO;
using System.Threading.Tasks;
using DescomplicandoTestes.Droid;
using Android.Support.V4.Content;
using Android;
using Android.Content.PM;
using Android.Support.V4.App;
using Java.IO;

[assembly: Dependency(typeof(SaveAndroid))]

namespace DescomplicandoTestes.Droid
{
    class SaveAndroid : ISave
    {
        public async Task SaveAndView(string filename, string contentType, MemoryStream stream)
        {
            string root = null;

            if (ContextCompat.CheckSelfPermission(Forms.Context, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions((Android.App.Activity)Forms.Context, new String[] { Manifest.Permission.WriteExternalStorage }, 1);
            }

            //Get the root path in android device.
            if (Android.OS.Environment.IsExternalStorageEmulated)
            {
                root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            }
            else
                root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            
            //Create directory and file 
            Java.IO.File myDir = new Java.IO.File(root + "/DescomplicandoTestes");
            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, filename);

            //Remove if the file exists
            if (file.Exists()) file.Delete();

            //Write the stream into the file
            FileOutputStream outs = new FileOutputStream(file);
            outs.Write(stream.ToArray());

            outs.Flush();
            outs.Close();

            //Invoke the created file for viewing
            if (file.Exists())
            {
                string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                Android.Net.Uri path = FileProvider.GetUriForFile(Forms.Context, Android.App.Application.Context.PackageName + ".provider", file);
                intent.SetDataAndType(path, mimeType);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose App"));
            }

        }
    }
}