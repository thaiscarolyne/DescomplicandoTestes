using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using DescomplicandoTestes.Banco;
using DescomplicandoTestes.Droid.Banco;
using MySql.Data.MySqlClient;
using DescomplicandoTestes.Model;

[assembly: Dependency(typeof(Conexao))]
namespace DescomplicandoTestes.Droid.Banco
{
    class Conexao : IConexaoBanco
    {       

        public MySqlConnection Conectar()
        {
            MySqlConnection conexaoMySQL;

            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
            {
                Server = "remotemysql.com",
                Database = "OjP46n7ap6",
                UserID = "OjP46n7ap6",
                Password = "AkNuwRvZuN"
            };

            try
            {
                conexaoMySQL = new MySqlConnection(builder.ToString());
                return conexaoMySQL;

            }
            catch (Exception)
            {
                return null;
            }
        }


        public bool ConsultaProfessor(string CPF, string Senha)
        {          
            string query = "SELECT * FROM PROFESSOR WHERE CPF_Professor = '"+CPF+"' && Senha = '"+Senha+"'";
            MySqlConnection conexaoMySQL = Conectar();
            if (conexaoMySQL != null)
            {
                conexaoMySQL.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);
                MySqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                return (rdr.Read());                                
            }
            conexaoMySQL.Close();
            return (false);
        }


        public List<Disciplina> BuscarDisciplinas(string CPF)
        {
            string nome = "";
            string sigla = "";
            List<Disciplina> lista = new List<Disciplina>();

            string query = "SELECT * FROM DISCIPLINA WHERE CPF_Professor = '"+CPF+"'";            

            MySqlConnection conexaoMySQL = Conectar();
            if (conexaoMySQL != null)
            {
                conexaoMySQL.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);
                MySqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    nome = rdr["Nome_Disciplina"].ToString();
                    sigla = rdr["Sigla"].ToString();

                    lista.Add(new Disciplina(nome, sigla));
                }

                conexaoMySQL.Close();
            }

            return (lista);
        }

        public List<Conteudo> BuscarConteudos(string CPF, string disciplina)
        {
            string nome = "";
            List<Conteudo> lista = new List<Conteudo>();

            string query = "SELECT * FROM CONTEUDO WHERE CPF_Professor = '" + CPF + "' && Nome_Disciplina = '" + disciplina + "'";

            MySqlConnection conexaoMySQL = Conectar();
            if (conexaoMySQL != null)
            {
                conexaoMySQL.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);
                MySqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    nome = rdr["Nome_Conteudo"].ToString();

                    lista.Add(new Conteudo(nome));
                }

                conexaoMySQL.Close();
            }

            return (lista);
        }

        public List<Questao> BuscarQuestoes(string CPF, string disciplina, string conteudo)
        {
            string nome = "";
            string dificuldade = "";
            string imagem = "";
            string enunciado = "";
            char resposta;

            List<Questao> lista = new List<Questao>();

            string query = "SELECT * FROM QUESTAO WHERE CPF_Professor = '" + CPF + "' && Nome_Disciplina = '" + disciplina+ "' && Nome_Conteudo = '" + conteudo + "'";

            MySqlConnection conexaoMySQL = Conectar();
            if (conexaoMySQL != null)
            {
                conexaoMySQL.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);
                MySqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    nome = rdr["Nome_Questao"].ToString();
                    dificuldade = rdr["Dificuldade"].ToString();
                    imagem = rdr["Imagem"].ToString();
                    enunciado = rdr["Enunciado"].ToString();
                    resposta = ((rdr["Resposta"].ToString()).ToCharArray())[0];

                    lista.Add(new Questao(nome, dificuldade, imagem, enunciado, resposta));
                }

                conexaoMySQL.Close();
            }

            return (lista);
        }

        public List<Alternativa> BuscarAlternativas(string CPF, string disciplina, string conteudo, string questao)
        {
            char letra;
            string texto = "";

            List<Alternativa> lista = new List<Alternativa>();

            string query = "SELECT * FROM ALTERNATIVA WHERE CPF_Professor = '" + CPF + "' && Nome_Disciplina = '" + disciplina + "' && Nome_Conteudo = '" + conteudo + "' && Nome_Questao = '" + questao + "'";

            MySqlConnection conexaoMySQL = Conectar();
            if (conexaoMySQL != null)
            {
                conexaoMySQL.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);
                MySqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    texto = rdr["Texto"].ToString();                    
                    letra = ((rdr["Letra"].ToString()).ToCharArray())[0];

                    lista.Add(new Alternativa(letra, texto));
                }

                conexaoMySQL.Close();
            }

            return (lista);
        }

        public string CadastrarDisciplina(string CPF, string nomedisciplina, string sigla)
        {

            string query = "INSERT INTO DISCIPLINA(Nome_Disciplina, CPF_Professor, Sigla) VALUES('" + nomedisciplina + "', '" + CPF + "', '" + sigla + "')";

            try
            {
                MySqlConnection conexaoMySQL = Conectar();
                if (conexaoMySQL != null)
                {
                    conexaoMySQL.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);

                    cmd.ExecuteNonQuery();

                    conexaoMySQL.Close();

                    return ("Cadastro realizado com sucesso!");
                }
                else
                {
                    return ("Não foi possível se conectar ao banco de dados! Tente novamente mais tarde!");
                }
                
            }
            catch (MySqlException e)
            {
                if (e.Number == 1062) //Erro de duplicidade de chave primária
                {
                    return ("Essa disciplina já está cadastrada em sua base de dados!");
                }
                else
                {
                    return ("Erro: " + e.Number);
                }
            }


            
        }

        public string CadastrarProfessor(string CPF, string senha, string nome)
        {
            string query = "INSERT INTO PROFESSOR(CPF_Professor, Senha, Nome) VALUES ('" + CPF + "', '" + senha + "', '" + nome + "')";

            try
            {
                MySqlConnection conexaoMySQL = Conectar();
                if (conexaoMySQL != null)
                {
                    conexaoMySQL.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);

                    cmd.ExecuteNonQuery();

                    conexaoMySQL.Close();

                    return ("Cadastro realizado com sucesso!");
                }
                else
                {
                    return ("Não foi possível se conectar ao banco de dados! Tente novamente mais tarde!");
                }

                
            }
            catch (MySqlException e)
            {
                if (e.Number == 1062) //Erro de duplicidade de chave primária
                {
                    return ("Esse CPF já se encontra cadastrado na base de dados!");
                }
                else
                {
                    return ("Erro: " + e.Number);
                }               
            }

            
        }

        public string ExcluirDisciplina(string CPF, string nomedisciplina)
        {
            string query = "DELETE FROM DISCIPLINA WHERE Nome_Disciplina='" + nomedisciplina + "' && CPF_Professor='" + CPF + "'";

            try
            {
                MySqlConnection conexaoMySQL = Conectar();
                if (conexaoMySQL != null)
                {
                    conexaoMySQL.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);

                    cmd.ExecuteNonQuery();

                    conexaoMySQL.Close();

                    return ("Disciplina excluída com sucesso!");
                }
                else
                {
                    return ("Não foi possível se conectar ao banco de dados! Tente novamente mais tarde!");
                }


            }
            catch (MySqlException e)
            {                
                return ("Erro: " + e.Number);
            }
        }
    }
}