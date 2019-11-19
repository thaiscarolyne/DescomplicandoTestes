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
using System.IO;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using DescomplicandoTestes.PDF;
using Syncfusion.Pdf.Grid;
using System.Reflection;

namespace DescomplicandoTestes.ViewModel
{
    public class DisciplinasViewModel : INotifyPropertyChanged
    {
        #region *********************************Comandos*********************************
        
        public Command PesquisarDisciplinas { get; set; } //Comando para selecionar as disciplinas do professor no banco de dados

        public Command PesquisarProvas { get; set; } //Comando para selecionar as provas do professor no banco de dados

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

        public Command IrParaGerarProva01 { get; set; } //Comando para ir para a primeira tela de gerar prova

        public Command IrParaGerarProva02 { get; set; } //Comando para ir para a segunda tela de gerar prova

        public Command IrParaGerarProva03 { get; set; } //Comando para ir para a terceira tela de gerar prova

        public Command GerarProva { get; set; } //Comando para gerar a prova

        public Command ExcluirProva { get; set; } //Comando para excluir prova

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

        private List<int> _ListaQtdFaceis;
        public List<int> ListaQtdFaceis
        {
            get { return _ListaQtdFaceis; }
            set
            {
                _ListaQtdFaceis = value;
                OnPropertyChanged("ListaQtdFaceis");
            }
        }

        private List<int> _ListaQtdMedias;
        public List<int> ListaQtdMedias
        {           
            get { return _ListaQtdMedias; }
            set
            {
                _ListaQtdMedias = value;
                OnPropertyChanged("ListaQtdMedias");
            }
        }        

        private List<int> _ListaQtdDificeis;
        public List<int> ListaQtdDificeis
        {
            get { return _ListaQtdDificeis; }
            set
            {
                _ListaQtdDificeis = value;
                OnPropertyChanged("ListaQtdDificeis");
            }
        }

        private List<Prova> _ListaProvas;
        public List<Prova> ListaProvas
        {
            get { return _ListaProvas; }
            set
            {
                _ListaProvas = value;
                OnPropertyChanged("ListaProvas");
            }
        }


        #endregion

        #region *********************************Variáveis para exibição*********************************

        public LoginCadastrarViewModel LoginVM
        {
            get { return ViewModel.ViewModelLocator.LoginCadastrarVM; }
        }

        List<Conteudo> ConteudosNaoFiltrados;
        private Disciplina _DisciplinaSelecionada;
        public Disciplina DisciplinaSelecionada
        {
            get { return _DisciplinaSelecionada; }
            set
            {
                if (value.Nome_Disciplina != "" && value.Nome_Disciplina != null && value.Nome_Disciplina != DisciplinaSelecionada.Nome_Disciplina)
                {
                    ConteudoSelecionado.Nome_Conteudo = null;

                    _DisciplinaSelecionada = value;
                    OnPropertyChanged("DisciplinaSelecionada");

                    /****************Consulta ao BD****************/
                    ListaConteudos = null;
                    ListaConteudos = Conteudo.BuscarConteudos(LoginVM.professor, DisciplinaSelecionada);

                    ConteudosNaoFiltrados = new List<Conteudo>(ListaConteudos);

                    ConteudoAPesquisar = "";
                }
            }
        }

        List<Questao> QuestoesNaoFiltradas;
        private Conteudo _ConteudoSelecionado;
        public Conteudo ConteudoSelecionado
        {
            get { return _ConteudoSelecionado; }
            set
            {
                if (value.Nome_Conteudo != "" && value.Nome_Conteudo != null && value.Nome_Conteudo != ConteudoSelecionado.Nome_Conteudo)
                {
                    QuestaoSelecionada.Nome_Questao = null;
                    QuestaoSelecionada.Dificuldade = null;
                    QuestaoSelecionada.Enunciado = null;
                    QuestaoSelecionada.Imagem = null;
                    QuestaoSelecionada.Resposta = Char.MinValue;

                    _ConteudoSelecionado = value;
                    OnPropertyChanged("ConteudoSelecionado");

                    /****************Consulta ao BD****************/
                    ListaQuestoes = null;
                    ListaQuestoes = Questao.BuscarQuestoes(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado);

                    QuestoesNaoFiltradas = new List<Questao>(ListaQuestoes);

                    QuestaoAPesquisar = "";
                }
            }
        }

