using DescomplicandoTestes.Banco;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DescomplicandoTestes.Model
{
    public class Prova
    {
        static IConexaoBanco dep = DependencyService.Get<IConexaoBanco>();

        public string Nome_Prova { get; set; }

        public int Valor { get; set; }

        public string Nome_Disciplina { get; set; }

        public string Nome_Conteudo { get; set; }

        public Prova(string nomeprova, int valor, string disc, string cont)
        {
            Nome_Prova = nomeprova;
            Valor = valor;
            Nome_Disciplina = disc;
            Nome_Conteudo = cont;
        }

        /// <summary>
        /// Método para cadastrar uma prova no banco de dados
        /// </summary>
        /// <param name="prof">Professor logado no sistema</param>
        /// <param name="disc">Disciplina selecionada</param>
        /// <param name="cont">Conteúdo selecionado</param>
        /// <param name="turma">Turma selecionada</param>
        /// <param name="prov">Prova a ser cadastrada</param>
        /// <returns></returns>
        public static string CadastrarProva(Professor prof, Disciplina disc, Conteudo cont, Turma turma, Prova prov)
        {
            return (dep.CadastrarProva(prov.Nome_Prova, prof.CPF_Professor, disc.Nome_Disciplina, cont.Nome_Conteudo, turma.Nome_Turma , prov.Valor));
        }

        /// <summary>
        /// Método para buscar as provas de um professor no banco de dados 
        /// </summary>
        /// <param name="prof">Professor logado no sistema</param>
        /// <returns></returns>
        public static List<Prova> BuscarProvas(Professor prof)
        {
            return (dep.BuscarProvas(prof.CPF_Professor));
        }


        public static string AdicionarQuestoesProva(Professor prof, Disciplina disc, Conteudo cont, Prova prov, List<Questao> listaquestoes)
        {
            return (dep.AdicionarQuestoesProva(prov.Nome_Prova, prof.CPF_Professor, disc.Nome_Disciplina, cont.Nome_Conteudo, listaquestoes));
        }


        public static string ExcluirProva(Professor prof, Prova prova)
        {
            return (dep.ExcluirProva(prof.CPF_Professor, prova.Nome_Prova, prova.Nome_Disciplina, prova.Nome_Conteudo));
        }

    }
}
