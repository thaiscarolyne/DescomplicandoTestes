using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using DescomplicandoTestes.View;

namespace DescomplicandoTestes.ViewModel
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {

        }

        private async void Logout(object sender, EventArgs args)
        {
            var confirma = await App.Current.MainPage.DisplayAlert("Confirmação", "Deseja realmente sair do sistema?", "SIM", "NÃO");

            if (confirma)
            {
                App.Current.MainPage = new NavigationPage(new View.Login()) { BarBackgroundColor = Color.HotPink };
            }
        }

        private void Teste(object sender, EventArgs args)
        {
            App.Current.MainPage.DisplayAlert("Clique", "Clicou", "OK");
        }
    }
}
