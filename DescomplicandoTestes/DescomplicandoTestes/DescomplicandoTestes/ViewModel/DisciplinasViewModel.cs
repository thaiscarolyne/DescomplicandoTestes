using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using DescomplicandoTestes.View;
using DescomplicandoTestes.Model;
using System.Globalization;
using DescomplicandoTestes.ViewModel;

namespace DescomplicandoTestes.ViewModel
{
    public class DisciplinasViewModel : INotifyPropertyChanged
    {
        /*********************************Comandos*********************************/

        public Command PesquisarDisciplinas { get; set; }

        public Command CadastrarDisciplina { get; set; }

        public Command IrParaCadastroDisciplina { get; set; }

        public Command FecharModalDiscCadSucesso { get; set; }


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

        private List<Alternativa> _ListaAlternativas;
        public List<Alternativa> ListaAlternativas
        {
            get { return _ListaAlternativas; }
            set
            {
                _ListaAlternativas = value;
                OnPropertyChanged("ListaAlternativas");
            }
        }


        /*********************************Variáveis para exibição*********************************/

        public LoginCadastrarViewModel LoginVM
        {
            get { return ViewModel.ViewModelLocator.LoginCadastrarVM; }
        }       


        private Disciplina _DisciplinaSelecionada;
        public Disciplina DisciplinaSelecionada
        {
            get { return _DisciplinaSelecionada; }
            set
            {
                _DisciplinaSelecionada = value;
                OnPropertyChanged("DisciplinaSelecionada");

                /****************Consulta ao BD****************/
                ListaConteudos = Conteudo.BuscarConteudos(LoginVM.professor, DisciplinaSelecionada);
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
                ListaQuestoes = Questao.BuscarQuestoes(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado);
            }
        }

        private Questao _QuestaoSelecionada;
        public Questao QuestaoSelecionada
        {
            get { return _QuestaoSelecionada; }
            set
            {
                _QuestaoSelecionada = value;
                OnPropertyChanged("QuestaoSelecionada");

                /****************Consulta ao BD****************/
                ListaAlternativas = null;               
                ListaAlternativas = Alternativa.BuscarAlternativas(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado, QuestaoSelecionada);
                
                foreach (var item in ListaAlternativas)
                {
                    if (item.Letra == QuestaoSelecionada.Resposta)
                    {
                        item.Cor = Color.FromHex("#72FB9B");
                    }
                    else
                    {
                        item.Cor = Color.White;
                    }
                }
            }
        }

        private Alternativa _AlternativaSelecionada;
        public Alternativa AlternativaSelecionada
        {
            get { return _AlternativaSelecionada; }
            set
            {
                _AlternativaSelecionada = value;
                OnPropertyChanged("AlternativaSelecionada");
            }
        }


        /*********************************Variáveis para cadastro*********************************/

        private Disciplina _DisciplinaACadastrar = new Disciplina(null, null);
        public Disciplina DisciplinaACadastrar
        {
            get { return _DisciplinaACadastrar; }
            set
            {
                _DisciplinaACadastrar = value;
                OnPropertyChanged("DisciplinaACadastrar");
            }
        }



        /*********************************Construtor*********************************/

        public DisciplinasViewModel()
        {
            IrParaCadastroDisciplina = new Command(IrParaCadastroDisciplinaAction);
            PesquisarDisciplinas = new Command(PesquisarDisciplinasAction);
            CadastrarDisciplina = new Command(CadastrarDisciplinaAction);
            FecharModalDiscCadSucesso = new Command(FecharModalDiscCadSucessoAction);            
        }


        /*********************************Métodos*********************************/

        private void IrParaCadastroDisciplinaAction()
        {
            App.Current.MainPage.Navigation.PushAsync(new CadastrarDisciplina());
        }

        private void PesquisarDisciplinasAction()
        {
            /****************Consulta ao BD****************/
            ListaDisciplinas = Disciplina.BuscarDisciplinas(LoginVM.professor);

            App.Current.MainPage.Navigation.PushAsync(new PesquisarDisciplinas());
        }

        private void CadastrarDisciplinaAction()
        {
            Disciplina.CadastrarDisciplina(LoginVM.professor, DisciplinaACadastrar);

            App.Current.MainPage.Navigation.PushModalAsync(new ModalDisciplinaCadastradaSucesso());            
        }

        private async void FecharModalDiscCadSucessoAction()
        {
            await App.Current.MainPage.Navigation.PopAsync();

            await App.Current.MainPage.Navigation.PopModalAsync();

            DisciplinaACadastrar = new Disciplina(null, null);
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
