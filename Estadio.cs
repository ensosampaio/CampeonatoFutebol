using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CampeonatoFutebol
{
    internal class Estadio
    {
        public string Nome {  get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }


        public bool registrarEstadio()
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(DatabaseInitializer.bancoServidorComDatabase))
                {
                    conexao.Open();

                    string insert = $"INSERT INTO Estadios (nome, cidade, estado) VALUES ('{Nome}', '{Cidade}', '{Estado}')";

                    MySqlCommand comando = new MySqlCommand(insert, conexao);
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    MessageBox.Show($"O estádio {Nome} foi registrado com sucesso!");
                    return true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no banco de dados - método registrarEstadio" + ex.Message);
                return false;
            }
        }
    
    }
}
