using DescomplicandoTestes.Banco;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DescomplicandoTestes.Model
{
    public class Questao
    {
        static IConexaoBanco dep = DependencyService.Get<IConexaoBanco>();

        public string Nome_Questao { get; set; }

        public string Dificuldade { get; set; }

        public string Imagem { get; set; }

        public string Enunciado { get; set; }

        public char Resposta { get; set; }


        public Questao(string nomequestao, string dificuldade, string imagem, string enunciado, char resposta)
        {
            Nome_Questao = nomequestao;
            Dificuldade = dificuldade;
            Enunciado = enunciado;
            Resposta = resposta;
            Imagem = imagem;
        }

        /// <summary>
        /// Método estático para buscar as questões de uma disciplina, conteúdo e professor
        /// </summary>
        /// <param name="prof">Professor logado no sistema</param>
        /// <param name="disc">Disciplina selecionada</param>
        /// <param name="cont">Conteúdo selecionado</param>
        /// <returns></returns>
        public static List<Questao> BuscarQuestoes(Professor prof, Disciplina disc, Conteudo cont)
        {
            return (dep.BuscarQuestoes(prof.CPF_Professor, disc.Nome_Disciplina, cont.Nome_Conteudo));
        }

        /// <summary>
        /// Método estático para adicionar uma questão de um conteúdo, disciplina e professor
        /// </summary>
        /// <param name="prof">Professor logado no sistema</param>
        /// <param name="disc">Disciplina</param>
        /// <param name="cont">Conteudo</param>
        /// <param name="quest">Questão a cadastrar</param>
        /// <returns>Retorna uma string de confirmação ou erro</returns>
        public static string AdicionarQuestao(Professor prof, Disciplina disc, Conteudo cont, Questao quest)
        {
            return (dep.AdicionarQuestao(prof.CPF_Professor,disc.Nome_Disciplina, cont.Nome_Conteudo, quest.Nome_Questao, quest.Enunciado, quest.Dificuldade, quest.Resposta));
        }

        /// <summary>
        /// Método estático para excluir uma questão de um conteúdo
        /// </summary>
        /// <param name="prof">Professor logado no sistema</param>
        /// <param name="disc">Disciplina</param>
        /// <param name="cont">Conteúdo</param>
        /// <param name="quest">Questão a ser excluída</param>
        /// <returns></returns>
        public static string ExcluirQuestao(Professor prof, Disciplina disc, Conteudo cont, Questao quest)
        {
            return(dep.ExcluirQuestao(prof.CPF_Professor, disc.Nome_Disciplina, cont.Nome_Conteudo, quest.Nome_Questao));
        }


        /// <summary>
        /// Método estático para editar uma questão
        /// </summary>
        /// <param name="prof">Professor logado</param>
        /// <param name="disc">Disciplina</param>
        /// <param name="cont">Conteudo</param>
        /// <param name="questnova">Novas informações da questão</param>
        /// <param name="questantiga">Questão antiga</param>
        /// <returns></returns>
        public static string EditarQuestao(Professor prof, Disciplina disc, Conteudo cont, Questao questnova, Questao questantiga)
        {
            return (dep.EditarQuestao(prof.CPF_Professor, disc.Nome_Disciplina, cont.Nome_Conteudo, questantiga.Nome_Questao, questnova.Nome_Questao, questnova.Enunciado, questnova.Dificuldade, questnova.Resposta));
        }

    }
}
