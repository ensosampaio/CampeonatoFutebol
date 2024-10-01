using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CampeonatoFutebol
{
    internal class Gol
    {
        public int PartidaId { get; set; } // Chave estrangeira referenciando a partida
        public int JogadorId { get; set; } // Chave estrangeira referenciando o jogador
        public int Minuto { get; set; } // Minuto do gol

        public bool RegistrarGol()
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(DatabaseInitializer.bancoServidorComDatabase))
                {
                    conexao.Open();
                    string insert = $"INSERT INTO Gols (partida_id, jogador_id, minuto) VALUES ({PartidaId}, {JogadorId}, {Minuto})";
                    MySqlCommand comando = new MySqlCommand(insert, conexao);
                    comando.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no banco de dados - método RegistrarGol: " + ex.Message);
                return false;
            }
        }

    }
}