        private Questao _QuestaoSelecionada;
        public Questao QuestaoSelecionada
        {
            get { return _QuestaoSelecionada; }
            set
            {
                if (value.Nome_Questao != "" && value.Nome_Questao != null && value.Nome_Questao != QuestaoSelecionada.Nome_Questao)
                {
                    AlternativaSelecionada.Letra = Char.MinValue;
                    AlternativaSelecionada.Texto = null;

                    _QuestaoSelecionada = value;
                    OnPropertyChanged("QuestaoSelecionada");

                    /****************Consulta ao BD****************/
                    ListaAlternativas = null;
                    ListaAlternativas = Alternativa.BuscarAlternativas(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado, QuestaoSelecionada);

                    foreach (var item in ListaAlternativas)
                    {
                        if (item.Letra == QuestaoSelecionada.Resposta)
                        {
                            item.Cor = Xamarin.Forms.Color.FromHex("#72FB9B");
                        }
                        else
                        {
                            item.Cor = Xamarin.Forms.Color.White;
                        }
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
                if (value.Letra != Char.MinValue)
                {
                    _AlternativaSelecionada = value;
                    OnPropertyChanged("AlternativaSelecionada");
                }                
            }
        }

        private int _QtdFaceis = 0;
        public int QtdFaceis
        {
            get { return _QtdFaceis; }
            set
            {
                _QtdFaceis = Convert.ToInt32(value);
                OnPropertyChanged("QtdFaceis");
            }
        }

        private int _QtdMedias = 0;
        public int QtdMedias
        {
            get { return _QtdMedias; }
            set
            {
                _QtdMedias = Convert.ToInt32(value);
                OnPropertyChanged("QtdMedias");
            }
        }

        private int _QtdDificeis = 0;
        public int QtdDificeis
        {
            get { return _QtdDificeis; }
            set
            {
                _QtdDificeis = Convert.ToInt32(value);
                OnPropertyChanged("QtdDificeis");
            }
        }

        private Prova _ProvaSelecionada;
        public Prova ProvaSelecionada
        {
            get { return _ProvaSelecionada; }
            set
            {
                QuestaoSelecionada.Nome_Questao = null;
                QuestaoSelecionada.Dificuldade = null;
                QuestaoSelecionada.Enunciado = null;
                QuestaoSelecionada.Imagem = null;
                QuestaoSelecionada.Resposta = Char.MinValue;

                if (value.Nome_Prova != "" && value.Nome_Prova != ProvaSelecionada.Nome_Prova)
                {                   

                    _ProvaSelecionada = value;
                    OnPropertyChanged("ProvaSelecionada");

                    //É necessário fazer isso para que quando ele clicar em uma questao da prova,
                    //Eu possa fazer um binding com a QuestaoSelecionada

                    DisciplinaSelecionada.Nome_Disciplina = ProvaSelecionada.Nome_Disciplina;
                    DisciplinaSelecionada.Sigla = "";

                    ConteudoSelecionado.Nome_Conteudo = ProvaSelecionada.Nome_Conteudo;

                    /****************Consulta ao BD****************/
                    ListaQuestoes = null;
                    ListaQuestoes = Questao.BuscarQuestoesProva(LoginVM.professor, ProvaSelecionada);
                }
                

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


        private Prova _ProvaACadastrar = new Prova("", 0,"","");
        public Prova ProvaACadastrar
        {
            get { return _ProvaACadastrar; }
            set
            {
                _ProvaACadastrar.Nome_Prova = value.Nome_Prova;
                _ProvaACadastrar.Valor = Convert.ToInt32(value.Valor);
                OnPropertyChanged("ProvaACadastrar");
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
                    ListaDisciplinas = new List<Disciplina>(DisciplinasNaoFiltradas);
                }
                else
                {
                    List<Disciplina> DisciplinasFiltradas = new List<Disciplina>();

                    DisciplinasFiltradas = DisciplinasNaoFiltradas.Where(w => (w.Nome_Disciplina.ToLower()).Contains(_DisciplinaAPesquisar)).ToList();

                    ListaDisciplinas = new List<Disciplina>(DisciplinasFiltradas);
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
                    ListaConteudos = new List<Conteudo>(ConteudosNaoFiltrados);
                }
                else
                {
                    List<Conteudo> ConteudosFiltrados = new List<Conteudo>();

                    ConteudosFiltrados = ConteudosNaoFiltrados.Where(w => (w.Nome_Conteudo.ToLower()).Contains(_ConteudoAPesquisar)).ToList();

                    ListaConteudos = new List<Conteudo>(ConteudosFiltrados);
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
                    ListaQuestoes = new List<Questao>(QuestoesNaoFiltradas);
                }
                else
                {
                    List<Questao> QuestoesFiltradas = new List<Questao>();

                    QuestoesFiltradas = QuestoesNaoFiltradas.Where(w => (w.Nome_Questao.ToLower()).Contains(_QuestaoAPesquisar)).ToList();

                    ListaQuestoes = new List<Questao>(QuestoesFiltradas);
                }
            }
        }

        List<Prova> ProvasNaoFiltradas = new List<Prova>();
        private string _ProvaAPesquisar = "";
        public string ProvaAPesquisar
        {
            get { return _ProvaAPesquisar; }
            set
            {
                _ProvaAPesquisar = value;
                OnPropertyChanged("ProvaAPesquisar");

                if (string.IsNullOrWhiteSpace(value))
                {
                    ListaProvas = new List<Prova>(ProvasNaoFiltradas);
                }
                else
                {
                    List<Prova> ProvasFiltradas = new List<Prova>();

                    ProvasFiltradas = ProvasNaoFiltradas.Where(w => (w.Nome_Prova.ToLower()).Contains(_ProvaAPesquisar)).ToList();

                    ListaProvas = new List<Prova>(ProvasFiltradas);
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
                        item.Cor = Xamarin.Forms.Color.FromHex("#72FB9B");
                    }
                    else
                    {
                        item.Cor = Xamarin.Forms.Color.White;
                    }
                }
            }
        }

        #endregion

        #region *********************************Construtor*********************************

        public DisciplinasViewModel()
        {
            _ListaDisciplinas = new List<Disciplina>();
            _ListaConteudos = new List<Conteudo>();
            _ListaQuestoes = new List<Questao>();
            _ListaAlternativas = new List<Alternativa>();
            _ListaQtdFaceis = new List<int>();
            _ListaQtdMedias = new List<int>();
            _ListaQtdDificeis = new List<int>();
            _ListaProvas = new List<Prova>();
            DisciplinasNaoFiltradas = new List<Disciplina>();

            _DisciplinaSelecionada = new Disciplina(null, null);
            _ConteudoSelecionado = new Conteudo(null);
            _QuestaoSelecionada = new Questao(null, null, null, null, Char.MinValue);
            _ProvaSelecionada = new Prova("", 0, "", "");
            _AlternativaSelecionada = new Alternativa(Char.MinValue, null);

            IrParaCadastroDisciplina = new Command(IrParaCadastroDisciplinaAction);
            PesquisarDisciplinas = new Command(PesquisarDisciplinasAction);
            PesquisarProvas = new Command(PesquisarProvasAction);
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
            IrParaGerarProva01 = new Command(IrParaGerarProva01Action);
            IrParaGerarProva02 = new Command(IrParaGerarProva02Action);
            IrParaGerarProva03 = new Command(IrParaGerarProva03Action);
            GerarProva = new Command(GerarProvaAction);
            ExcluirProva = new Command(ExcluirProvaAction);
        }

        #endregion

        #region ******************************Métodos para resetar variáveis******************************

        public void ResetarVariaveisDisciplina()
        {
            /*DISCIPLINA*/
            /*
            DisciplinaSelecionada.Nome_Disciplina = null;
            DisciplinaSelecionada.Sigla = null;
            */
            DisciplinaSelecionada.Nome_Disciplina = null;
            DisciplinaSelecionada.Sigla = null;

            DisciplinaACadastrar.Nome_Disciplina = null;
            DisciplinaACadastrar.Sigla = null;
        }

        public void ResetarVariaveisConteudo()
        {
            /*CONTEÚDO*/

            ConteudoSelecionado.Nome_Conteudo = null;

            ConteudoACadastrar.Nome_Conteudo = null;
        }

        public void ResetarVariaveisQuestao()
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


        public List<Disciplina> DisciplinasNaoFiltradas;

        private void PesquisarDisciplinasAction()
        {
            ResetarVariaveisDisciplina();

            ResetarVariaveisConteudo();

            ResetarVariaveisQuestao();

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
                    ListaDisciplinas = new List<Disciplina>();

                    ListaDisciplinas = Disciplina.BuscarDisciplinas(LoginVM.professor);

                    DisciplinasNaoFiltradas = new List<Disciplina>(ListaDisciplinas);

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

                    DisciplinasNaoFiltradas = new List<Disciplina>(ListaDisciplinas);
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

                    DisciplinasNaoFiltradas = new List<Disciplina>(ListaDisciplinas);

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


        private void AtualizarConteudos()
        {
            if (DisciplinaSelecionada.Nome_Disciplina == null) //Então, estamos na parte de cadastro
            {
                ListaConteudos = new List<Conteudo>(Conteudo.BuscarConteudos(LoginVM.professor, DisciplinaACadastrar));
            }
            else //Então, estamos na parte de pesquisa
            {
                ListaConteudos = new List<Conteudo>(Conteudo.BuscarConteudos(LoginVM.professor, DisciplinaSelecionada));
            }
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

                    ListaAlternativas = new List<Alternativa>(ListaAuxiliar);

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

                    ListaAlternativas = new List<Alternativa>(ListaAuxiliar);
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

                ListaAlternativas = new List<Alternativa>(ListaAuxiliar);
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

                                if (DisciplinaSelecionada.Nome_Disciplina != null) //Só preciso atualizar a lista de questões se antes ele estava na parte de pesquisa
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
                
                    ListaAlternativas = new List<Alternativa>(ListaAuxiliar);

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
        
        #region *****************************Métodos Provas*****************************

        private void IrParaGerarProva01Action()
        {
            ProvaACadastrar.Nome_Prova = "";
            ProvaACadastrar.Valor = 0;

            DisciplinaSelecionada.Nome_Disciplina = null;
            DisciplinaSelecionada.Sigla = null;

            ConteudoSelecionado.Nome_Conteudo = null;

            App.Current.MainPage.Navigation.PushAsync(new GerarProva01());
        }

        public void FiltrarConteudosAction(Disciplina disc)
        {
            ListaConteudos.Clear();

            DisciplinaSelecionada = disc;
        }

        public void ConteudoProvaSelecionado(Conteudo cont)
        {
            ListaQuestoes.Clear();

            ConteudoSelecionado = cont;

            ListaQuestoes = Questao.BuscarQuestoes(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado);

            int facil = 0, medio = 0, dificil = 0;

            foreach (var item in ListaQuestoes)
            {
                if (item.Dificuldade == "Fácil")
                {
                    facil++;
                }
                else
                {
                    if(item.Dificuldade == "Médio")
                    {
                        medio++;
                    }
                    else
                    {
                        dificil++;
                    }
                }
            }

            ListaQtdFaceis.Clear();
            ListaQtdMedias.Clear();
            ListaQtdDificeis.Clear();

            int contador = 0;
            while(contador <= facil)
            {
                ListaQtdFaceis.Add(contador);
                contador++;
            }

            contador = 0;
            while (contador <= medio)
            {
                ListaQtdMedias.Add(contador);
                contador++;
            }

            contador = 0;
            while (contador <= dificil)
            {
                ListaQtdDificeis.Add(contador);
                contador++;
            }
        }

        private async void IrParaGerarProva02Action()
        {
            if (ProvaACadastrar.Nome_Prova == "" || ProvaACadastrar.Valor == 0 || DisciplinaSelecionada.Nome_Disciplina == null || ConteudoSelecionado.Nome_Conteudo == null)
            {
                await App.Current.MainPage.DisplayAlert("ERRO", "Por favor, preencha todos os campos!", "OK");
            }
            else
            {
                //COLOQUEI A TURMA MANUALMENTE PQ CASO CONTRÁRIO TERIA QUE FAZER AS TELAS PARA CADASTRAR TURMA PARA O PROFESSOR
                //ATÉ QUE SE CRIE ESSAS TELAS, APENAS O PROFESSOR COM CPF 11234532476 CONSEGUIRÁ CADASTRAR PROVAS, POIS APENAS
                //PARA ELE CADASTREI A TURMA DIRETAMENTE NO BANCO DE DADOS
                string retorno = Prova.CadastrarProva(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado, new Turma("BD-EC-02-19"), ProvaACadastrar);

                if (retorno == "Cadastro realizado com sucesso!")
                {
                    ListaProvas = new List<Prova>();

                    ListaProvas = Prova.BuscarProvas(LoginVM.professor);

                    await App.Current.MainPage.Navigation.PushAsync(new GerarProva02());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("ERRO", retorno, "OK");

                    await App.Current.MainPage.Navigation.PopAsync();
                }                
            }            
        }

        public void QtdFaceisSelecionada(int Qtd)
        {
            QtdFaceis = Qtd;
        }

        public void QtdMediasSelecionada(int Qtd)
        {
            QtdMedias = Qtd;
        }

        public void QtdDificeisSelecionada(int Qtd)
        {
            QtdDificeis = Qtd;
        }

        private void IrParaGerarProva03Action()
        {
            if(QtdFaceis == 0 && QtdMedias == 0 && QtdDificeis == 0)
            {
                App.Current.MainPage.DisplayAlert("ERRO", "Não é possível gerar a prova sem questões!", "OK");
            }
            else
            {
                List<Questao> QuestoesFaceis = new List<Questao>();
                List<Questao> QuestoesMedias = new List<Questao>();
                List<Questao> QuestoesDificeis = new List<Questao>();

                int cont = 0;
                
                int contfaceis = QtdFaceis;
                int contmedias = QtdMedias;
                int contdificeis = QtdDificeis;

                foreach (var item in ListaQuestoes)
                {
                    if (item.Dificuldade == "Fácil")
                    {
                        QuestoesFaceis.Add(item);
                    }
                    if (item.Dificuldade == "Médio")
                    {
                        QuestoesMedias.Add(item);
                    }
                    if (item.Dificuldade == "Difícil")
                    {
                        QuestoesDificeis.Add(item);
                    }

                }

                var rnd = new Random();

                ListaQuestoes.Clear();

                while (contfaceis > 0)
                {
                    int pos = rnd.Next(0, QuestoesFaceis.Count());
                    ListaQuestoes.Add(QuestoesFaceis[pos]);
                    QuestoesFaceis.RemoveAt(pos);
                    contfaceis--;
                }

                while (contmedias > 0)
                {
                    int pos = rnd.Next(0, QuestoesMedias.Count());
                    ListaQuestoes.Add(QuestoesMedias[pos]);
                    QuestoesMedias.RemoveAt(pos);
                    contmedias--;
                }

                while (contdificeis > 0)
                {
                    int pos = rnd.Next(0, QuestoesDificeis.Count());
                    ListaQuestoes.Add(QuestoesDificeis[pos]);
                    QuestoesDificeis.RemoveAt(pos);
                    contdificeis--;
                }
                

                string retorno = Prova.AdicionarQuestoesProva(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado, ProvaACadastrar, ListaQuestoes);

                if (retorno == "Questoes vinculadas à prova com sucesso!")
                {
                    App.Current.MainPage.Navigation.PushAsync(new GerarProva03());
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("ERRO", retorno, "OK");

                    ProvaACadastrar = new Prova("", 0,"","");

                    ResetarVariaveisDisciplina();

                    ResetarVariaveisConteudo();

                    App.Current.MainPage.Navigation.PopAsync();
                }
                
            }           
        }

        private async void GerarProvaAction()
        {
            PdfDocument document = new PdfDocument();

            PdfPage page = document.Pages.Add();

            PdfGrid pdfGrid = new PdfGrid();

            PdfGraphics graphics = page.Graphics;
            //graphics.DrawString(ListaQuestoes[0].Enunciado, font, PdfBrushes.Black, new RectangleF(0,0, page.GetClientSize().Width, page.GetClientSize().Height));


            PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 15);

            PdfFont font2 = new PdfStandardFont(PdfFontFamily.TimesRoman, 18, PdfFontStyle.Bold);

            PdfGridLayoutResult gridResult = pdfGrid.Draw(page, new PointF(0, 0));

            PdfTextElement textElement;

            if (ProvaACadastrar.Nome_Prova != "")
            {
                textElement = new PdfTextElement("Disciplina: " + DisciplinaSelecionada.Nome_Disciplina + "\n\nConteúdo: " + ConteudoSelecionado.Nome_Conteudo + "\n\nValor: " + ProvaACadastrar.Valor + " pontos", font2, PdfBrushes.Black);
            }
            else
            {
                textElement = new PdfTextElement("Disciplina: " + DisciplinaSelecionada.Nome_Disciplina + "\n\nConteúdo: " + ConteudoSelecionado.Nome_Conteudo + "\n\nValor: " + ProvaSelecionada.Valor + " pontos", font2, PdfBrushes.Black);
            }



            //graphics.DrawRectangle(PdfPens.LightPink, PdfBrushes.LightPink, new RectangleF(0, 0, page.GetClientSize().Width, 50));

            PdfLayoutResult result = textElement.Draw( gridResult.Page, new RectangleF(0, 20, page.GetClientSize().Width, page.GetClientSize().Height));

            textElement.StringFormat = new PdfStringFormat(PdfTextAlignment.Justify);

            int cont = 1;
            List<Alternativa> alternativas = new List<Alternativa>();
            foreach (var item in ListaQuestoes)
            {
                alternativas.Clear();

                alternativas = Alternativa.BuscarAlternativas(LoginVM.professor, DisciplinaSelecionada, ConteudoSelecionado, item);

                textElement = new PdfTextElement(cont + ") " + item.Enunciado , font, PdfBrushes.Black);

                if (result.Bounds.Bottom + 150 > page.Size.Height)
                {
                    page = document.Pages.Add();

                    gridResult = pdfGrid.Draw(page, new PointF(0, 0));

                    result = textElement.Draw(gridResult.Page, new RectangleF(0, 30, page.GetClientSize().Width, page.GetClientSize().Height));

                }
                else
                {
                    result = textElement.Draw(gridResult.Page, new RectangleF(0, result.Bounds.Bottom + 30, page.GetClientSize().Width, page.GetClientSize().Height));
                }


                foreach (var itemalt in alternativas)
                {
                    textElement = new PdfTextElement(itemalt.Letra + ") " + itemalt.Texto, font, PdfBrushes.Black);

                    if (result.Bounds.Bottom + 150 > page.Size.Height)
                    {
                        page = document.Pages.Add();

                        gridResult = pdfGrid.Draw(page, new PointF(0, 0));

                        result = textElement.Draw(gridResult.Page, new RectangleF(0, 10, page.GetClientSize().Width, page.GetClientSize().Height));
                    }
                    else
                    {
                        result = textElement.Draw(gridResult.Page, new RectangleF(0, result.Bounds.Bottom + 10, page.GetClientSize().Width, page.GetClientSize().Height));
                    }
                }

                cont++;
            }

            MemoryStream stream = new MemoryStream();
            document.Save(stream);

            document.Close(true);

            if (ProvaACadastrar.Nome_Prova != "")
            {
                Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Prova_" + ProvaACadastrar.Nome_Prova + ".pdf", "application/pdf", stream);
            }
            else
            {
                Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Prova_" + ProvaSelecionada.Nome_Prova + ".pdf", "application/pdf", stream);
            }


            await App.Current.MainPage.DisplayAlert("SUCESSO", "Prova salva com sucesso em:\nInternalStorage/DescomplicandoTestes", "OK");

            if (ProvaACadastrar.Nome_Prova != "")
            {
                App.Current.MainPage.Navigation.PopAsync();
                App.Current.MainPage.Navigation.PopAsync();
                App.Current.MainPage.Navigation.PopAsync();

                ProvaACadastrar = new Prova("", 0, "", "");
            }                           

        }

        private void PesquisarProvasAction()
        {
            ProvasNaoFiltradas = new List<Prova>(ListaProvas);

            ProvaAPesquisar = "";

            App.Current.MainPage.Navigation.PushAsync(new PesquisarProvas());
        }

        private async void ExcluirProvaAction(object obj)
        {
            var confirma = await App.Current.MainPage.DisplayAlert("Confirmação", "Deseja realmente excluir essa prova?", "SIM", "NÃO");
            
            if (confirma)
            {
                Prova prova = obj as Prova;

                string retorno = Prova.ExcluirProva(LoginVM.professor, prova);

                if (retorno == "Prova excluída com sucesso!")
                {
                    App.Current.MainPage.DisplayAlert("SUCESSO", retorno, "OK");

                    ListaProvas = new List<Prova>();

                    ListaProvas = Prova.BuscarProvas(LoginVM.professor);
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("ERRO", retorno, "OK");
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
