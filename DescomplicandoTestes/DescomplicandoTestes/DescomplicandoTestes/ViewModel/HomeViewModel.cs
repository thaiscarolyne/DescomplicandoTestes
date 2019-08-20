using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using DescomplicandoTestes.View;

namespace DescomplicandoTestes.ViewModel
{
    public class HomeViewModel
    {
        public Command Logout { get; set; }

        public Command Teste { get; set; }

        public HomeViewModel()
        {
            Logout = new Command(LogoutAction);

            Teste = new Command(TesteAction);
        }

        private async void LogoutAction()
        {
            var confirma = await App.Current.MainPage.DisplayAlert("Confirmação", "Deseja realmente sair do sistema?", "SIM", "NÃO");

            if (confirma)
            {
                App.Current.MainPage = new NavigationPage(new View.Login()) { BarBackgroundColor = Color.HotPink };
            }
        }

        private void TesteAction()
        {
            App.Current.MainPage.DisplayAlert("Clique", "Clicou", "OK");
        }
    }
}
