﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DescomplicandoTestes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new View.AdicionarQuestao()) { BarBackgroundColor = Color.HotPink };
            MainPage = new NavigationPage(new View.Login()) { BarBackgroundColor =Color.HotPink};
            //MainPage = new NavigationPage(new View.Home()) { BarBackgroundColor = Color.HotPink };


#if DEBUG

            HotReloader.Current.Run(this, new HotReloader.Configuration {
                DeviceUrlPort = 8001
            });            
#endif
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
