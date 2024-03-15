using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace demka
{
    public partial class IspolForm : Form
    {
        private readonly string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=demka";

        public IspolForm()
        {
            InitializeComponent();
            LoadRequests();
        }

        private void LoadRequests()
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT Номер_заявки, Оборудование, Тип_неисправности, Статус FROM requests";
                    using (var cmd = new NpgsqlCommand(selectQuery, connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            dataGridView1.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки списка заявок: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string n = row.Cells["Номер_заявки"].Value.ToString();
                string ob = row.Cells["Оборудование"].Value.ToString();
                string type = row.Cells["Тип_неисправности"].Value.ToString();
                string status = row.Cells["Статус"].Value.ToString();

                textBoxRequestNumber.Text = n;
                textBoxEquipment.Text = ob;
                textBoxIssueType.Text = type;
                comboBoxStatus.Text = status;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                string номерЗаявки = dataGridView1.Rows[selectedRowIndex].Cells["Номер_заявки"].Value.ToString();

                string новыйСтатус = comboBoxStatus.Text;

                try
                {
                    using (var connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();
                        string updateQuery = "UPDATE requests SET Статус = @Статус WHERE Номер_заявки = @Номер_заявки";

                        using (var command = new NpgsqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Статус", новыйСтатус);
                            command.Parameters.AddWithValue("@Номер_заявки", номерЗаявки);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Изменения прошли успешно для заявки " + номерЗаявки);
                                LoadRequests();
                            }
                            else
                            {
                                MessageBox.Show("Не удалось обновить статус для заявки " + номерЗаявки);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка обновления статуса: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для изменения статуса.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            statistic mainForm = new statistic();
            this.Hide();
            mainForm.ShowDialog();
            this.Close();
        }

        private void IspolForm_Load(object sender, EventArgs e)
        {

        }
    }
}
