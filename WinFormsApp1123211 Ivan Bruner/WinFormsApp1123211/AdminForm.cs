using System;
using System.Windows.Forms;

namespace WinFormsApp1123211
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void btnManageResidents_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ManageResidentsForm(), "Manage Residents");
        }

        private void btnManageManagers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ManageManagersForm(), "Manage Managers");
        }

        private void btnManageHomes_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ManageHomesForm(), "Manage Homes");
        }

        private void OpenChildForm(Form childForm, string formName)
        {
            try
            {
                childForm.Owner = this; 
                this.Hide(); 
                childForm.FormClosed += (s, args) => this.Show(); 
                childForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening {formName} form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ConfirmAndExit();
        }

        private void ConfirmAndExit()
        {
            var confirmExit = MessageBox.Show(
                "Ar tikrai norite išeiti iš programos?",
                "Patvirtinti išėjimą",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmExit == DialogResult.Yes)
            {
                Application.Exit(); 
            }
        }
    }
}
