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
        /// <param name="Disciplina">Nome da disciplina</param>
        /// <param name="Sig">Sigla da disciplina</param>
        public Disciplina(string Disciplina, string Sig)
        {
            Nome_Disciplina = Disciplina;

            Sigla = Sig;
        }
    }
}
