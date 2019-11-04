using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using DescomplicandoTestes.View;
using DescomplicandoTestes.Model;
using System.Globalization;
using DescomplicandoTestes.ViewModel;
using System.Linq;

namespace DescomplicandoTestes.ViewModel
{
    public class DisciplinasViewModel : INotifyPropertyChanged
    {
        #region *********************************Comandos*********************************
        
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

        public Command IrParaAdicionarAlternativas { get; set; } //Comando para ir para tela de adicionar alternativas a partir da tela AdicionarQuestao

        public Command ExcluirAlternativa { get; set; } //Comando para excluir uma alternativas da lista de alternativas

        public Command AdicionarAlternativaListView { get; set; } //Comando para adicionar uma alternativa no listview

        public Command AlternativaMarcada { get; set; } //Comando que será acionado quando uma alternativa for marcada, para decidir qual ficará marcada ou não

        public Command AdicionarQuestao { get; set; } //Comando para adicionar uma questão e suas alternativas no banco de dados

        public Command AdicionarNovaQuestao { get; set; } //Comando para voltar para a tela de adicionar questão

        public Command FecharModalQuestCadSucesso { get; set; } //Comando para fechar o módulo de cadastro de questão

        public Command ExcluirConteudo { get; set; } //Comando para excluir um conteúdo do banco de dados

        public Command ExcluirQuestao { get; set; } //Comando para excluir uma questão do banco de dados

        public Command IrParaEditarDisciplina { get; set; } //Comando para ir para a tela de edição da disciplina

        public Command EditarDisciplina { get; set; } //Comando para editar as informações da disciplina no banco de dados

        public Command IrParaEditarConteudo { get; set; } //Comando para ir para a tela de edição do conteúdo

        public Command EditarConteudo { get; set; } //Comando para editar as informações de um conteúdo de uma disciplina

        public Command IrParaEditarQuestao { get; set; } //Comando para ir para a tela de edição da questão

        public Command IrParaEditarAlternativas { get; set; } //Comando para ir para a tela de edição das alternativas da questão

        public Command EditarQuestao { get; set; } //Comando para editar as informações da questão e suas devidas alternativas

        #endregion

        #region **********************************Listas**********************************

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

        private List<Alternativa> _ListaAlternativas = new List<Alternativa>();
        public List<Alternativa> ListaAlternativas
        {
            get { return _ListaAlternativas; }
            set
            {
                _ListaAlternativas = value;
                OnPropertyChanged("ListaAlternativas");
            }
        }

        #endregion

        #region *********************************Variáveis para exibição*********************************

        public LoginCadastrarViewModel LoginVM
        {
            get { return ViewModel.ViewModelLocator.LoginCadastrarVM; }
        }

        List<Conteudo> ConteudosNaoFiltrados;
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

                ConteudosNaoFiltrados = new List<Conteudo>(ListaConteudos);

