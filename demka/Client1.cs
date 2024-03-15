using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demka
{
    public partial class Client1 : Form
    {
        public Client1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string equipmentName = textBoxEquipment.Text;
            string issueType = textBoxIssueType.Text;
            string issueDescription = textBoxIssueDescription.Text;
            DateTime requestDate = DateTime.Now;

            try
            {
                string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=demka";

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO requests (Оборудование, Тип_неисправности, Описание_проблемы, Дата_заявки) VALUES (@Оборудование, @Тип_неисправности, @Описание_проблемы, @Дата_заявки)";

                    using (var cmd = new NpgsqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@Оборудование", equipmentName);
                        cmd.Parameters.AddWithValue("@Тип_неисправности", issueType);
                        cmd.Parameters.AddWithValue("@Описание_проблемы", issueDescription);
                        cmd.Parameters.AddWithValue("@Дата_заявки", requestDate);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Заявка успешно подана.");
                            ClearFormFields();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при подаче заявки. Пожалуйста, попробуйте еще раз.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении заявки: " + ex.Message);
            }
        }

        private void ClearFormFields()
        {
            textBoxEquipment.Text = "";
            textBoxIssueType.Text = "";
            textBoxIssueDescription.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Client mainForm = new Client();
            mainForm.Activate();
            this.Hide();
            mainForm.ShowDialog();
            this.Close();
        }
    }
}
