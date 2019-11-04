using DescomplicandoTestes.Banco;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DescomplicandoTestes.Model
{
    public class Alternativa
    {
        static IConexaoBanco dep = DependencyService.Get<IConexaoBanco>();

        public char Letra { get; set; }

        public string Texto { get; set; }

        public Color Cor { get; set; }

        public bool EhResposta { get; set; }

        public Alternativa(char letra, string texto)
        {
            Letra = letra;
            Texto = texto;
        }


        /// <summary>
        /// Método estático para buscar as alternativas de uma questão, conteúdo, disciplina e professor
        /// </summary>
        /// <param name="prof">Professor logado no sistema</param>
        /// <param name="disc">Disciplina selecionada</param>
        /// <param name="cont">Conteúdo selecionado</param>
        /// <param name="quest">Questão selecionada</param>
        /// <returns></returns>
        public static List<Alternativa> BuscarAlternativas(Professor prof, Disciplina disc, Conteudo cont, Questao quest)
        {
            return (dep.BuscarAlternativas(prof.CPF_Professor, disc.Nome_Disciplina, cont.Nome_Conteudo, quest.Nome_Questao));
        }
        
        /// <summary>
        /// Método estático para adicionar alternativas relacionadas à uma questão
        /// </summary>
        /// <param name="prof">Professor logado no sistema</param>
        /// <param name="disc">Disciplina selecionada</param>
        /// <param name="cont">Conteúdo selecionado</param>
        /// <param name="quest">Questão selecionada</param>
        /// <param name="alt">Lista de alternativas a serem adicionadas</param>
        public static void AdicionarAlternativas(Professor prof, Disciplina disc, Conteudo cont, Questao quest, List<Alternativa> alt)
        {
            dep.AdicionarAlternativas(quest.Nome_Questao, prof.CPF_Professor, disc.Nome_Disciplina, cont.Nome_Conteudo, alt);
        }

        /// <summary>
        /// Método estático para editar alternativas de uma questão
        /// </summary>
        /// <param name="prof">Professor logado</param>
        /// <param name="disc">Disciplina selecionada</param>
        /// <param name="cont">Conteúdo selecionado</param>
        /// <param name="quest">Questão selecionada</param>
        /// <param name="alt">Lista com as novas alternativas</param>
        public static void EditarAlternativas(Professor prof, Disciplina disc, Conteudo cont, Questao questantiga, Questao questnova, List<Alternativa> alt)
        {
            dep.EditarAlternativas(prof.CPF_Professor, disc.Nome_Disciplina, cont.Nome_Conteudo, questantiga.Nome_Questao, questnova.Nome_Questao, alt);
        }

    }
}
