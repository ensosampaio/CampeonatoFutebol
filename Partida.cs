using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CampeonatoFutebol
{
    internal class Partida
    {
        public DateTime Data { get; set; }
        public TimeSpan Horario { get; set; }
        public int TimeCasaId { get; set; } // Chave estrangeira referenciando o Time da casa
        public int TimeVisitanteId { get; set; } // Chave estrangeira referenciando o Time visitante
        public int PlacarCasa { get; set; }
        public int PlacarVisitante { get; set; }

        public bool RegistrarPartida()
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(DatabaseInitializer.bancoServidorComDatabase))
                {
                    conexao.Open();
                    string insert = $"INSERT INTO Partidas (data, horario, time_casa_id, time_visitante_id, placar_casa, placar_visitante) " +
                                    $"VALUES ('{Data:yyyy-MM-dd}', '{Horario}', {TimeCasaId}, {TimeVisitanteId}, {PlacarCasa}, {PlacarVisitante})";
                    MySqlCommand comando = new MySqlCommand(insert, conexao);
                    comando.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no banco de dados - método RegistrarPartida: " + ex.Message);
                return false;
            }
        }




    }
}
