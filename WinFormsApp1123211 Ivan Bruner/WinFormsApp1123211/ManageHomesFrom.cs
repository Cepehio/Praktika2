using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WinFormsApp1123211
{
    public partial class ManageHomesForm : Form
    {
        private Database db = new Database();

        public ManageHomesForm()
        {
            InitializeComponent();
            RefreshData();
        }

        private void ManageHomesForm_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            LoadHomes();
            LoadServices();
        }

        private void LoadHomes()
        {
            try
            {
                string query = "SELECT namo_id, namo_pavadinimas FROM namai";
                DataTable homes = db.ExecuteQuery(query);

                cmbHomes.DataSource = homes;
                cmbHomes.DisplayMember = "namo_pavadinimas";
                cmbHomes.ValueMember = "namo_id";
                cmbHomes.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading homes: {ex.Message}");
            }
        }

        private void LoadServices()
        {
            try
            {
                string query = "SELECT paslaugos_id, paslaugos_pavadinimas FROM paslaugos";
                DataTable services = db.ExecuteQuery(query);

                cmbServices.DataSource = services;
                cmbServices.DisplayMember = "paslaugos_pavadinimas";
                cmbServices.ValueMember = "paslaugos_id";
                cmbServices.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading services: {ex.Message}");
            }
        }

        private void lblHomeName_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCreateHome_Click(object sender, EventArgs e)
        {
            string homeName = txtHomeName.Text.Trim();

            if (string.IsNullOrWhiteSpace(homeName))
            {
                MessageBox.Show("Please enter a home name.");
                return;
            }

            try
            {
                string query = "INSERT INTO namai (namo_pavadinimas) VALUES (@homeName)";
                MySqlParameter[] parameters = { new MySqlParameter("@homeName", homeName) };

                int rowsAffected = db.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Home successfully created.");
                    RefreshData();
                    txtHomeName.Clear();
                }
                else
                {
                    MessageBox.Show("Error creating home.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating home: {ex.Message}");
            }
        }

        private void btnDeleteHome_Click(object sender, EventArgs e)
        {
            if (cmbHomes.SelectedValue == null)
            {
                MessageBox.Show("Please select a home to delete.");
                return;
            }

            int homeId = Convert.ToInt32(cmbHomes.SelectedValue);

            try
            {
                string query = "DELETE FROM namai WHERE namo_id = @homeId";
                MySqlParameter[] parameters = { new MySqlParameter("@homeId", homeId) };

                int rowsAffected = db.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Home successfully deleted.");
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Error deleting home.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting home: {ex.Message}");
            }
        }

        private void btnCreateService_Click(object sender, EventArgs e)
        {
            string serviceName = txtNewServiceName.Text.Trim();

            if (string.IsNullOrWhiteSpace(serviceName))
            {
                MessageBox.Show("Please enter a service name.");
                return;
            }

            try
            {
                string query = "INSERT INTO paslaugos (paslaugos_pavadinimas) VALUES (@serviceName)";
                MySqlParameter[] parameters = { new MySqlParameter("@serviceName", serviceName) };

                int rowsAffected = db.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Service successfully created.");
                    RefreshData();
                    txtNewServiceName.Clear();
                }
                else
                {
                    MessageBox.Show("Error creating service.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating service: {ex.Message}");
            }
        }

        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            if (cmbServices.SelectedValue == null)
            {
                MessageBox.Show("Please select a service to delete.");
                return;
            }

            int serviceId = Convert.ToInt32(cmbServices.SelectedValue);

            try
            {
                string checkQuery = "SELECT COUNT(*) FROM kainos WHERE paslaugos_id = @serviceId";
                MySqlParameter[] checkParams = { new MySqlParameter("@serviceId", serviceId) };
                int associationCount = Convert.ToInt32(db.ExecuteScalar(checkQuery, checkParams));

                if (associationCount > 0)
                {
                    MessageBox.Show("This service is assigned to a home. Remove the association first.");
                    return;
                }

                string deleteQuery = "DELETE FROM paslaugos WHERE paslaugos_id = @serviceId";
                MySqlParameter[] deleteParams = { new MySqlParameter("@serviceId", serviceId) };

                int rowsAffected = db.ExecuteNonQuery(deleteQuery, deleteParams);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Service successfully deleted.");
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Error deleting service.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting service: {ex.Message}");
            }
        }

        private void btnAssignServiceToHome_Click(object sender, EventArgs e)
        {
            if (cmbHomes.SelectedValue == null || cmbServices.SelectedValue == null)
            {
                MessageBox.Show("Please select a home and a service.");
                return;
            }

            int homeId = Convert.ToInt32(cmbHomes.SelectedValue);
            int serviceId = Convert.ToInt32(cmbServices.SelectedValue);

            if (!decimal.TryParse(txtServicePrice.Text, out decimal price))
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }

            try
            {
                string checkQuery = "SELECT COUNT(*) FROM kainos WHERE namo_id = @homeId AND paslaugos_id = @serviceId";
                MySqlParameter[] checkParams = {
                    new MySqlParameter("@homeId", homeId),
                    new MySqlParameter("@serviceId", serviceId)
                };
                int associationCount = Convert.ToInt32(db.ExecuteScalar(checkQuery, checkParams));

                if (associationCount > 0)
                {
                    MessageBox.Show("This service is already assigned to the selected home.");
                    return;
                }

                string insertQuery = "INSERT INTO kainos (namo_id, paslaugos_id, kaina) VALUES (@homeId, @serviceId, @price)";
                MySqlParameter[] insertParams = {
                    new MySqlParameter("@homeId", homeId),
                    new MySqlParameter("@serviceId", serviceId),
                    new MySqlParameter("@price", price)
                };

                int rowsAffected = db.ExecuteNonQuery(insertQuery, insertParams);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Service successfully assigned to the home with a price.");
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Error assigning service to the home.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error assigning service: {ex.Message}");
            }
        }

        private void btnRemoveServiceFromHome_Click(object sender, EventArgs e)
        {
            if (cmbHomes.SelectedValue == null || cmbServices.SelectedValue == null)
            {
                MessageBox.Show("Please select a home and a service.");
                return;
            }

            int homeId = Convert.ToInt32(cmbHomes.SelectedValue);
            int serviceId = Convert.ToInt32(cmbServices.SelectedValue);

            try
            {
                string query = "DELETE FROM kainos WHERE namo_id = @homeId AND paslaugos_id = @serviceId";
                MySqlParameter[] parameters = {
                    new MySqlParameter("@homeId", homeId),
                    new MySqlParameter("@serviceId", serviceId)
                };

                int rowsAffected = db.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Service successfully removed from the home.");
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Error removing the service.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnUpdatePrice_Click(object sender, EventArgs e)
        {
            if (cmbHomes.SelectedValue == null || cmbServices.SelectedValue == null)
            {
                MessageBox.Show("Please select a home and a service.");
                return;
            }

            int homeId = Convert.ToInt32(cmbHomes.SelectedValue);
            int serviceId = Convert.ToInt32(cmbServices.SelectedValue);

            if (!decimal.TryParse(txtServicePrice.Text, out decimal newPrice))
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }

            try
            {
                string query = "UPDATE kainos SET kaina = @newPrice WHERE namo_id = @homeId AND paslaugos_id = @serviceId";
                MySqlParameter[] parameters = {
                    new MySqlParameter("@newPrice", newPrice),
                    new MySqlParameter("@homeId", homeId),
                    new MySqlParameter("@serviceId", serviceId)
                };

                int rowsAffected = db.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Price successfully updated.");
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Error updating price.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating price: {ex.Message}");
            }
        }

        private void btnDeleteHome_Click_1(object sender, EventArgs e)
        {
            btnDeleteHome_Click(sender, e);
        }

        private void btnCreateService_Click_1(object sender, EventArgs e)
        {
            btnCreateService_Click(sender, e);
        }

        private void btnDeleteService_Click_1(object sender, EventArgs e)
        {
            btnDeleteService_Click(sender, e);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminForm adminForm = new AdminForm();
            adminForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}