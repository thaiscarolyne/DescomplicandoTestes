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

        public Command PesquisarDisciplinas { get; set; } //Comando para selecionar as disciplinas do professor no banco de dados

        public Command CadastrarDisciplina { get; set; } //Comando para cadastrar disciplina no banco de dados

        public Command CadastrarNovaDisciplina { get; set; } //Comando para voltar para a tela de cadastrar disciplina        

        public Command IrParaCadastroDisciplina { get; set; } //Comando para ir para tela de cadastro de disciplina

        public Command FecharModalDiscCadSucesso { get; set; }  //Comando para fechar o modal que confirma o cadastro da disciplina

        public Command FecharModalContCadSucesso { get; set; }  //Comando para fechar o modal que confirma o cadastro do conteúdo

        public Command ExcluirDisciplina { get; set; } //Comando para excluir uma disciplina do banco de dados

        public Command IrParaAdicionarConteudoComModal { get; set; } //Comando para ir para tela de adicionar conteúdo a partir do modal de confirmação de cadastro da disciplina

        public Command IrParaAdicionarConteudoSemModal { get; set; } //Comando para ir para tela de adicionar conteúdo a partir da tela de VisualizarDisciplina

        public Command AdicionarConteudo { get; set; } //Comando para adicionar um novo conteúdo no banco de dados

        public Command AdicionarNovoConteudo { get; set; } //Comando para voltar para a tela de adicionar conteúdo

        public Command IrParaAdicionarQuestaoComModal { get; set; } //Comando para ir para tela de adicionar questão a partir do modal de confirmação de cadastro do conteúdo

        public Command IrParaAdicionarQuestaoSemModal { get; set; } //Comando para ir para tela de adicionar questão a partir da tela de VisualizarConteudo

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


        private Disciplina _DisciplinaSelecionada = new Disciplina(null, null);
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

        private Conteudo _ConteudoSelecionado = new Conteudo(null);
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

        private Questao _QuestaoSelecionada = new Questao(null, null, null, null, Char.MinValue);
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


        private Conteudo _ConteudoACadastrar = new Conteudo(null);
        public Conteudo ConteudoACadastrar
        {
            get { return _ConteudoACadastrar; }
            set
            {
                _ConteudoACadastrar = value;
                OnPropertyChanged("ConteudoACadastrar");
            }
        }


        private Questao _QuestaoACadastrar = new Questao(null,null,null,null,Char.MinValue);
        public Questao QuestaoACadastrar
        {
            get { return _QuestaoACadastrar; }
            set
            {
                _QuestaoACadastrar = value;
                OnPropertyChanged("QuestaoACadastrar");
            }
        }



        /*********************************Construtor*********************************/

        public DisciplinasViewModel()
        {
            IrParaCadastroDisciplina = new Command(IrParaCadastroDisciplinaAction);
            PesquisarDisciplinas = new Command(PesquisarDisciplinasAction);
            CadastrarDisciplina = new Command(CadastrarDisciplinaAction);
            CadastrarNovaDisciplina = new Command(CadastrarNovaDisciplinaAction);
            FecharModalDiscCadSucesso = new Command(FecharModalDiscCadSucessoAction);
            FecharModalContCadSucesso = new Command(FecharModalContCadSucessoAction);
            ExcluirDisciplina = new Command(ExcluirDisciplinaAction);
            IrParaAdicionarConteudoComModal = new Command(IrParaAdicionarConteudoComModalAction);
            IrParaAdicionarConteudoSemModal = new Command(IrParaAdicionarConteudoSemModalAction);
            AdicionarConteudo = new Command(AdicionarConteudoAction);
            AdicionarNovoConteudo = new Command(AdicionarNovoConteudoAction);
            IrParaAdicionarQuestaoComModal = new Command(IrParaAdicionarQuestaoComModalAction);
            IrParaAdicionarQuestaoSemModal = new Command(IrParaAdicionarQuestaoSemModalAction);
        }



        /***********************************************************MÉTODOS***********************************************************/

            private void ResetarVariaveisDisciplina()
            {
                /*DISCIPLINA*/            

                DisciplinaSelecionada.Nome_Disciplina = null;
                DisciplinaSelecionada.Sigla = null;

                DisciplinaACadastrar.Nome_Disciplina = null;
                DisciplinaACadastrar.Sigla = null;   
            }

            private void ResetarVariaveisConteudo()
            {
                /*CONTEÚDO*/

                ConteudoSelecionado.Nome_Conteudo = null;

                ConteudoACadastrar.Nome_Conteudo = null;
            }

            private void ResetarVariaveisQuestao()
            {
                /*QUESTÃO*/

                QuestaoSelecionada.Nome_Questao = null;
                QuestaoSelecionada.Resposta = Char.MinValue;
                QuestaoSelecionada.Imagem = null;
                QuestaoSelecionada.Dificuldade = null;
                QuestaoSelecionada.Enunciado = null;

                QuestaoACadastrar.Nome_Questao = null;
                QuestaoACadastrar.Resposta = Char.MinValue;
                QuestaoACadastrar.Imagem = null;
                QuestaoACadastrar.Dificuldade = null;
                QuestaoACadastrar.Enunciado = null;
            }


        /*****************************DISCIPLINAS*****************************/

            private void IrParaCadastroDisciplinaAction()
            {
                ResetarVariaveisDisciplina();

                ResetarVariaveisConteudo();

                ResetarVariaveisQuestao();

                App.Current.MainPage.Navigation.PushAsync(new CadastrarDisciplina());
            }

            private void PesquisarDisciplinasAction()
            {
                ResetarVariaveisDisciplina();

                ResetarVariaveisConteudo();

                ResetarVariaveisQuestao();

                /****************Consulta ao BD****************/
                ListaDisciplinas = Disciplina.BuscarDisciplinas(LoginVM.professor);

                App.Current.MainPage.Navigation.PushAsync(new PesquisarDisciplinas());
            }

            private async void CadastrarDisciplinaAction()
            {
                if (DisciplinaACadastrar.Nome_Disciplina == null)
                {
                    await App.Current.MainPage.DisplayAlert("ERRO", "Por favor preencha o nome da disciplina!", "OK");
                }
                else
                {
                    string retorno = Disciplina.CadastrarDisciplina(LoginVM.professor, DisciplinaACadastrar);    

                    if (retorno == "Cadastro realizado com sucesso!")
                    {
                        await App.Current.MainPage.Navigation.PushModalAsync(new ModalDisciplinaCadastradaSucesso());
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("ERRO", retorno, "OK");

                        DisciplinaACadastrar = new Disciplina(null, null);
                    }
                }                    
            }

            private async void CadastrarNovaDisciplinaAction()
            {
                DisciplinaACadastrar = new Disciplina(null, null);

                await App.Current.MainPage.Navigation.PopModalAsync();
            }


            private async void FecharModalDiscCadSucessoAction()
            {
                await App.Current.MainPage.Navigation.PopAsync();

                await App.Current.MainPage.Navigation.PopModalAsync();

                DisciplinaACadastrar = new Disciplina(null, null);
            }


            private async void ExcluirDisciplinaAction(object obj)
            {
                var confirma = await App.Current.MainPage.DisplayAlert("Confirmação", "Deseja realmente excluir essa disciplina?", "SIM", "NÃO");


                if (confirma)
                {
                    Disciplina disc = obj as Disciplina;

                    string retorno = Disciplina.ExcluirDisciplina(LoginVM.professor, disc);

                    if (retorno == "Disciplina excluída com sucesso!")
                    {
                        App.Current.MainPage.DisplayAlert("SUCESSO", retorno, "OK");


                        /****************Consulta ao BD****************/
                        ListaDisciplinas = Disciplina.BuscarDisciplinas(LoginVM.professor);
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("ERRO", retorno, "OK");
                    }
                }

            }


        /*****************************CONTEÚDOS*****************************/


            private async void AdicionarNovoConteudoAction()
            {
                ResetarVariaveisConteudo();

                await App.Current.MainPage.Navigation.PopModalAsync();
            }


            private async void AdicionarConteudoAction()
            {
                if (ConteudoACadastrar.Nome_Conteudo == null)
                {
                    await App.Current.MainPage.DisplayAlert("ERRO", "Por favor preencha o nome do conteúdo!", "OK");
                }
                else
                {
                    string retorno;

                    if (DisciplinaSelecionada.Nome_Disciplina == null) //Então, estamos na parte de cadastro
                    {
                        retorno = Conteudo.AdicionarConteudo(LoginVM.professor, DisciplinaACadastrar, ConteudoACadastrar);
                    }
                    else //Então, estamos na parte de pesquisa
                    {
                        retorno = Conteudo.AdicionarConteudo(LoginVM.professor, DisciplinaSelecionada, ConteudoACadastrar);
                    }
                
                    if (retorno == "Conteúdo adicionado com sucesso!")
                    {
                        await App.Current.MainPage.Navigation.PushModalAsync(new ModalConteudoCadastradoSucesso());

                        AtualizarConteudos();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("ERRO", retorno, "OK");

                        DisciplinaACadastrar = new Disciplina(null, null);
                    }
                }

            }


            private async void IrParaAdicionarConteudoComModalAction()
            {
                ResetarVariaveisConteudo();

                await App.Current.MainPage.Navigation.PopAsync();

                await App.Current.MainPage.Navigation.PopModalAsync();

                await App.Current.MainPage.Navigation.PushAsync(new AdicionarConteudo());
            }


            private void AtualizarConteudos()
            {
                ListaConteudos = Conteudo.BuscarConteudos(LoginVM.professor, DisciplinaSelecionada);
            }       


            private async void IrParaAdicionarConteudoSemModalAction()
            {
                ResetarVariaveisConteudo();  

                await App.Current.MainPage.Navigation.PushAsync(new AdicionarConteudo());
            }        

            private async void FecharModalContCadSucessoAction()
            {
                await App.Current.MainPage.Navigation.PopAsync();

                await App.Current.MainPage.Navigation.PopModalAsync();

                ConteudoACadastrar = new Conteudo(null);
            }


        /*****************************QUESTÕES*****************************/

            private async void IrParaAdicionarQuestaoComModalAction()
            {
                ResetarVariaveisQuestao();

                await App.Current.MainPage.Navigation.PopAsync();

                await App.Current.MainPage.Navigation.PopModalAsync();

                await App.Current.MainPage.Navigation.PushAsync(new AdicionarQuestao());
            }
            
            private async void IrParaAdicionarQuestaoSemModalAction()
            {
                ResetarVariaveisQuestao();

                await App.Current.MainPage.Navigation.PushAsync(new AdicionarQuestao());
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
