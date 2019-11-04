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

        /// <summary>
        /// Método estático para adicionar um conteúdo em uma disciplina
        /// </summary>
        /// <param name="prof">Professor logado</param>
        /// <param name="disc">Disciplina</param>
        /// <param name="cont">Conteúdo</param>
        /// <returns></returns>
        public static string AdicionarConteudo(Professor prof, Disciplina disc, Conteudo cont)
        {
            return (dep.AdicionarConteudo(prof.CPF_Professor, disc.Nome_Disciplina, cont.Nome_Conteudo));
        }

        /// <summary>
        /// Método estático para excluir um conteúdo de uma disciplina
        /// </summary>
        /// <param name="prof">Professor logado</param>
        /// <param name="disc">Disciplina</param>
        /// <param name="cont">Conteúdo</param>
        /// <returns></returns>
        public static string ExcluirConteudo(Professor prof, Disciplina disc, Conteudo cont)
        {
            return (dep.ExcluirConteudo(prof.CPF_Professor, disc.Nome_Disciplina, cont.Nome_Conteudo));
        }

        /// <summary>
        /// Método estático para editar um conteúdo de uma disciplina
        /// </summary>
        /// <param name="prof">Professor logado</param>
        /// <param name="disc">Disciplina</param>
        /// <param name="contAntigo">Conteúdo antigo</param>
        /// <param name="contNovo">Conteúdo novo</param>
        /// <returns></returns>
        public static string EditarConteudo(Professor prof, Disciplina disc, Conteudo contAntigo, Conteudo contNovo)
        {
            return (dep.EditarConteudo(prof.CPF_Professor, disc.Nome_Disciplina, contAntigo.Nome_Conteudo, contNovo.Nome_Conteudo));
        }
    }
}
