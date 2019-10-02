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
        void CadastrarDisciplina(string CPF, string nomedisciplina, string sigla);

    }
}
