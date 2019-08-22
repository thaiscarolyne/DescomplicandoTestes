using System;
using System.Collections.Generic;
using System.Text;

namespace DescomplicandoTestes.Model
{
    public class Disciplina
    {
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
    }
}
