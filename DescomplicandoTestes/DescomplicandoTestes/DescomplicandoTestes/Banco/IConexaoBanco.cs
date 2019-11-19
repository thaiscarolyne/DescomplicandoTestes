using System;
using System.Collections.Generic;
using System.Text;
using DescomplicandoTestes.Model;

namespace DescomplicandoTestes.Banco
{
    public interface IConexaoBanco
    {
        //Esse método irá retornar se esse CPF com essa senha existe no Banco de Dados
        bool ConsultaProfessor(string CPF, string Senha);

        //Esse método irá buscar quais disciplinas no banco estão relacionadas à esse CPF
        List<Disciplina> BuscarDisciplinas(string CPF);

        //Esse método irá buscar quais conteúdos no banco estão relacionadas à essa disciplina e esse CPF
        List<Conteudo> BuscarConteudos(string CPF, string disciplina);

        //Esse método irá buscar quais questões no banco estão relacionadas à esse conteúdo, disciplina e CPF
        List<Questao> BuscarQuestoes(string CPF, string disciplina, string conteudo);

        //Esse método irá buscar quais alternativas no banco estão relacionadas à essa questão, conteúdo, disciplina e CPF
        List<Alternativa> BuscarAlternativas(string CPF, string disciplina, string conteudo, string questao);

        //Esse método irá cadastrar a disciplina no banco de dados
        string CadastrarDisciplina(string CPF, string nomedisciplina, string sigla);

        //Esse método irá cadastrar o professor no banco de dados
        string CadastrarProfessor(string CPF, string senha, string nome);

        //Esse método irá excluir uma disciplina do banco de dados
        string ExcluirDisciplina(string CPF, string nomedisciplina);

        //Esse método irá adicionar um novo conteúdo vinculado à uma disciplina no banco de dados
        string AdicionarConteudo(string CPF, string nomedisciplina, string nomeconteudo);

        //Esse método irá adicionar uma nova questão vinculada à um conteúdo e disciplina
        string AdicionarQuestao(string CPF, string nomedisciplina, string nomeconteudo, string nomequestao, string enunciado, string dificuldade, char resposta);

        //Esse método irá adicionar alternativas relacionadas à uma questão 
        void AdicionarAlternativas(string nomequestao, string CPF, string nomedisciplina, string nomeconteudo, List<Alternativa> alt);

        //Esse método irá excluir um conteúdo do banco de dados
        string ExcluirConteudo(string CPF, string nomedisciplina, string nomeconteudo);

        //Esse método irá excluir uma questão do banco de dados
        string ExcluirQuestao(string CPF, string nomedisciplina, string nomeconteudo, string nomequestao);

        //Esse método irá atualizar as informações da disciplina
        string EditarDisciplina(string CPF, string nomedisciplinaantiga, string nomedisciplinanova, string siglanova);

        //Esse método irá atualizar as informações do conteúdo
        string EditarConteudo(string CPF, string nomedisciplina, string nomeconteudoantigo, string nomeconteudonovo);

        //Esse método irá atualizar as informações da questão
        string EditarQuestao(string CPF, string nomedisciplina, string nomeconteudo, string nomequestaoantiga, string nomequestaonova, string enunciado, string dificuldade, char resposta);

        //Esse método irá excluir as alternativas que existiam para essa questão, e chamar a função de adicionar alternativas para adicionar as novas
        void EditarAlternativas(string CPF, string nomedisciplina, string nomeconteudo, string nomequestaoantiga, string nomequestaonova, List<Alternativa> alt);

        //Esse método irá cadastrar a prova no banco de dados
        string CadastrarProva(string nomeprova, string CPF, string nomedisciplina, string nomeconteudo, string nometurma, int valor);

        //Esse método irá buscar as provas do professor no banco de dados
        List<Prova> BuscarProvas(string CPF);

        //Esse método irá adicionar as questões relacionadas à prova na tabela QUEST_PROV
        string AdicionarQuestoesProva(string nomeprova, string CPF, string nomedisciplina, string nomeconteudo, List<Questao> listaquestoes);

        //Esse método irá excluir uma prova do banco de dados
        string ExcluirProva(string CPF, string nomeprova, string disciplina, string conteudo);

        //Esse método irá buscar questões de uma determinada prova
        List<Questao> BuscarQuestoesProva(string CPF, string nomeprova, string disciplina, string conteudo);
    }
}
