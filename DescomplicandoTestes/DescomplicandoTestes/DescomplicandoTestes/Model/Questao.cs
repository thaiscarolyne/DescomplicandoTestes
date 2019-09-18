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

    }
}