                ConteudoAPesquisar = "";
            }
        }

        List<Questao> QuestoesNaoFiltradas;
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

                QuestoesNaoFiltradas = new List<Questao>(ListaQuestoes);

                QuestaoAPesquisar = "";
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

        #endregion

        #region *********************************Variáveis para cadastro*********************************

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


        private Questao _QuestaoACadastrar = new Questao("","","","",Char.MinValue);
        public Questao QuestaoACadastrar
        {
            get { return _QuestaoACadastrar; }
            set
            {
                _QuestaoACadastrar = value;
                OnPropertyChanged("QuestaoACadastrar");
            }
        }

        #endregion

        #region *********************************Variáveis para pesquisa*********************************

        private string _DisciplinaAPesquisar = "";
        public string DisciplinaAPesquisar
        {
            get { return _DisciplinaAPesquisar; }
            set
            {
                _DisciplinaAPesquisar = value;
                OnPropertyChanged("DisciplinaAPesquisar");

                if (string.IsNullOrWhiteSpace(value))
                {
                    ListaDisciplinas = null;
                    ListaDisciplinas = DisciplinasNaoFiltradas;
                }
                else
                {
                    List<Disciplina> DisciplinasFiltradas = new List<Disciplina>();

                    DisciplinasFiltradas = DisciplinasNaoFiltradas.Where(w => (w.Nome_Disciplina.ToLower()).Contains(_DisciplinaAPesquisar)).ToList();

                    ListaDisciplinas = null;
                    ListaDisciplinas = DisciplinasFiltradas;
                }
            }
        }


        private string _ConteudoAPesquisar = "";
        public string ConteudoAPesquisar
        {
            get { return _ConteudoAPesquisar; }
            set
            {
                _ConteudoAPesquisar = value;
                OnPropertyChanged("ConteudoAPesquisar");

                if (string.IsNullOrWhiteSpace(value))
                {
                    ListaConteudos = null;
                    ListaConteudos = ConteudosNaoFiltrados;
                }
                else
                {
                    List<Conteudo> ConteudosFiltrados = new List<Conteudo>();

                    ConteudosFiltrados = ConteudosNaoFiltrados.Where(w => (w.Nome_Conteudo.ToLower()).Contains(_ConteudoAPesquisar)).ToList();

                    ListaConteudos = null;
                    ListaConteudos = ConteudosFiltrados;
                }
            }
        }


        private string _QuestaoAPesquisar = "";
        public string QuestaoAPesquisar
        {
            get { return _QuestaoAPesquisar; }
            set
            {
                _QuestaoAPesquisar = value;
                OnPropertyChanged("QuestaoAPesquisar");

                if (string.IsNullOrWhiteSpace(value))
                {
                    ListaQuestoes = null;
                    ListaQuestoes = QuestoesNaoFiltradas;
                }
                else
                {
                    List<Questao> QuestoesFiltradas = new List<Questao>();

                    QuestoesFiltradas = QuestoesNaoFiltradas.Where(w => (w.Nome_Questao.ToLower()).Contains(_QuestaoAPesquisar)).ToList();

                    ListaQuestoes = null;
                    ListaQuestoes = QuestoesFiltradas;
                }
            }
        }

        #endregion

        #region *********************************Variáveis para edição*********************************

        private Disciplina _DisciplinaAEditar = new Disciplina(null, null);
        public Disciplina DisciplinaAEditar
        {
            get { return _DisciplinaAEditar; }
            set
            {
                _DisciplinaAEditar = value;
                OnPropertyChanged("DisciplinaAEditar");
            }
        }

        private Conteudo _ConteudoAEditar = new Conteudo(null);
        public Conteudo ConteudoAEditar
        {
            get { return _ConteudoAEditar; }
            set
            {
                _ConteudoAEditar = value;
                OnPropertyChanged("ConteudoAEditar");
            }
        }
        
        private Questao _QuestaoAEditar = new Questao("", "", "", "", Char.MinValue);
        public Questao QuestaoAEditar
        {
            get { return _QuestaoAEditar; }
            set
            {
                _QuestaoAEditar = value;
                OnPropertyChanged("QuestaoAEditar");

                /****************Consulta ao BD****************/
                ListaAlternativas = null;
                ListaAlternativas = Alternativa.BuscarAlternativas(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado, QuestaoAEditar);

                foreach (var item in ListaAlternativas)
                {
                    if (item.Letra == QuestaoAEditar.Resposta)
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

        #endregion

        #region *********************************Construtor*********************************

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
            IrParaAdicionarAlternativas = new Command(IrParaAdicionarAlternativasAction);
            ExcluirAlternativa = new Command(ExcluirAlternativaAction);
            AdicionarAlternativaListView = new Command(AdicionarAlternativaListViewAction);
            AlternativaMarcada = new Command(AlternativaMarcadaAction);
            AdicionarQuestao = new Command(AdicionarQuestaoAction);
            AdicionarNovaQuestao = new Command(AdicionarNovaQuestaoAction);
            FecharModalQuestCadSucesso = new Command(FecharModalQuestCadSucessoAction);
            ExcluirConteudo = new Command(ExcluirConteudoAction);
            ExcluirQuestao = new Command(ExcluirQuestaoAction);
            IrParaEditarDisciplina = new Command(IrParaEditarDisciplinaAction);
            EditarDisciplina = new Command(EditarDisciplinaAction);
            IrParaEditarConteudo = new Command(IrParaEditarConteudoAction);
            EditarConteudo = new Command(EditarConteudoAction);
            IrParaEditarQuestao = new Command(IrParaEditarQuestaoAction);
            IrParaEditarAlternativas = new Command(IrParaEditarAlternativasAction);
            EditarQuestao = new Command(EditarQuestaoAction);
        }

        #endregion

        #region ******************************Métodos para resetar variáveis******************************

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
                
                QuestaoACadastrar.Nome_Questao = "";
                QuestaoACadastrar.Resposta = Char.MinValue;
                QuestaoACadastrar.Imagem = "";
                QuestaoACadastrar.Dificuldade = "";
                QuestaoACadastrar.Enunciado = "";

                QuestaoAEditar.Nome_Questao = "";
                QuestaoAEditar.Resposta = Char.MinValue;
                QuestaoAEditar.Imagem = "";
                QuestaoAEditar.Dificuldade = "";
                QuestaoAEditar.Enunciado = "";
            }

        #endregion

        #region *****************************Métodos Disciplinas*****************************

        private void IrParaCadastroDisciplinaAction()
            {
                ResetarVariaveisDisciplina();

                ResetarVariaveisConteudo();

                ResetarVariaveisQuestao();

                App.Current.MainPage.Navigation.PushAsync(new CadastrarDisciplina());
            }

            List<Disciplina> DisciplinasNaoFiltradas;

            private void PesquisarDisciplinasAction()
            {
                ResetarVariaveisDisciplina();

                ResetarVariaveisConteudo();

                ResetarVariaveisQuestao();

                /****************Consulta ao BD****************/
                ListaDisciplinas = Disciplina.BuscarDisciplinas(LoginVM.professor);

                DisciplinasNaoFiltradas = new List<Disciplina>(ListaDisciplinas);

                DisciplinaAPesquisar = "";

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

            Disciplina BkpDisc;
            private async void IrParaEditarDisciplinaAction(object obj)
            {
                DisciplinaAEditar = new Disciplina((obj as Disciplina).Nome_Disciplina, (obj as Disciplina).Sigla);

                BkpDisc = new Disciplina(DisciplinaAEditar.Nome_Disciplina, DisciplinaAEditar.Sigla);

                await App.Current.MainPage.Navigation.PushAsync(new EditarDisciplina());

            }

            private async void EditarDisciplinaAction()
            {
                if (DisciplinaAEditar.Nome_Disciplina == null)
                {
                    await App.Current.MainPage.DisplayAlert("ERRO", "Por favor preencha o nome da disciplina!", "OK");
                }
                else
                {                    
                    string retorno = Disciplina.EditarDisciplina(LoginVM.professor, BkpDisc, DisciplinaAEditar);

                    if (retorno == "Disciplina editada com sucesso!")
                    {
                        await App.Current.MainPage.DisplayAlert("SUCESSO", retorno, "OK");

                        ResetarVariaveisDisciplina();

                        ListaDisciplinas.Clear();

                        ListaDisciplinas = Disciplina.BuscarDisciplinas(LoginVM.professor);

                        await App.Current.MainPage.Navigation.PopAsync();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("ERRO", retorno, "OK");

                    }
                }
            }

        #endregion

        #region *****************************Métodos Conteúdos*****************************


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

            private async void ExcluirConteudoAction(object obj)
            {
                var confirma = await App.Current.MainPage.DisplayAlert("Confirmação", "Deseja realmente excluir esse conteúdo?", "SIM", "NÃO");


                if (confirma)
                {
                    Conteudo cont = obj as Conteudo;

                    string retorno = Conteudo.ExcluirConteudo(LoginVM.professor, DisciplinaSelecionada, cont);

                    if (retorno == "Conteúdo excluído com sucesso!")
                    {
                        App.Current.MainPage.DisplayAlert("SUCESSO", retorno, "OK");


                        /****************Consulta ao BD****************/
                        ListaConteudos = Conteudo.BuscarConteudos(LoginVM.professor, DisciplinaSelecionada);
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("ERRO", retorno, "OK");
                    }
                }
            }

            Conteudo BkpCont;
            private async void IrParaEditarConteudoAction(object obj)
            {
                ConteudoAEditar = new Conteudo((obj as Conteudo).Nome_Conteudo);

                BkpCont = new Conteudo(ConteudoAEditar.Nome_Conteudo);

                await App.Current.MainPage.Navigation.PushAsync(new EditarConteudo());
            }


            private async void EditarConteudoAction()
            {
                if (ConteudoAEditar.Nome_Conteudo == null)
                {
                    await App.Current.MainPage.DisplayAlert("ERRO", "Por favor preencha o nome do conteúdo!", "OK");
                }
                else
                {
                    string retorno = Conteudo.EditarConteudo(LoginVM.professor, DisciplinaSelecionada, BkpCont, ConteudoAEditar);

                    if (retorno == "Conteúdo editado com sucesso!")
                    {
                        await App.Current.MainPage.DisplayAlert("SUCESSO", retorno, "OK");

                        ResetarVariaveisConteudo();

                        ListaConteudos.Clear();

                        ListaConteudos = Conteudo.BuscarConteudos(LoginVM.professor, DisciplinaSelecionada);

                        await App.Current.MainPage.Navigation.PopAsync();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("ERRO", retorno, "OK");

                    }
                }
            }

        #endregion

        #region *****************************Métodos Questões*****************************

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

            private async void IrParaAdicionarAlternativasAction()
            {
                if (QuestaoACadastrar.Nome_Questao == "" || QuestaoACadastrar.Enunciado == "" || QuestaoACadastrar.Dificuldade == "")
                {
                    await App.Current.MainPage.DisplayAlert("ERRO", "Por favor, preencha todos os campos!", "OK");
                }
                else
                {
                    ListaAlternativas.Clear();

                    ListaAlternativas.Add(new Alternativa('A', ""));

                    await App.Current.MainPage.Navigation.PushAsync(new AdicionarAlternativas());
                }

                
            }

            private async void ExcluirAlternativaAction(object obj)
            {
                var confirma = await App.Current.MainPage.DisplayAlert("Confirmação", "Deseja realmente excluir essa alternativa?", "SIM", "NÃO");


                if (confirma)
                {
                    Alternativa alt = obj as Alternativa;

                    ListaAlternativas.Remove(ListaAlternativas.Where(i => i.Letra == alt.Letra).Single());

                    string alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                    List<Alternativa> ListaAuxiliar = new List<Alternativa>(ListaAlternativas);

                    int pos = 0;
                    foreach (Alternativa item in ListaAuxiliar)
                    {
                        item.Letra = alfabeto[pos];
                        pos++;
                    }

                    ListaAlternativas = null;

                    ListaAlternativas = ListaAuxiliar;

                }

            }

            private void AdicionarAlternativaListViewAction()
            {
                if (ListaAlternativas.Count == 5)
                {
                    App.Current.MainPage.DisplayAlert("Aviso", "O número máximo de alternativas por questão foi atingido!", "OK");
                }
                else
                {                

                    char letra;

                    if (ListaAlternativas.Count == 0)
                    {
                        letra = 'A';
                    }
                    else
                    {
                        letra = Convert.ToChar(ListaAlternativas.Last().Letra + 1);
                    }

                    ListaAlternativas.Add(new Alternativa(letra, ""));

                    List<Alternativa> ListaAuxiliar = new List<Alternativa>(ListaAlternativas);

                    ListaAlternativas = null;

                    ListaAlternativas = ListaAuxiliar;
                }               

            }

            
            private void AlternativaMarcadaAction(object obj)
            {     
                Alternativa alt = obj as Alternativa;

                foreach (var item in ListaAlternativas)
                {
                    if (item.Letra == alt.Letra)
                    {
                        item.EhResposta = true;
                    }
                    else
                    {
                        item.EhResposta = false;
                    }
                }

                List<Alternativa> ListaAuxiliar = new List<Alternativa>(ListaAlternativas);

                ListaAlternativas = null;

                ListaAlternativas = ListaAuxiliar;
            }

            private async void AdicionarQuestaoAction()
            {
                if (ListaAlternativas.Count < 2)
                {
                    await App.Current.MainPage.DisplayAlert("Aviso", "O número mínino de alternativas para uma questão são duas!", "OK");
                }
                else
                {
                    char resposta = Char.MinValue;

                    bool existeAltVazia = false;

                    foreach (var item in ListaAlternativas) 
                    {
                        if (item.EhResposta == true)
                        {
                            resposta = item.Letra;
                        }

                        if (item.Texto == "")
                        {
                            existeAltVazia = true;
                        }
                    }


                    if (existeAltVazia) //Quer dizer que alguma alternativa está com o texto vazio ainda
                    {
                        await App.Current.MainPage.DisplayAlert("Aviso", "Por favor, preencha o texto de todas as alternativas!", "OK");
                    }
                    else
                    {
                        if (resposta == Char.MinValue) //Quer dizer que não marcou nenhuma resposta
                        {
                            await App.Current.MainPage.DisplayAlert("Aviso", "Por favor, marque uma alternativa como resposta da questão!", "OK");
                        }
                        else
                        {
                            QuestaoACadastrar.Resposta = resposta;

                            string retorno;

                            if (DisciplinaSelecionada.Nome_Disciplina == null && ConteudoSelecionado.Nome_Conteudo == null) //Então, as duas variáveis a serem utilizadas são de cadastro
                            {
                                retorno = Questao.AdicionarQuestao(LoginVM.professor, DisciplinaACadastrar, ConteudoACadastrar, QuestaoACadastrar);

                                Alternativa.AdicionarAlternativas(LoginVM.professor, DisciplinaACadastrar, ConteudoACadastrar, QuestaoACadastrar, ListaAlternativas);
                            }
                            else 
                            {
                                if (ConteudoSelecionado.Nome_Conteudo == null) //Então, a variável da disciplina é a selecionada, e da do conteúdo é a que está sendo cadastrada
                                {
                                    retorno = Questao.AdicionarQuestao(LoginVM.professor, DisciplinaSelecionada, ConteudoACadastrar, QuestaoACadastrar);

                                    Alternativa.AdicionarAlternativas(LoginVM.professor, DisciplinaSelecionada, ConteudoACadastrar, QuestaoACadastrar, ListaAlternativas);
                                }
                                else //Então, as duas variáveis são as selecionadas
                                {
                                    retorno = Questao.AdicionarQuestao(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado, QuestaoACadastrar);

                                    Alternativa.AdicionarAlternativas(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado, QuestaoACadastrar, ListaAlternativas);
                                }
                            }

                            if (retorno == "Questão adicionada com sucesso!")
                            {
                                await App.Current.MainPage.Navigation.PushModalAsync(new ModalQuestaoCadastradaSucesso());

                                if (DisciplinaSelecionada != null) //Só preciso atualizar a lista de questões se antes ele estava na parte de pesquisa
                                {
                                    ListaQuestoes = Questao.BuscarQuestoes(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado);
                                }
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("ERRO", retorno, "OK");

                                QuestaoACadastrar = new Questao("", "", "", "", Char.MinValue);
                            }

                        }
                    }
                   
                }

            }


            private async void AdicionarNovaQuestaoAction()
            {
                ResetarVariaveisQuestao();

                ListaAlternativas.Clear();

                await App.Current.MainPage.Navigation.PopAsync();

                await App.Current.MainPage.Navigation.PopAsync();

                await App.Current.MainPage.Navigation.PushAsync(new AdicionarQuestao());

                App.Current.MainPage.Navigation.PopModalAsync();            
            }


            private async void FecharModalQuestCadSucessoAction()
            {
                await App.Current.MainPage.Navigation.PopAsync();

                await App.Current.MainPage.Navigation.PopAsync();

                await App.Current.MainPage.Navigation.PopModalAsync();

                QuestaoACadastrar = new Questao("", "", "", "", Char.MinValue);

                ListaAlternativas.Clear();
            }


            private async void ExcluirQuestaoAction(object obj)
            {
                var confirma = await App.Current.MainPage.DisplayAlert("Confirmação", "Deseja realmente excluir essa questão?", "SIM", "NÃO");


                if (confirma)
                {
                    Questao quest = obj as Questao;

                    string retorno = Questao.ExcluirQuestao(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado, quest);

                    if (retorno == "Questão excluída com sucesso!")
                    {
                        App.Current.MainPage.DisplayAlert("SUCESSO", retorno, "OK");


                        /****************Consulta ao BD****************/
                        ListaQuestoes = Questao.BuscarQuestoes(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado);
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("ERRO", retorno, "OK");
                    }
                }
            }

            Questao BkpQuest;
            private async void IrParaEditarQuestaoAction(object obj)
            {
                QuestaoAEditar = new Questao((obj as Questao).Nome_Questao, (obj as Questao).Dificuldade, (obj as Questao).Imagem, (obj as Questao).Enunciado, (obj as Questao).Resposta) ;

                BkpQuest = new Questao(QuestaoAEditar.Nome_Questao, null, null, null, Char.MinValue);

                await App.Current.MainPage.Navigation.PushAsync(new EditarQuestao());
            }


            private async void IrParaEditarAlternativasAction()
            {
                if (QuestaoAEditar.Nome_Questao == "" || QuestaoAEditar.Enunciado == "" || QuestaoAEditar.Dificuldade == "")
                {
                    await App.Current.MainPage.DisplayAlert("ERRO", "Por favor, preencha todos os campos!", "OK");
                }
                else
                {
                    List<Alternativa> ListaAuxiliar = new List<Alternativa>(ListaAlternativas);

                    ListaAlternativas = null;

                    ListaAlternativas = ListaAuxiliar;

                    foreach (var item in ListaAlternativas)
                    {
                        if(QuestaoAEditar.Resposta == item.Letra)
                        {
                            item.EhResposta = true;
                        }
                        else
                        {
                            item.EhResposta = false;
                        }
                    }

                    await App.Current.MainPage.Navigation.PushAsync(new EditarAlternativas());
                    
                }
            }


            private async void EditarQuestaoAction()
            {
                if (ListaAlternativas.Count < 2)
                {
                    await App.Current.MainPage.DisplayAlert("Aviso", "O número mínino de alternativas para uma questão são duas!", "OK");
                }
                else
                {
                    char resposta = Char.MinValue;

                    bool existeAltVazia = false;

                    foreach (var item in ListaAlternativas)
                    {
                        if (item.EhResposta == true)
                        {
                            resposta = item.Letra;
                        }

                        if (item.Texto == "")
                        {
                            existeAltVazia = true;
                        }
                    }


                    if (existeAltVazia) //Quer dizer que alguma alternativa está com o texto vazio ainda
                    {
                        await App.Current.MainPage.DisplayAlert("Aviso", "Por favor, preencha o texto de todas as alternativas!", "OK");
                    }
                    else
                    {
                        if (resposta == Char.MinValue) //Quer dizer que não marcou nenhuma resposta
                        {
                            await App.Current.MainPage.DisplayAlert("Aviso", "Por favor, marque uma alternativa como resposta da questão!", "OK");
                        }
                        else
                        {
                            QuestaoAEditar.Resposta = resposta;

                            string retorno;
                            
                            retorno = Questao.EditarQuestao(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado, QuestaoAEditar, BkpQuest);                            
                            
                            if (retorno == "Questão editada com sucesso!")
                            {
                                Alternativa.EditarAlternativas(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado, BkpQuest, QuestaoAEditar, ListaAlternativas);

                                await App.Current.MainPage.DisplayAlert("SUCESSO", "Questão editada com sucesso!", "OK");

                                App.Current.MainPage.Navigation.PopAsync();

                                App.Current.MainPage.Navigation.PopAsync();                                

                                if (DisciplinaSelecionada != null) //Só preciso atualizar a lista de questões se antes ele estava na parte de pesquisa
                                {
                                    ListaQuestoes = Questao.BuscarQuestoes(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado);

                                    ResetarVariaveisQuestao();
                                }
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("ERRO", retorno, "OK");

                            }

                        }
                    }

                }
            }

        #endregion

        #region **************************Método PropertyChanged**************************

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string NameProperty)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(NameProperty));
            }
        }

        #endregion
    }
}
