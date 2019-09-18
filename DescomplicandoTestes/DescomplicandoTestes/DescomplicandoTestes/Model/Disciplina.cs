using DescomplicandoTestes.Banco;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DescomplicandoTestes.Model
{
    public class Disciplina
    {
        static IConexaoBanco dep = DependencyService.Get<IConexaoBanco>();

        public string Nome_Disciplina { get; set; }

        public string Sigla { get; set; }

        /// <summary>
        /// Construtor da classe Disciplina
        /// </summary>
        /// <param name="nomedisciplina">Nome da disciplina</param>
        /// <param name="sigla">Sigla da disciplina</param>
        public Disciplina(string nomedisciplina, string sigla)
        {
            Nome_Disciplina = nomedisciplina;

            Sigla = sigla;
        }

        /// <summary>
        /// Método estático para buscar as disciplinas de um professor
        /// </summary>
        /// <param name="prof">Professor logado no sistema</param>
        /// <returns></returns>
        public static List<Disciplina> BuscarDisciplinas(Professor prof)
        {
            return (dep.BuscarDisciplinas(prof.CPF_Professor));
        }


    }
}
