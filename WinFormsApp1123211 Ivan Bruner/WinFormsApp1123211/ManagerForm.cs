using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WinFormsApp1123211
{
    public partial class ManagerForm : Form
    {
        private readonly int _userId; 
        private readonly Database _db;

        public ManagerForm(int userId) : this(userId, new Database()) { }

        public ManagerForm(int userId, Database db)
        {
            InitializeComponent();
            _userId = userId;
            _db = db ?? throw new ArgumentNullException(nameof(db));

            InitializeManagerData();
        }

        private void InitializeManagerData()
        {
            LoadAssignedHome();
            LoadServices();
        }

        private void LoadAssignedHome()
        {
            try
            {
                string homeName = GetAssignedHome();
                lblAssignedHome.Text = !string.IsNullOrEmpty(homeName)
                    ? $"Paskirtas namas: {homeName}"
                    : "Jums nepriskirtas joks namas.";
            }
            catch (Exception ex)
            {
                DisplayError("Klaida įkeliant priskirtą namą", ex);
            }
        }

        private string GetAssignedHome()
        {
            const string query = @"
                SELECT namo_pavadinimas 
                FROM namai 
                WHERE vadybininko_id = @userId";

            MySqlParameter[] parameters = { new MySqlParameter("@userId", _userId) };

            DataTable result = _db.ExecuteQuery(query, parameters);
            return result.Rows.Count > 0 ? result.Rows[0]["namo_pavadinimas"].ToString() : string.Empty;
        }

        private void LoadServices()
        {
            try
            {
                DataTable services = GetServices();
                BindServicesToComboBox(services);
            }
            catch (Exception ex)
            {
                DisplayError("Klaida įkeliant paslaugas", ex);
            }
        }

        private DataTable GetServices()
        {
            const string query = @"
                SELECT p.paslaugos_id, p.paslaugos_pavadinimas
                FROM paslaugos p
                JOIN kainos k ON p.paslaugos_id = k.paslaugos_id
                JOIN namai n ON k.namo_id = n.namo_id
                WHERE n.vadybininko_id = @userId";

            MySqlParameter[] parameters = { new MySqlParameter("@userId", _userId) };
            return _db.ExecuteQuery(query, parameters);
        }

        private void BindServicesToComboBox(DataTable services)
        {
            cmbServices.DataSource = services;
            cmbServices.DisplayMember = "paslaugos_pavadinimas";
            cmbServices.ValueMember = "paslaugos_id";
            cmbServices.SelectedIndex = -1; 
        }

        private void btnUpdatePrice_Click(object sender, EventArgs e)
        {
            if (cmbServices.SelectedValue == null || string.IsNullOrWhiteSpace(txtServicePrice.Text))
            {
                MessageBox.Show("Prašome pasirinkti paslaugą ir įvesti kainą.");
                return;
            }

            if (!decimal.TryParse(txtServicePrice.Text, out decimal price))
            {
                MessageBox.Show("Prašome įvesti tinkamą kainą.");
                return;
            }

            try
            {
                UpdateServicePrice(price);
                MessageBox.Show("Kaina sėkmingai atnaujinta.");
            }
            catch (Exception ex)
            {
                DisplayError("Klaida keičiant kainą", ex);
            }
        }

        private void UpdateServicePrice(decimal price)
        {
            const string query = @"
                UPDATE kainos
                SET kaina = @price
                WHERE paslaugos_id = @serviceId
                AND namo_id = (
                    SELECT namo_id 
                    FROM namai 
                    WHERE vadybininko_id = @userId
                )";

            MySqlParameter[] parameters = {
                new MySqlParameter("@price", price),
                new MySqlParameter("@serviceId", cmbServices.SelectedValue),
                new MySqlParameter("@userId", _userId)
            };

            int rowsAffected = _db.ExecuteNonQuery(query, parameters);
            if (rowsAffected <= 0)
            {
                throw new Exception("Nepavyko atnaujinti kainos.");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ConfirmAndExit();
        }

        private void ConfirmAndExit()
        {
            var result = MessageBox.Show(
                "Ar tikrai norite išeiti iš programos?",
                "Patvirtinti išėjimą",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void DisplayError(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
