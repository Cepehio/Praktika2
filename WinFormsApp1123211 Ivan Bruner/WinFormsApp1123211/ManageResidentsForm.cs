using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WinFormsApp1123211
{
    public partial class ManageResidentsForm : Form
    {
        private Database db = new Database();

        public ManageResidentsForm()
        {
            InitializeComponent();
            LoadHomes();
            LoadResidents();
        }

        private void LoadHomes()
        {
            try
            {
                cmbHomes.Items.Clear();
                string query = "SELECT namo_id, namo_pavadinimas FROM namai";
                DataTable dataTable = db.ExecuteQuery(query);
                foreach (DataRow row in dataTable.Rows)
                {
                    cmbHomes.Items.Add(new ComboBoxItem(row["namo_pavadinimas"].ToString(), Convert.ToInt32(row["namo_id"])));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Klaida įkeliant namus: {ex.Message}");
            }
        }

        private void LoadResidents()
        {
            try
            {
                cmbResidents.Items.Clear();
                string query = "SELECT naudotojo_id, vardas FROM naudotojai WHERE vaidmuo = 'Gyventojas'";
                DataTable dataTable = db.ExecuteQuery(query);
                foreach (DataRow row in dataTable.Rows)
                {
                    cmbResidents.Items.Add(new ComboBoxItem(row["vardas"].ToString(), Convert.ToInt32(row["naudotojo_id"])));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Klaida įkeliant gyventojus: {ex.Message}");
            }
        }

        private void btnCreateResident_Click(object sender, EventArgs e)
        {
            string firstName = txtResidentName.Text.Trim(); 
            string lastName = txtResidentLogin.Text.Trim(); 

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Įveskite gyventojo vardą ir pavardę!");
                return;
            }

            try
            {
                string loginName = firstName; 
                string password = lastName; 

                string query = @"
                    INSERT INTO naudotojai (vardas, prisijungimo_vardas, slaptazodis, vaidmuo, sukurta) 
                    VALUES (@firstName, @loginName, @password, 'Gyventojas', NOW())";

                var parameters = new[]
                {
                    new MySqlParameter("@firstName", firstName),
                    new MySqlParameter("@loginName", loginName),
                    new MySqlParameter("@password", password)
                };

                int rowsAffected = db.ExecuteNonQuery(query, parameters);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Gyventojas sėkmingai sukurtas!");
                    LoadResidents();
                    txtResidentName.Clear();
                    txtResidentLogin.Clear();
                }
                else
                {
                    MessageBox.Show("Nepavyko sukurti gyventojo.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Klaida kuriant gyventoją: {ex.Message}");
            }
        }

        private void btnDeleteResident_Click(object sender, EventArgs e)
        {
            if (cmbResidents.SelectedItem is ComboBoxItem selectedResident)
            {
                try
                {
                    string query = "DELETE FROM naudotojai WHERE naudotojo_id = @id";
                    var parameters = new[] { new MySqlParameter("@id", selectedResident.Value) };

                    int rowsAffected = db.ExecuteNonQuery(query, parameters);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Gyventojas sėkmingai pašalintas!");
                        LoadResidents();
                    }
                    else
                    {
                        MessageBox.Show("Nepavyko pašalinti gyventojo.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Klaida šalinant gyventoją: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Pasirinkite gyventoją iš sąrašo!");
            }
        }

        private void btnAssignResident_Click(object sender, EventArgs e)
        {
            if (cmbHomes.SelectedItem is ComboBoxItem selectedHome && cmbResidents.SelectedItem is ComboBoxItem selectedResident)
            {
                try
                {
                    string query = "INSERT INTO namo_gyventojai (namo_id, naudotojo_id, vaidmuo_grupeje) VALUES (@homeId, @residentId, 'Gyventojas')";
                    var parameters = new[]
                    {
                        new MySqlParameter("@homeId", selectedHome.Value),
                        new MySqlParameter("@residentId", selectedResident.Value)
                    };

                    int rowsAffected = db.ExecuteNonQuery(query, parameters);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Gyventojas sėkmingai priskirtas namui!");
                    }
                    else
                    {
                        MessageBox.Show("Nepavyko priskirti gyventojo.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Klaida priskiriant gyventoją: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Pasirinkite namą ir gyventoją iš sąrašo!");
            }
        }

        private void btnRemoveResident_Click(object sender, EventArgs e)
        {
            if (cmbHomes.SelectedItem is ComboBoxItem selectedHome && cmbResidents.SelectedItem is ComboBoxItem selectedResident)
            {
                try
                {
                    string query = "DELETE FROM namo_gyventojai WHERE namo_id = @homeId AND naudotojo_id = @residentId";
                    var parameters = new[]
                    {
                        new MySqlParameter("@homeId", selectedHome.Value),
                        new MySqlParameter("@residentId", selectedResident.Value)
                    };

                    int rowsAffected = db.ExecuteNonQuery(query, parameters);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Gyventojas sėkmingai pašalintas iš namo!");
                    }
                    else
                    {
                        MessageBox.Show("Nepavyko pašalinti gyventojo iš namo.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Klaida šalinant gyventoją iš namo: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Pasirinkite namą ir gyventoją iš sąrašo!");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    
    public class ComboBoxItem
    {
        public string Text { get; }
        public int Value { get; }

        public ComboBoxItem(string text, int value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
