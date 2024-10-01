using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CampeonatoFutebol
{
    internal class Jogador
    {

        public string Nome { get; set; }
        public string Posicao {  get; set; }
        public int Numero { get; set; }
        public int TimeID {  get; set; }


        public bool registrarJogador()
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(DatabaseInitializer.bancoServidorComDatabase))
                {
                    conexao.Open();
                
                    string insert = $"INSERT INTO Jogadores (nome, posicao, numero, time_id) VALUES ('{Nome}', '{Posicao}', {Numero}, {TimeID})";

                    MySqlCommand comando = new MySqlCommand(insert, conexao);
                    comando.ExecuteNonQuery();
                    conexao.Close();

                    MessageBox.Show($"O jogador {Nome} foi registrado! ");
                    return true;
                }   
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar no banco de dados - método registrarJogador" + ex.Message);
                return false;
            }


        }
   
    
    }
}
