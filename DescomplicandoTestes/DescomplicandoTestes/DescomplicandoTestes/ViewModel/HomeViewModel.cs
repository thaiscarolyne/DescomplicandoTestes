﻿using System;
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

        public Command MudarPaginaDisciplinas { get; set; }

        public Command MudarPaginaProvas { get; set; }

        LoginCadastrarViewModel LoginVM = ViewModel.ViewModelLocator.LoginCadastrarVM;

        public DisciplinasViewModel DisciplinasVM
        {
            get { return ViewModel.ViewModelLocator.DisciplinasVM; }
        }

        public HomeViewModel()
        {
            Logout = new Command(LogoutAction);

            Teste = new Command(TesteAction);

            MudarPaginaDisciplinas = new Command(MudarPaginaDisciplinasAction);

            MudarPaginaProvas = new Command(MudarPaginaProvasAction);
        }

        private async void LogoutAction()
        {
            var confirma = await App.Current.MainPage.DisplayAlert("Confirmação", "Deseja realmente sair do sistema?", "SIM", "NÃO");

            if (confirma)
            {
                LoginVM.professor = new Model.Professor();

                DisciplinasVM.ResetarVariaveisDisciplina();
                DisciplinasVM.ResetarVariaveisConteudo();
                DisciplinasVM.ResetarVariaveisQuestao();

                App.Current.MainPage = new NavigationPage(new View.Login()) { BarBackgroundColor = Color.HotPink };
            }
        }

        private void TesteAction()
        {          

            App.Current.MainPage.DisplayAlert("Clique", "Clicou", "OK");
        }


        private void MudarPaginaDisciplinasAction()
        {
            App.Current.MainPage.Navigation.PushAsync(new Disciplinas());
        }

        private void MudarPaginaProvasAction()
        {
            App.Current.MainPage.Navigation.PushAsync(new Provas());
        }
    }
}
