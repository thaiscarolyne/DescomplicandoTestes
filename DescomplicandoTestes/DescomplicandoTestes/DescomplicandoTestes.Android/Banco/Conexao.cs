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
    }
}