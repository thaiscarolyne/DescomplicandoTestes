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

        public string AdicionarConteudo(string CPF, string nomedisciplina, string nomeconteudo)
        {            
            string query = "INSERT INTO CONTEUDO(Nome_Conteudo, CPF_Professor, Nome_Disciplina) VALUES ('" + nomeconteudo + "', '" + CPF + "', '" + nomedisciplina + "')";

            try
            {
                MySqlConnection conexaoMySQL = Conectar();
                if (conexaoMySQL != null)
                {
                    conexaoMySQL.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);

                    cmd.ExecuteNonQuery();

                    conexaoMySQL.Close();

                    return ("Conteúdo adicionado com sucesso!");
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
                    return ("Esse conteúdo já está cadastrado nessa disciplina!");
                }
                else
                {
                    return ("Erro: " + e.Number);
                }
            }

        }

        public string AdicionarQuestao(string CPF, string nomedisciplina, string nomeconteudo, string nomequestao, string enunciado, string dificuldade, char resposta)
        {
            string query = "INSERT INTO QUESTAO(CPF_Professor, Nome_Disciplina, Nome_Conteudo, Nome_Questao, Enunciado, Dificuldade, Resposta) VALUES ('" + CPF + "', '" + nomedisciplina + "', '" + nomeconteudo + "', '" + nomequestao + "', '" + enunciado + "', '" + dificuldade + "', '" + resposta + "')";

            try
            {
                MySqlConnection conexaoMySQL = Conectar();
                if (conexaoMySQL != null)
                {
                    conexaoMySQL.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);

                    cmd.ExecuteNonQuery();

                    conexaoMySQL.Close();

                    return ("Questão adicionada com sucesso!");
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
                    return ("Esse questão já está cadastrada nesse conteúdo e disciplina!");
                }
                else
                {
                    return ("Erro: " + e.Number);
                }
            }
        }

        public void AdicionarAlternativas(string nomequestao, string CPF, string nomedisciplina, string nomeconteudo, List<Alternativa> alt)
        {
            List<string> queries = new List<string>();
                        
            foreach (var item in alt) //Vai criar uma query para cada alternativa
            {
                queries.Add("INSERT INTO ALTERNATIVA(Nome_Questao, CPF_Professor, Nome_Disciplina, Nome_Conteudo, Texto, Letra) VALUES('" + nomequestao + "', '" + CPF + "', '" + nomedisciplina + "', '" + nomeconteudo + "', '" + item.Texto + "', '" + item.Letra + "')");
            }

            
            MySqlConnection conexaoMySQL = Conectar();
            if (conexaoMySQL != null)
            {
                conexaoMySQL.Open();

                foreach (var item in queries)
                {
                    MySqlCommand cmd = new MySqlCommand(item, conexaoMySQL);

                    cmd.ExecuteNonQuery();
                }      

                conexaoMySQL.Close();
            }    
            
        }

        public string ExcluirConteudo(string CPF, string nomedisciplina, string nomeconteudo)
        {
            string query = "DELETE FROM CONTEUDO WHERE Nome_Conteudo='" + nomeconteudo + "' && CPF_Professor='" + CPF + "' && Nome_Disciplina='" + nomedisciplina + "'";

            try
            {
                MySqlConnection conexaoMySQL = Conectar();
                if (conexaoMySQL != null)
                {
                    conexaoMySQL.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);

                    cmd.ExecuteNonQuery();

                    conexaoMySQL.Close();

                    return ("Conteúdo excluído com sucesso!");
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

        public string ExcluirQuestao(string CPF, string nomedisciplina, string nomeconteudo, string nomequestao)
        {
            string query = "DELETE FROM QUESTAO WHERE Nome_Questao='" + nomequestao + "' && Nome_Conteudo ='" + nomeconteudo + "' && CPF_Professor='" + CPF + "' && Nome_Disciplina='" + nomedisciplina + "'";

            try
            {
                MySqlConnection conexaoMySQL = Conectar();
                if (conexaoMySQL != null)
                {
                    conexaoMySQL.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);

                    cmd.ExecuteNonQuery();

                    conexaoMySQL.Close();

                    return ("Questão excluída com sucesso!");
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

        public string EditarDisciplina(string CPF, string nomedisciplinaantiga, string nomedisciplinanova, string siglanova)
        {
            string query = "UPDATE DISCIPLINA SET Nome_Disciplina='"+nomedisciplinanova+"', Sigla='"+siglanova+"' WHERE Nome_Disciplina='" + nomedisciplinaantiga + "' && CPF_Professor='"+CPF+"'";

            try
            {
                MySqlConnection conexaoMySQL = Conectar();
                if (conexaoMySQL != null)
                {
                    conexaoMySQL.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);

                    cmd.ExecuteNonQuery();

                    conexaoMySQL.Close();

                    return ("Disciplina editada com sucesso!");
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

        public string EditarConteudo(string CPF, string nomedisciplina, string nomeconteudoantigo, string nomeconteudonovo)
        {
            string query = "UPDATE CONTEUDO SET Nome_Conteudo='" + nomeconteudonovo + "' WHERE Nome_Disciplina='" + nomedisciplina + "' && CPF_Professor='" + CPF + "' && Nome_Conteudo='" + nomeconteudoantigo + "'";

            try
            {
                MySqlConnection conexaoMySQL = Conectar();
                if (conexaoMySQL != null)
                {
                    conexaoMySQL.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);

                    cmd.ExecuteNonQuery();

                    conexaoMySQL.Close();

                    return ("Conteúdo editado com sucesso!");
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
                    return ("Esse conteúdo já está cadastrado em sua base de dados!");
                }
                else
                {
                    return ("Erro: " + e.Number);
                }
            }
        }

        public string EditarQuestao(string CPF, string nomedisciplina, string nomeconteudo, string nomequestaoantiga, string nomequestaonova, string enunciado, string dificuldade, char resposta)
        {
            string query = "UPDATE QUESTAO SET Nome_Questao='" + nomequestaonova + "', Enunciado='"+ enunciado + "', Dificuldade='" + dificuldade + "', Resposta='" + resposta + "' WHERE Nome_Questao='" + nomequestaoantiga + "' && Nome_Disciplina='" + nomedisciplina + "' && CPF_Professor='" + CPF + "' && Nome_Conteudo='" + nomeconteudo + "'";

            try
            {
                MySqlConnection conexaoMySQL = Conectar();
                if (conexaoMySQL != null)
                {
                    conexaoMySQL.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);

                    cmd.ExecuteNonQuery();

                    conexaoMySQL.Close();

                    return ("Questão editada com sucesso!");
                }
                else
                {
                    return ("Não foi possível se conectar ao banco de dados! Tente novamente mais tarde!");
                }

            }
            catch (MySqlException e)
            {
                if (e.Number == 1062 || e.Number == 1761) //Erro de duplicidade de chave primária
                {
                    return ("Uma questão com esse nome já está cadastrada em sua base de dados!");
                }
                else
                {
                    return ("Erro: " + e.Number);
                }
            }
        }

        public void EditarAlternativas(string CPF, string nomedisciplina, string nomeconteudo, string nomequestaoantiga, string nomequestaonova, List<Alternativa> alt)
        {
            string query = "DELETE FROM ALTERNATIVA WHERE Nome_Questao='" + nomequestaonova + "' && CPF_Professor='" + CPF + "' && Nome_Disciplina='" + nomedisciplina + "' && Nome_Conteudo='" + nomeconteudo + "'";

            MySqlConnection conexaoMySQL = Conectar();
            if (conexaoMySQL != null)
            {
                conexaoMySQL.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);

                cmd.ExecuteNonQuery();

                conexaoMySQL.Close();

                AdicionarAlternativas(nomequestaonova, CPF, nomedisciplina, nomeconteudo, alt);
            }           

        }

        public string CadastrarProva(string nomeprova, string CPF, string nomedisciplina, string nomeconteudo, string nometurma, int valor)
        {
            
            string query = "INSERT INTO PROVA(Nome_Prova, CPF_Professor, Nome_Disciplina, Nome_Conteudo, Nome_Turma, Valor) VALUES('" + nomeprova + "', '" + CPF + "', '" + nomedisciplina + "', '" + nomeconteudo + "', '" + nometurma + "', " + valor + ")";

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
                    return ("Uma prova com esse nome já está cadastrada em sua base de dados!");
                }
                else
                {
                    return ("Erro: " + e.Number);
                }
            }


        }

        public List<Prova> BuscarProvas(string CPF)
        {
            string nome = "";
            int valor;
            string nome_disciplina = "";
            string nome_conteudo = "";

            List<Prova> lista = new List<Prova>();

            string query = "SELECT * FROM PROVA WHERE CPF_Professor = '" + CPF + "'";

            MySqlConnection conexaoMySQL = Conectar();
            if (conexaoMySQL != null)
            {
                conexaoMySQL.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);
                MySqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    nome = rdr["Nome_Prova"].ToString();
                    valor = Convert.ToInt32((rdr["Valor"].ToString()));
                    nome_disciplina = rdr["Nome_Disciplina"].ToString();
                    nome_conteudo = rdr["Nome_Conteudo"].ToString();

                    lista.Add(new Prova(nome,valor,nome_disciplina, nome_conteudo));
                }

                conexaoMySQL.Close();
            }

            return (lista);
        }

        public string AdicionarQuestoesProva(string nomeprova, string CPF, string nomedisciplina, string nomeconteudo, List<Questao> listaquestoes)
        {
            List<string> queries = new List<string>();

            foreach (var item in listaquestoes) //Vai criar uma query para cada questao
            {
                queries.Add("INSERT INTO QUEST_PROV(Nome_Questao, Nome_Prova, CPF_Professor, Nome_Disciplina, Nome_Conteudo) VALUES('" + item.Nome_Questao + "', '" + nomeprova + "', '" + CPF + "', '" + nomedisciplina + "', '" + nomeconteudo + "')");
            }


            MySqlConnection conexaoMySQL = Conectar();
            if (conexaoMySQL != null)
            {
                conexaoMySQL.Open();

                foreach (var item in queries)
                {
                    MySqlCommand cmd = new MySqlCommand(item, conexaoMySQL);

                    cmd.ExecuteNonQuery();
                }

                conexaoMySQL.Close();

                return ("Questoes vinculadas à prova com sucesso!");                
            }

            return ("Não foi possível se conectar ao banco de dados! Tente novamente mais tarde!");
        }

        public string ExcluirProva(string CPF, string nomeprova, string disciplina, string conteudo)
        {
            string query = "DELETE FROM PROVA WHERE Nome_Prova='" + nomeprova + "' && Nome_Conteudo ='" + conteudo + "' && CPF_Professor='" + CPF + "' && Nome_Disciplina='" + disciplina + "'";

            try
            {
                MySqlConnection conexaoMySQL = Conectar();
                if (conexaoMySQL != null)
                {
                    conexaoMySQL.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexaoMySQL);

                    cmd.ExecuteNonQuery();

                    conexaoMySQL.Close();

                    return ("Prova excluída com sucesso!");
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

        public List<Questao> BuscarQuestoesProva(string CPF, string nomeprova, string disciplina, string conteudo)
        {           

            if (nomeprova != "")
            {
                List<Questao> lista = new List<Questao>();

                string nome = "";
                string dificuldade = "";
                string imagem = "";
                string enunciado = "";
                char resposta;

                string query = "SELECT Q.Nome_Questao, Q.Dificuldade, Q.Imagem, Q.Enunciado, Q.Resposta FROM QUEST_PROV QP " +
                                "INNER JOIN QUESTAO Q ON (QP.Nome_Questao = Q.Nome_Questao AND QP.Nome_Disciplina = Q.Nome_Disciplina " +
                                "AND QP.Nome_Conteudo = Q.Nome_Conteudo AND QP.CPF_Professor = Q.CPF_Professor) WHERE (QP.CPF_Professor = '"+CPF+"' " +
                                "&& QP.Nome_Disciplina = '"+disciplina+"' && QP.Nome_Conteudo = '"+conteudo+"' && QP.Nome_Prova = '"+nomeprova+"')";

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
            else
            {
                return (null);
            }

            

            
        }
    }
}
 