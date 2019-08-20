using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using DescomplicandoTestes.View;

namespace DescomplicandoTestes.ViewModel
{
    public class LoginCadastrarViewModel
    {
        public Command MudarPaginaHome { get; set; }

        public Command MudarPaginaCadastrar { get; set; }


        public LoginCadastrarViewModel()
        {
            MudarPaginaHome = new Command(MudarPaginaHomeAction);

            MudarPaginaCadastrar = new Command(MudarPaginaCadastrarAction);
        }

        private void MudarPaginaHomeAction()
        {
            App.Current.MainPage = new NavigationPage(new Home()) {BarBackgroundColor = Color.HotPink};
        }

        private void MudarPaginaCadastrarAction()
        {
            App.Current.MainPage.Navigation.PushAsync(new Cadastrar());
        }

    }
}
