using MySql.Data.MySqlClient;
using Mysqlx;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CampeonatoFutebol
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DatabaseInitializer.CriarDatabase("Campeonato_de_Futebol");
            DatabaseInitializer.CriarTabelas();
            CarregarTimes();
            CarregarEstadios();
        }

        // Método para carregar times na ListBox e ComboBox
        private void CarregarTimes()
        {
            listTimes.Items.Clear();
            cmbTimes.Items.Clear();
            cmbTimeCasa.Items.Clear();
            cmbTimeVisitante.Items.Clear();

            try
            {
                using (MySqlConnection conexao = new MySqlConnection(DatabaseInitializer.bancoServidorComDatabase))
                {
                    conexao.Open();
                    string query = "SELECT id, nome FROM Times";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        listTimes.Items.Add(reader["nome"].ToString());
                        cmbTimes.Items.Add(new ComboBoxItem { Content = reader["nome"], Tag = reader["id"] });
                        cmbTimeCasa.Items.Add(new ComboBoxItem { Content = reader["nome"], Tag = reader["id"] });
                        cmbTimeVisitante.Items.Add(new ComboBoxItem { Content = reader["nome"], Tag = reader["id"] });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar times: " + ex.Message);
            }
        }

        // Método para carregar estádios
        private void CarregarEstadios()
        {
            cmbEstadios.Items.Clear();

            try
            {
                using (MySqlConnection conexao = new MySqlConnection(DatabaseInitializer.bancoServidorComDatabase))
                {
                    conexao.Open();
                    string query = "SELECT id, nome FROM Estadios";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        cmbEstadios.Items.Add(new ComboBoxItem { Content = reader["nome"], Tag = reader["id"] });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar estádios: " + ex.Message);
            }
        }

        private void btnCadastrarTime_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!txtNomeTime.Text.Equals("") && !txtEstadoTime.Text.Equals("") && !txtCidadeTime.Text.Equals(""))
                {
                    Time time = new Time();
                    time.nome = txtNomeTime.Text;
                    time.estado = txtEstadoTime.Text;
                    time.cidade = txtCidadeTime.Text;
                   

                    if (time.RegistrarTime())
                    {
                        MessageBox.Show("Time cadastrado com sucesso!");
                        txtCidadeTime.Clear();
                        txtEstadoTime.Clear();
                        txtNomeTime.Clear();
                        CarregarTimes();
                    }
                    else
                    {
                        MessageBox.Show("Nao foi possivel cadastrar time");
                    }

                }
                else
                {
                    MessageBox.Show("Favor preencher todos os campos!");
                    txtCidadeTime.Clear();
                    txtEstadoTime.Clear();
                    txtNomeTime.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar time " + ex.Message);
            }
        }

        private void btnCadastrarJogador_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!txtNomeJogador.Text.Equals("") && !txtNumeroJogador.Text.Equals("") && !txtPosicaoJogador.Text.Equals("") && cmbTimes.SelectedItem != null)
                {
                    int timeId = (int)((ComboBoxItem)cmbTimes.SelectedItem).Tag; ; //puxa do CarregarTimes()
                    Jogador jogador = new Jogador();
                    jogador.Nome = txtNomeJogador.Text;
                    jogador.Posicao = txtPosicaoJogador.Text;
                    jogador.Numero = int.Parse(txtNumeroJogador.Text);
                    jogador.TimeID = timeId; 

                    if (jogador.registrarJogador())
                    {
                        MessageBox.Show("Jogador cadastrado com sucesso!");
                        txtNomeJogador.Clear();
                        txtNumeroJogador.Clear();
                        txtPosicaoJogador.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível cadastrar jogador");
                    }

                }
                else
                {
                    MessageBox.Show("Favor preencher todos os campos!");
                    txtNomeJogador.Clear();
                    txtNumeroJogador.Clear();
                    txtPosicaoJogador.Clear();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Erro ao cadastrar jogador " + ex.Message);
            }

        
        }

        private void btnCadastrarEstadio_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!txtNomeEstadio.Text.Equals("") && !txtCidadeEstadio.Text.Equals("") && !txtEstadoEstadio.Text.Equals(""))
                {
                    Estadio estadio = new Estadio();
                    estadio.Estado = txtEstadoEstadio.Text;
                    estadio.Cidade = txtCidadeEstadio.Text;
                    estadio.Nome = txtNomeEstadio.Text;
                    if (estadio.registrarEstadio())
                    {
                        MessageBox.Show("Estadio cadastrado com sucesso!");
                        txtEstadoEstadio.Clear();
                        txtCidadeEstadio.Clear();
                        txtNomeEstadio.Clear();
                        CarregarEstadios();
                    }
                    else
                    {
                        MessageBox.Show("Favor preencher todos os campos!");
                        txtEstadoEstadio.Clear();
                        txtCidadeEstadio.Clear();
                        txtNomeEstadio.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar o estádio " + ex.Message);
            }
                
        
        }


    }
}



