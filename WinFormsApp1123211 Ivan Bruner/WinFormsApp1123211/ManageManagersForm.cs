using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WinFormsApp1123211
{
    public partial class ManageManagersForm : Form
    {
        private Database db = new Database();

        public ManageManagersForm()
        {
            InitializeComponent();
            LoadHomes();
            LoadManagers();
        }

        private void LoadHomes()
        {
            try
            {
                string query = "SELECT * FROM namai";
                DataTable homes = db.ExecuteQuery(query);

                cmbHomes.DataSource = homes;
                cmbHomes.DisplayMember = "namo_pavadinimas";
                cmbHomes.ValueMember = "namo_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Klaida įkeliant namus: {ex.Message}");
            }
        }

        private void LoadManagers()
        {
            try
            {
                string query = "SELECT * FROM naudotojai WHERE vaidmuo = 'Vadybininkas'";
                DataTable managers = db.ExecuteQuery(query);

                cmbManagers.DataSource = managers;
                cmbManagers.DisplayMember = "vardas";
                cmbManagers.ValueMember = "naudotojo_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Klaida įkeliant vadybininkus: {ex.Message}");
            }
        }

        private void btnAssignManager_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbHomes.SelectedValue == null || cmbManagers.SelectedValue == null)
                {
                    MessageBox.Show("Pasirinkite namą ir vadybininką!");
                    return;
                }

                int managerId = Convert.ToInt32(cmbManagers.SelectedValue);
                int homeId = Convert.ToInt32(cmbHomes.SelectedValue);

                string query = @"UPDATE namai SET vadybininko_id = @managerId WHERE namo_id = @homeId";
                MySqlParameter[] parameters = {
                    new MySqlParameter("@homeId", homeId),
                    new MySqlParameter("@managerId", managerId)
                };

                int rowsAffected = db.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Vadybininkas sėkmingai priskirtas prie namo.");
                    LoadHomes();
                }
                else
                {
                    MessageBox.Show("Nepavyko priskirti vadybininko.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Klaida priskiriant vadybininką: {ex.Message}");
            }
        }

        private void btnRemoveManager_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbHomes.SelectedValue == null || cmbManagers.SelectedValue == null)
                {
                    MessageBox.Show("Pasirinkite namą ir vadybininką!");
                    return;
                }

                int managerId = Convert.ToInt32(cmbManagers.SelectedValue);
                int homeId = Convert.ToInt32(cmbHomes.SelectedValue);

                string query = @"UPDATE namai SET vadybininko_id = NULL WHERE namo_id = @homeId AND vadybininko_id = @managerId";
                MySqlParameter[] parameters = {
                    new MySqlParameter("@homeId", homeId),
                    new MySqlParameter("@managerId", managerId)
                };

                int rowsAffected = db.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Vadybininkas pašalintas iš namo.");
                    LoadHomes();
                }
                else
                {
                    MessageBox.Show("Nepavyko pašalinti vadybininko.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Klaida šalinant vadybininką: {ex.Message}");
            }
        }

        private void btnCreateManager_Click(object sender, EventArgs e)
        {
            try
            {
                string managerName = txtNewManagerName.Text.Trim();
                string password = txtNewManagerPassword.Text.Trim();

                if (string.IsNullOrWhiteSpace(managerName) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Prašome įvesti vardą ir slaptažodį.");
                    return;
                }

                string query = @"INSERT INTO naudotojai (vardas, prisijungimo_vardas, slaptazodis, vaidmuo) 
                                 VALUES (@name, @username, @password, 'Vadybininkas')";

                MySqlParameter[] parameters = {
                    new MySqlParameter("@name", managerName),
                    new MySqlParameter("@username", managerName),
                    new MySqlParameter("@password", password)
                };

                int rowsAffected = db.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Naujas vadybininkas sukurtas.");
                    LoadManagers(); 
                }
                else
                {
                    MessageBox.Show("Įvyko klaida kuriant vadybininką.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Klaida kuriant vadybininką: {ex.Message}");
            }
        }

        private void btnDeleteManager_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbManagers.SelectedValue == null)
                {
                    MessageBox.Show("Pasirinkite vadybininką!");
                    return;
                }

                int managerId = Convert.ToInt32(cmbManagers.SelectedValue);

                string query = "DELETE FROM naudotojai WHERE naudotojo_id = @managerId AND vaidmuo = 'Vadybininkas'";

                MySqlParameter[] parameters = {
                    new MySqlParameter("@managerId", managerId)
                };

                int rowsAffected = db.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Vadybininkas pašalintas iš sistemos.");
                    LoadManagers(); 
                }
                else
                {
                    MessageBox.Show("Nepavyko pašalinti vadybininko.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Klaida šalinant vadybininką: {ex.Message}");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
