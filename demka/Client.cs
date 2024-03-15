using Npgsql;
using System;
using System.Windows.Forms;

namespace demka
{
    public partial class Client : Form
    {
        private readonly string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=demka";

        public Client()
        {
            InitializeComponent();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            // Установка заголовков столбцов для ListView
            listView1.Columns.Add("Название оборудования", 150);
            listView1.Columns.Add("Тип неисправности", 120);
            listView1.Columns.Add("Дата", 100);
            listView1.Columns.Add("Статус", 80);

            // Отображение заявок клиента
            DisplayClientRequests();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Переход на другую форму при нажатии кнопки
            Client1 mainForm = new Client1();
            this.Hide();
            mainForm.ShowDialog();
            this.Close();
        }

        private void DisplayClientRequests()
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM requests WHERE Клиент = 'Романова'";

                    using (var cmd = new NpgsqlCommand(selectQuery, connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                // Если у клиента нет заявок, скрыть ListView и отобразить сообщение
                                listView1.Visible = false;
                                lblNoRequests.Visible = true;
                                lblNoRequests.Text = "У вас нет заявок.";
                            }
                            else
                            {
                                // Очистить ListView перед загрузкой заявок
                                listView1.Items.Clear();
                                while (reader.Read())
                                {
                                    // Добавить заявки в ListView
                                    ListViewItem item = new ListViewItem(reader["Оборудование"].ToString());
                                    item.SubItems.Add(reader["Тип_неисправности"].ToString());
                                    item.SubItems.Add(reader["Дата_заявки"].ToString());
                                    item.SubItems.Add(reader["Статус"].ToString());
                                    listView1.Items.Add(item);
                                }
                                // Отобразить ListView и скрыть сообщение
                                listView1.Visible = true;
                                lblNoRequests.Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке заявок клиента: " + ex.Message);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
