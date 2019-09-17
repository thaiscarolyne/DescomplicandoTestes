using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using DescomplicandoTestes.View;
using DescomplicandoTestes.Model;
using System.ComponentModel;

namespace DescomplicandoTestes.ViewModel
{
    public class LoginCadastrarViewModel
    {   
        public Command MudarPaginaCadastrar { get; set; }

        public Command MudarPaginaHome { get; set; }

        public static Professor professor;

        private string _CPF;
        public string CPF
        {
            get { return _CPF; }
            set { _CPF = value; OnPropertyChanged("CPF"); }
        }
        
        public string _Senha;
        public string Senha
        {
            get { return _Senha; }
            set { _Senha = value; OnPropertyChanged("Senha"); }
        }


        public LoginCadastrarViewModel()
        {
            MudarPaginaHome = new Command(MudarPaginaHomeAction);

            MudarPaginaCadastrar = new Command(MudarPaginaCadastrarAction);

            professor = new Professor();
        }

        private void MudarPaginaHomeAction()
        {
            professor.CPF_Professor = CPF;

            professor.Senha = Senha;

            if (professor.VerificarProfessorCadastrado(professor))
            {
                App.Current.MainPage = new NavigationPage(new Home()) { BarBackgroundColor = Color.HotPink };
            }
            else
            {
                App.Current.MainPage.DisplayAlert("ERRO", "Login e/ou senha incorretos!", "OK");
            }

            
        }

        private void MudarPaginaCadastrarAction()
        {
            App.Current.MainPage.Navigation.PushAsync(new Cadastrar());
        }




        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string NameProperty)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(NameProperty));
            }
        }

    }
}
