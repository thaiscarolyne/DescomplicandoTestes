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


        /*********************************Variáveis*********************************/

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



        private Disciplina _DisciplinaSelecionada;
        public Disciplina DisciplinaSelecionada
        {
            get { return _DisciplinaSelecionada; }
            set
            {
                _DisciplinaSelecionada = value;
                OnPropertyChanged("DisciplinaSelecionada");
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
            }
        }
        


        /*********************************Construtor*********************************/

        public DisciplinasViewModel()
        {
            Teste = new Command(TesteAction);

            PesquisarDisciplinas = new Command(PesquisarDisciplinasAction);


            ListaDisciplinas = new List<Disciplina>();

            ListaDisciplinas.Add(new Disciplina("Banco de dados", "BD"));
            ListaDisciplinas.Add(new Disciplina("Matemática", "MAT"));
            ListaDisciplinas.Add(new Disciplina("Engenharia de software", "ENGSOFT"));
            ListaDisciplinas.Add(new Disciplina("Processamento digital de imagens e teste teste", "PDI"));
            ListaDisciplinas.Add(new Disciplina("Cálculo Numérico", "CALCNUM"));
            ListaDisciplinas.Add(new Disciplina("Cálculo Numérico", "CALCNUM"));
            ListaDisciplinas.Add(new Disciplina("Cálculo Numérico", "CALCNUM"));
            ListaDisciplinas.Add(new Disciplina("Cálculo Numérico", "CALCNUM"));
            ListaDisciplinas.Add(new Disciplina("Cálculo Numérico", "CALCNUM"));
            ListaDisciplinas.Add(new Disciplina("Cálculo Numérico", "CALCNUM"));


            ListaConteudos = new List<Conteudo>();

            ListaConteudos.Add(new Conteudo("Trigger"));
            ListaConteudos.Add(new Conteudo("Álgebra Relacional"));
            ListaConteudos.Add(new Conteudo("Normalização"));
            ListaConteudos.Add(new Conteudo("Projeto de banco de dados"));
            ListaConteudos.Add(new Conteudo("Views"));
            ListaConteudos.Add(new Conteudo("Modelo físico"));
            ListaConteudos.Add(new Conteudo("Modelo lógico"));
        }


        /*********************************Métodos*********************************/

        private void TesteAction()
        {
            App.Current.MainPage.DisplayAlert("Clique", "Clicou", "OK");
        }

        private void PesquisarDisciplinasAction()
        {
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
