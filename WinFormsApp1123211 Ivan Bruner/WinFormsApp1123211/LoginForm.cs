using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WinFormsApp1123211
{
    public partial class LoginForm : Form
    {
        private readonly Database _database; 

        public LoginForm()
        {
            InitializeComponent();
            _database = new Database();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Įveskite naudotojo vardą ir slaptažodį!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                User user = AuthenticateUser(username, password);
                if (user != null)
                {
                    NavigateToRoleSpecificForm(user);
                }
                else
                {
                    MessageBox.Show("Neteisingi prisijungimo duomenys!", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Įvyko klaida: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private User AuthenticateUser(string username, string password)
        {
            string query = "SELECT naudotojo_id, vardas, vaidmuo FROM naudotojai WHERE prisijungimo_vardas = @username AND slaptazodis = @password";

            MySqlParameter[] parameters = {
                new MySqlParameter("@username", username),
                new MySqlParameter("@password", password)
            };

            DataTable userTable = _database.ExecuteQuery(query, parameters);

            if (userTable.Rows.Count > 0)
            {
                return new User
                {
                    UserId = Convert.ToInt32(userTable.Rows[0]["naudotojo_id"]),
                    Name = userTable.Rows[0]["vardas"].ToString(),
                    Role = userTable.Rows[0]["vaidmuo"].ToString()
                };
            }

            return null;
        }

        private void NavigateToRoleSpecificForm(User user)
        {
            this.Hide();

            Form nextForm = user.Role switch
            {
                "Administratorius" => new AdminForm(),
                "Vadybininkas" => new ManagerForm(user.UserId),
                "Gyventojas" => new ResidentForm(user.UserId),
                _ => null
            };

            if (nextForm != null)
            {
                nextForm.Show();
            }
            else
            {
                MessageBox.Show("Nepavyko nustatyti naudotojo vaidmens.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Show();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
