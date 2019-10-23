using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using DescomplicandoTestes.Banco;

namespace DescomplicandoTestes.Model
{
    public class Conteudo
    {
        static IConexaoBanco dep = DependencyService.Get<IConexaoBanco>();

        public string Nome_Conteudo { get; set; }

        public Conteudo(string nomeconteudo)
        {
            Nome_Conteudo = nomeconteudo;
        }


        /// <summary>
        /// Método estático para buscar os conteúdos de uma disciplina e de um professor
        /// </summary>
        /// <param name="prof">Professor logado</param>
        /// <param name="disc">Disciplina selecionada</param>
        /// <returns>Retorna uma lista de conteúdos</returns>
        public static List<Conteudo> BuscarConteudos(Professor prof, Disciplina disc)
        {
            return (dep.BuscarConteudos(prof.CPF_Professor, disc.Nome_Disciplina));
        }


        public static string AdicionarConteudo(Professor prof, Disciplina disc, Conteudo cont)
        {
            return (dep.AdicionarConteudo(prof.CPF_Professor, disc.Nome_Disciplina, cont.Nome_Conteudo));
        }
    }
}
