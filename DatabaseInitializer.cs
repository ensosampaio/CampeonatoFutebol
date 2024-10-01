using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CampeonatoFutebol
{
    internal class DatabaseInitializer
    {
        private const string servidor = "localhost";
        private const string usuario = "root";
        private const string senha = "ensosampaio1";

   
        static public string bancoServidorSemDatabase = $"server={servidor};user id={usuario};password={senha}";

       
        static public string bancoServidorComDatabase;

        public static void CriarDatabase(string nomeDatabase)
        {
            // Conexão inicial sem o banco de dados
            using (MySqlConnection conexao = new MySqlConnection(bancoServidorSemDatabase))
            {
                try
                {
                    conexao.Open();
                    string createDatabaseQuery = $"CREATE DATABASE IF NOT EXISTS `{nomeDatabase}`;";
                    MySqlCommand command = new MySqlCommand(createDatabaseQuery, conexao);
                    command.ExecuteNonQuery();
                    MessageBox.Show($"Database '{nomeDatabase}' criada ou já existente.");

                    // Configurar a string de conexão com o banco de dados criado
                    bancoServidorComDatabase = $"server={servidor};user id={usuario};password={senha};database={nomeDatabase}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao criar o banco de dados: " + ex.Message);
                }
            }
        }



        public static void CriarTabelas()
        {
            using (MySqlConnection conexao = new MySqlConnection($"{bancoServidorComDatabase}"))
            {
                try
                {
                    conexao.Open();
                    string createTablesQuery = @"
                        CREATE TABLE IF NOT EXISTS Times (
                            id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                            nome VARCHAR(100) NOT NULL,
                            cidade VARCHAR(100) NOT NULL,
                            estado VARCHAR(100) NOT NULL
                        );

                        CREATE TABLE IF NOT EXISTS Jogadores (
                            id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                            nome VARCHAR(100) NOT NULL,
                            posicao VARCHAR(50) NOT NULL,
                            numero INT NOT NULL,
                            time_id INT NOT NULL,
                            FOREIGN KEY (time_id) REFERENCES Times(id)
                        );

                        CREATE TABLE IF NOT EXISTS Estadios (
                            id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                            nome VARCHAR(100) NOT NULL,
                            cidade VARCHAR(100) NOT NULL,
                            estado VARCHAR(100) NOT NULL
                        );

                        CREATE TABLE IF NOT EXISTS Partidas (
                            id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                            data DATE NOT NULL,
                            horario TIME NOT NULL,
                            time_casa_id INT NOT NULL,
                            time_visitante_id INT NOT NULL,
                            estadio_id INT NOT NULL,
                            gols_time_casa INT NOT NULL,
                            gols_time_visitante INT NOT NULL,
                            FOREIGN KEY (time_casa_id) REFERENCES Times(id),
                            FOREIGN KEY (time_visitante_id) REFERENCES Times(id),
                            FOREIGN KEY (estadio_id) REFERENCES Estadios(id)
                        );

                        CREATE TABLE IF NOT EXISTS Classificacao (
                            time_id INT NOT NULL PRIMARY KEY,
                            pontos INT NOT NULL,
                            jogos INT NOT NULL,
                            vitorias INT NOT NULL,
                            empates INT NOT NULL,
                            derrotas INT NOT NULL,
                            gols_pro INT NOT NULL,
                            gols_contra INT NOT NULL,
                            FOREIGN KEY (time_id) REFERENCES Times(id)
                        );

                        CREATE TABLE IF NOT EXISTS Gols (
                            id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                            partida_id INT NOT NULL,
                            jogador_id INT NOT NULL,
                            time_id INT NOT NULL,
                            minuto INT NOT NULL,
                            FOREIGN KEY (partida_id) REFERENCES Partidas(id),
                            FOREIGN KEY (jogador_id) REFERENCES Jogadores(id),
                            FOREIGN KEY (time_id) REFERENCES Times(id)
                        );";

                    MySqlCommand comando = new MySqlCommand(createTablesQuery, conexao);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Tabelas criadas ou já existentes.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao criar tabelas: " + ex.Message);
                }
            }
        }

    }
}
