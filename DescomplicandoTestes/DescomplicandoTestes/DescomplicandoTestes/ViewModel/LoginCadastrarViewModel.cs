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

        /*********************************Comandos*********************************/

        public Command MudarPaginaCadastrar { get; set; }

        public Command MudarPaginaHome { get; set; }

        public Command CadastrarProfessor { get; set; }


        /*****************************Variáveis para consulta e cadastro*****************************/

        private Professor _professor = new Professor();
        public Professor professor
        {
            get { return _professor; }
            set {
                _professor = value;
                OnPropertyChanged("professor");
            }
        }
        


        /*********************************Construtor*********************************/
               
        public LoginCadastrarViewModel()
        {
            MudarPaginaHome = new Command(MudarPaginaHomeAction);

            MudarPaginaCadastrar = new Command(MudarPaginaCadastrarAction);

            CadastrarProfessor = new Command(CadastrarProfessorAction);
        }


        /*********************************Métodos*********************************/

        private void MudarPaginaHomeAction()
        {
            if (professor.VerificarProfessorCadastrado(professor))
            {
                App.Current.MainPage = new NavigationPage(new Home()) { BarBackgroundColor = Color.HotPink };
            }
            else
            {
                App.Current.MainPage.DisplayAlert("ERRO", "Login e/ou senha incorretos!", "OK");
            }
                                  
        }

        private async void CadastrarProfessorAction()
        {
            if(professor.CPF_Professor == null || professor.Nome == null || professor.Senha == null)
            {
                await App.Current.MainPage.DisplayAlert("ERRO", "Por favor preencha todos os campos!", "OK");
            }
            else
            {
                if(professor.CPF_Professor.Length < 11)
                {
                    await App.Current.MainPage.DisplayAlert("ERRO", "O CPF digitado não possui 11 dígitos", "OK");
                }
                else
                {
                    string retorno = professor.CadastrarProfessor(professor);                    

                    if (retorno == "Cadastro realizado com sucesso!")
                    {
                        await App.Current.MainPage.DisplayAlert("SUCESSO", retorno, "OK");

                        App.Current.MainPage = new NavigationPage(new Home()) { BarBackgroundColor = Color.HotPink };
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("ERRO", retorno, "OK");

                        professor = new Professor();
                    }
                }
                
            }

            
        }

        private void MudarPaginaCadastrarAction()
        {
            App.Current.MainPage.Navigation.PushAsync(new Cadastrar());
        }


        /**************************Notificar modificações**************************/

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
