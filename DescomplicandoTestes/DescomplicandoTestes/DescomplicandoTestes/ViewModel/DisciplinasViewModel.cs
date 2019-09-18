using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using DescomplicandoTestes.View;
using DescomplicandoTestes.Model;

namespace DescomplicandoTestes.ViewModel
{
    public class DisciplinasViewModel : INotifyPropertyChanged
    {
        /*********************************Comandos*********************************/

        public Command PesquisarDisciplinas { get; set; }

        public Command Teste { get; set; }


        /**********************************Listas**********************************/

        private List<Disciplina> _ListaDisciplinas;
        public List<Disciplina> ListaDisciplinas
        {
            get { return _ListaDisciplinas; }
            set
            {
                _ListaDisciplinas = value;
                OnPropertyChanged("ListaDisciplinas");
            }
        }

        private List<Conteudo> _ListaConteudos;
        public List<Conteudo> ListaConteudos
        {
            get { return _ListaConteudos; }
            set
            {
                _ListaConteudos = value;
                OnPropertyChanged("ListaConteudos");
            }
        }

        private List<Questao> _ListaQuestoes;
        public List<Questao> ListaQuestoes
        {
            get { return _ListaQuestoes; }
            set
            {
                _ListaQuestoes = value;
                OnPropertyChanged("ListaQuestoes");
            }
        }


        /*********************************Variáveis*********************************/

        private Disciplina _DisciplinaSelecionada;
        public Disciplina DisciplinaSelecionada
        {
            get
            {                
                return _DisciplinaSelecionada;                
            }
            set
            {
                _DisciplinaSelecionada = value;
                OnPropertyChanged("DisciplinaSelecionada");

                /****************Consulta ao BD****************/
                ListaConteudos = Conteudo.BuscarConteudos(LoginCadastrarViewModel.professor, DisciplinaSelecionada);
            }
        }

        private Conteudo _ConteudoSelecionado;
        public Conteudo ConteudoSelecionado
        {
            get { return _ConteudoSelecionado; }
            set
            {
                _ConteudoSelecionado = value;
                OnPropertyChanged("ConteudoSelecionado");

                /****************Consulta ao BD****************/
                ListaQuestoes = Questao.BuscarQuestoes(LoginCadastrarViewModel.professor, DisciplinaSelecionada, ConteudoSelecionado);
            }
        }       


        /*********************************Construtor*********************************/

        public DisciplinasViewModel()
        {            
            Teste = new Command(TesteAction);
            PesquisarDisciplinas = new Command(PesquisarDisciplinasAction);            
        }


        /*********************************Métodos*********************************/

            private void TesteAction()
        {
            App.Current.MainPage.DisplayAlert("Clique", "Clicou", "OK");
        }

        private void PesquisarDisciplinasAction()
        {
            /****************Consulta ao BD****************/
            ListaDisciplinas = Disciplina.BuscarDisciplinas(LoginCadastrarViewModel.professor);

            App.Current.MainPage.Navigation.PushAsync(new PesquisarDisciplinas());
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
