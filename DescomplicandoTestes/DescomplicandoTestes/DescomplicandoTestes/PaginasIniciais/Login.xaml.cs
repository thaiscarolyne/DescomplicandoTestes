using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DescomplicandoTestes.PaginasIniciais
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();
		}

        private void MudarPaginaCadastrar(object sender, EventArgs args)
        {
            Navigation.PushAsync(new Cadastrar());
        }

        private void MudarPaginaHome(object sender, EventArgs args)
        {
            Navigation.PushAsync(new Home());
        }
    }
}