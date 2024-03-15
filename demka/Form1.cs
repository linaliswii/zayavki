using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Npgsql;

namespace demka
{
    public partial class Form1 : Form
    {
        private readonly string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=demka";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadRequests();
        }

        private void LoadRequests()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM requests", connection))
                {
                    var dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO requests (Номер_заявки, Дата_заявки, Оборудование, Тип_неисправности, Описание_проблемы, Клиент, Статус, Ответственный) " +
                                                   "VALUES (@Номер_заявки, @Дата_заявки, @Оборудование, @Тип_неисправности, @Описание_проблемы, @Клиент, @Статус, @Ответственный)", connection))
                {
                    cmd.Parameters.AddWithValue("Номер_заявки", textBoxRequestNumber.Text);
                    cmd.Parameters.AddWithValue("Дата_заявки", DateTime.Now);
                    cmd.Parameters.AddWithValue("Оборудование", textBoxEquipment.Text);
                    cmd.Parameters.AddWithValue("Тип_неисправности", textBoxIssueType.Text);
                    cmd.Parameters.AddWithValue("Описание_проблемы", textBoxDescription.Text);
                    cmd.Parameters.AddWithValue("Клиент", textBoxClient.Text);
                    cmd.Parameters.AddWithValue("Статус", comboBoxStatus.Text);
                    cmd.Parameters.AddWithValue("Ответственный", textBox1.Text);

                    cmd.ExecuteNonQuery();
                }
            }

        LoadRequests();
    }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBoxRequestNumber.Text = row.Cells["Номер_заявки"].Value.ToString();
                textBoxEquipment.Text = row.Cells["Оборудование"].Value.ToString();
                textBoxIssueType.Text = row.Cells["Тип_неисправности"].Value.ToString();
                textBoxDescription.Text = row.Cells["Описание_проблемы"].Value.ToString();
                textBoxClient.Text = row.Cells["Клиент"].Value.ToString();
                comboBoxStatus.Text = row.Cells["Статус"].Value.ToString();
                textBox1.Text = row.Cells["Ответственный"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("UPDATE requests SET Статус = @Статус, Описание_проблемы = @Описание_проблемы, Ответственный = @Ответственный  WHERE Номер_заявки = @Номер_заявки", connection))
                {
                    cmd.Parameters.AddWithValue("Статус", comboBoxStatus.Text);
                    cmd.Parameters.AddWithValue("Описание_проблемы", textBoxDescription.Text);
                    cmd.Parameters.AddWithValue("Номер_заявки", textBoxRequestNumber.Text);
                    cmd.Parameters.AddWithValue("Ответственный", textBox1.Text);

                    cmd.ExecuteNonQuery();
                }
            }

            LoadRequests();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM requests WHERE Номер_заявки LIKE @searchText OR Оборудование LIKE @searchText OR Тип_неисправности LIKE @searchText", connection))
                {
                    cmd.Parameters.AddWithValue("searchText", $"%{textBox6.Text}%");

                    var dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

