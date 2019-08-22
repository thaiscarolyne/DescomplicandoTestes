using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using DescomplicandoTestes.View;

namespace DescomplicandoTestes.ViewModel
{
    public class DisciplinasViewModel
    {
        public Command PesquisarDisciplinas { get; set; }

        public Command Teste { get; set; }

        public DisciplinasViewModel()
        {
            Teste = new Command(TesteAction);

            PesquisarDisciplinas = new Command(PesquisarDisciplinasAction);
        }


        private void TesteAction()
        {
            App.Current.MainPage.DisplayAlert("Clique", "Clicou", "OK");
        }

        private void PesquisarDisciplinasAction()
        {
            App.Current.MainPage.Navigation.PushAsync(new PesquisarDisciplinas());
        }
    }
}
