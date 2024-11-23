using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WinFormsApp1123211
{
    public partial class ResidentForm : Form
    {
        private readonly int _userId;
        private readonly Database _db;

        public ResidentForm(int userId) : this(userId, new Database())
        {
        }

        public ResidentForm(int userId, Database db)
        {
            InitializeComponent();
            _userId = userId;
            _db = db ?? throw new ArgumentNullException(nameof(db));
            LoadResidentData();
        }

        private void LoadResidentData()
        {
            try
            {
                string assignedHome = GetAssignedHome();
                lblAssignedHome.Text = !string.IsNullOrEmpty(assignedHome)
                    ? $"Jūsų namas: {assignedHome}"
                    : "Jūs neturite priskirto namo.";

                DataTable servicesTable = GetServicesForAssignedHome();
                dgvServices.DataSource = servicesTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Klaida kraunant duomenis: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetAssignedHome()
        {
            const string query = @"
                SELECT namai.namo_pavadinimas 
                FROM namai 
                INNER JOIN namo_gyventojai ON namai.namo_id = namo_gyventojai.namo_id 
                WHERE namo_gyventojai.naudotojo_id = @userId";

            MySqlParameter[] parameters = {
                new MySqlParameter("@userId", _userId)
            };

            DataTable resultTable = _db.ExecuteQuery(query, parameters);
            return resultTable.Rows.Count > 0 ? resultTable.Rows[0]["namo_pavadinimas"].ToString() : string.Empty;
        }

        private DataTable GetServicesForAssignedHome()
        {
            const string query = @"
                SELECT paslaugos.paslaugos_pavadinimas AS 'Paslauga', kainos.kaina AS 'Kaina'
                FROM kainos
                INNER JOIN paslaugos ON kainos.paslaugos_id = paslaugos.paslaugos_id
                INNER JOIN namai ON kainos.namo_id = namai.namo_id
                WHERE namai.namo_id = (
                    SELECT namo_id 
                    FROM namo_gyventojai 
                    WHERE naudotojo_id = @userId
                )";

            MySqlParameter[] parameters = {
                new MySqlParameter("@userId", _userId)
            };

            return _db.ExecuteQuery(query, parameters);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ConfirmExit();
        }

        private void ConfirmExit()
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
    }
}
