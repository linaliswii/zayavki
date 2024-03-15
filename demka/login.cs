using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Npgsql;


namespace demka
{
    public partial class login : Form
    {
        private readonly string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=demka";
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string role = "";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("SELECT роль FROM users WHERE логин = @логин AND пароль = @пароль", connection))
                {
                    cmd.Parameters.AddWithValue("@логин", username);
                    cmd.Parameters.AddWithValue("@пароль", password);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        role = result.ToString();
                    }
                }
            }

            if (role == "admin")
            {
                Form1 mainForm = new Form1();
                mainForm.Activate(); 
                this.Hide();
                mainForm.ShowDialog();
                this.Close();
            }
            else if (role == "romanova")
            {
                Client clientForm = new Client();
                clientForm.Activate(); 
                this.Hide();
                clientForm.ShowDialog();
                this.Close();
            }
            else if (role == "ispol")
            {
                IspolForm ispolForm = new IspolForm();
                ispolForm.Activate(); 
                this.Hide();
                ispolForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль");
            }

        }
    }
}


