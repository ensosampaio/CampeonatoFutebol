using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CampeonatoFutebol
{
    internal class Time
    {
        public string nome {  get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }

        public bool RegistrarTime()
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(DatabaseInitializer.bancoServidorComDatabase);
                conexao.Open();

                string insert = $"INSERT INTO Times (nome, cidade, estado) VALUES ('{nome}', '{cidade}', '{estado}')";

                MySqlCommand comando = new MySqlCommand(insert, conexao);
                comando.ExecuteNonQuery();
                conexao.Close();

                MessageBox.Show($"O time {nome} foi cadastrado com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no banco de dados - método registrarTime: {ex.Message}");
                return false;
            }
        }

    }
}
