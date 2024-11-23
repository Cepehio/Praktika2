namespace WinFormsApp1123211
{
    partial class ManageManagersForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox cmbHomes;
        private System.Windows.Forms.ComboBox cmbManagers;
        private System.Windows.Forms.Button btnAssignManager;
        private System.Windows.Forms.Button btnRemoveManager;
        private System.Windows.Forms.Button btnCreateManager;
        private System.Windows.Forms.Button btnDeleteManager;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblHomes;
        private System.Windows.Forms.Label lblManagers;
        private System.Windows.Forms.Label lblNewManagerName;
        private System.Windows.Forms.Label lblNewManagerPassword;
        private System.Windows.Forms.TextBox txtNewManagerName;
        private System.Windows.Forms.TextBox txtNewManagerPassword;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            cmbHomes = new ComboBox();
            cmbManagers = new ComboBox();
            btnAssignManager = new Button();
            btnRemoveManager = new Button();
            btnCreateManager = new Button();
            btnDeleteManager = new Button();
            btnBack = new Button();
            lblHomes = new Label();
            lblManagers = new Label();
            lblNewManagerName = new Label();
            lblNewManagerPassword = new Label();
            txtNewManagerName = new TextBox();
            txtNewManagerPassword = new TextBox();
            SuspendLayout();
          


            cmbHomes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHomes.FormattingEnabled = true;
            cmbHomes.Location = new Point(218, 30);
            cmbHomes.Name = "cmbHomes";
            cmbHomes.Size = new Size(367, 33);
            cmbHomes.TabIndex = 1;
           


            cmbManagers.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbManagers.FormattingEnabled = true;
            cmbManagers.Location = new Point(218, 70);
            cmbManagers.Name = "cmbManagers";
            cmbManagers.Size = new Size(367, 33);
            cmbManagers.TabIndex = 3;
         


            btnAssignManager.Location = new Point(218, 120);
            btnAssignManager.Name = "btnAssignManager";
            btnAssignManager.Size = new Size(140, 30);
            btnAssignManager.TabIndex = 4;
            btnAssignManager.Text = "Priskirti ";
            btnAssignManager.UseVisualStyleBackColor = true;
            btnAssignManager.Click += btnAssignManager_Click;
          


            btnRemoveManager.Location = new Point(364, 120);
            btnRemoveManager.Name = "btnRemoveManager";
            btnRemoveManager.Size = new Size(221, 30);
            btnRemoveManager.TabIndex = 5;
            btnRemoveManager.Text = "Pašalinti vadybininką";
            btnRemoveManager.UseVisualStyleBackColor = true;
            btnRemoveManager.Click += btnRemoveManager_Click;
         



            btnCreateManager.Location = new Point(271, 250);
            btnCreateManager.Name = "btnCreateManager";
            btnCreateManager.Size = new Size(115, 30);
            btnCreateManager.TabIndex = 10;
            btnCreateManager.Text = "Sukurti ";
            btnCreateManager.UseVisualStyleBackColor = true;
            btnCreateManager.Click += btnCreateManager_Click;
         


            btnDeleteManager.Location = new Point(432, 250);
            btnDeleteManager.Name = "btnDeleteManager";
            btnDeleteManager.Size = new Size(104, 30);
            btnDeleteManager.TabIndex = 11;
            btnDeleteManager.Text = "Pašalinti ";
            btnDeleteManager.UseVisualStyleBackColor = true;
            btnDeleteManager.Click += btnDeleteManager_Click;
        



            btnBack.Location = new Point(359, 315);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(102, 35);
            btnBack.TabIndex = 12;
            btnBack.Text = "Grįžti";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
         


            lblHomes.AutoSize = true;
            lblHomes.Location = new Point(50, 30);
            lblHomes.Name = "lblHomes";
            lblHomes.Size = new Size(71, 25);
            lblHomes.TabIndex = 0;
            lblHomes.Text = "Namas:";
           


            lblManagers.AutoSize = true;
            lblManagers.Location = new Point(50, 70);
            lblManagers.Name = "lblManagers";
            lblManagers.Size = new Size(120, 25);
            lblManagers.TabIndex = 2;
            lblManagers.Text = "Vadybininkas:";
        


            lblNewManagerName.AutoSize = true;
            lblNewManagerName.Location = new Point(50, 170);
            lblNewManagerName.Name = "lblNewManagerName";
            lblNewManagerName.Size = new Size(171, 25);
            lblNewManagerName.TabIndex = 6;
            lblNewManagerName.Text = "Vadybininko vardas:";
          


            lblNewManagerPassword.AutoSize = true;
            lblNewManagerPassword.Location = new Point(50, 210);
            lblNewManagerPassword.Name = "lblNewManagerPassword";
            lblNewManagerPassword.Size = new Size(208, 25);
            lblNewManagerPassword.TabIndex = 8;
            lblNewManagerPassword.Text = "Vadybininko slaptažodis:";
           


            txtNewManagerName.Location = new Point(276, 173);
            txtNewManagerName.Name = "txtNewManagerName";
            txtNewManagerName.Size = new Size(260, 31);
            txtNewManagerName.TabIndex = 7;
          


            txtNewManagerPassword.Location = new Point(276, 210);
            txtNewManagerPassword.Name = "txtNewManagerPassword";
            txtNewManagerPassword.Size = new Size(260, 31);
            txtNewManagerPassword.TabIndex = 9;
           


            ClientSize = new Size(656, 450);
            Controls.Add(lblHomes);
            Controls.Add(cmbHomes);
            Controls.Add(lblManagers);
            Controls.Add(cmbManagers);
            Controls.Add(btnAssignManager);
            Controls.Add(btnRemoveManager);
            Controls.Add(lblNewManagerName);
            Controls.Add(txtNewManagerName);
            Controls.Add(lblNewManagerPassword);
            Controls.Add(txtNewManagerPassword);
            Controls.Add(btnCreateManager);
            Controls.Add(btnDeleteManager);
            Controls.Add(btnBack);
            Name = "ManageManagersForm";
            Text = "Redaguoti vadybininkus";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
