using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CampeonatoFutebol
{
    internal class Classificacao
    {
        public int TimeId { get; set; } // Chave estrangeira referenciando o time
        public int Pontos { get; set; }
        public int Vitorias { get; set; }
        public int Empates { get; set; }
        public int Derrotas { get; set; }

        public bool RegistrarClassificacao()
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(DatabaseInitializer.bancoServidorComDatabase))
                {
                    conexao.Open();
                    string insert = $"INSERT INTO Classificacao (time_id, pontos, vitorias, empates, derrotas) VALUES ({TimeId}, {Pontos}, {Vitorias}, {Empates}, {Derrotas})";
                    MySqlCommand comando = new MySqlCommand(insert, conexao);
                    comando.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no banco de dados - método RegistrarClassificacao: " + ex.Message);
                return false;
            }
        }
    }
}
