using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using DescomplicandoTestes.Banco;

namespace DescomplicandoTestes.Model
{
    public class Professor
    {
        IConexaoBanco dep = DependencyService.Get<IConexaoBanco>();

        public string CPF_Professor { get; set; }

        public string Nome { get; set; }

        public string Senha { get; set; }        

        public Professor()
        {
            CPF_Professor = null;
            Nome = null;
            Senha = null;           
        }

        public bool VerificarProfessorCadastrado(Professor prof)
        {
            return (dep.ConsultaProfessor(prof.CPF_Professor, prof.Senha));
        }

        public string CadastrarProfessor(Professor prof)
        {
           return (dep.CadastrarProfessor(prof.CPF_Professor, prof.Senha, prof.Nome));
        }
    }
}
