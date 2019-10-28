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
        
        public static void AdicionarAlternativas(Professor prof, Disciplina disc, Conteudo cont, Questao quest, List<Alternativa> alt)
        {
            dep.AdicionarAlternativas(quest.Nome_Questao, prof.CPF_Professor, disc.Nome_Disciplina, cont.Nome_Conteudo, alt);
        }

    }
}
