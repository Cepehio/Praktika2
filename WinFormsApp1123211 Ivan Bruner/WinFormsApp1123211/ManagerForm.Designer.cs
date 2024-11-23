namespace WinFormsApp1123211
{
    partial class ManagerForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox cmbServices;
        private System.Windows.Forms.TextBox txtServicePrice;
        private System.Windows.Forms.Button btnUpdatePrice;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblAssignedHome;
        private System.Windows.Forms.Label lblService;
        private System.Windows.Forms.Label lblServicePrice;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        void InitializeComponent()
        {
            cmbServices = new ComboBox();
            txtServicePrice = new TextBox();
            btnUpdatePrice = new Button();
            btnExit = new Button();
            lblAssignedHome = new Label();
            lblService = new Label();
            lblServicePrice = new Label();
            SuspendLayout();
         


            cmbServices.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbServices.FormattingEnabled = true;
            cmbServices.Location = new Point(234, 119);
            cmbServices.Name = "cmbServices";
            cmbServices.Size = new Size(200, 33);
            cmbServices.TabIndex = 1;
          



            txtServicePrice.Location = new Point(234, 159);
            txtServicePrice.Name = "txtServicePrice";
            txtServicePrice.Size = new Size(200, 31);
            txtServicePrice.TabIndex = 2;
        



            btnUpdatePrice.Location = new Point(261, 211);
            btnUpdatePrice.Name = "btnUpdatePrice";
            btnUpdatePrice.Size = new Size(150, 30);
            btnUpdatePrice.TabIndex = 3;
            btnUpdatePrice.Text = "Keisti kainą";
            btnUpdatePrice.UseVisualStyleBackColor = true;
            btnUpdatePrice.Click += btnUpdatePrice_Click;
         



            btnExit.Location = new Point(261, 261);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(150, 30);
            btnExit.TabIndex = 4;
            btnExit.Text = "Išeiti";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
         



            lblAssignedHome.AutoSize = true;
            lblAssignedHome.Location = new Point(54, 79);
            lblAssignedHome.Name = "lblAssignedHome";
            lblAssignedHome.Size = new Size(177, 25);
            lblAssignedHome.TabIndex = 5;
            lblAssignedHome.Text = "Paskirtas namas: XYZ";
          



            lblService.AutoSize = true;
            lblService.Location = new Point(54, 119);
            lblService.Name = "lblService";
            lblService.Size = new Size(85, 25);
            lblService.TabIndex = 6;
            lblService.Text = "Paslauga:";
          


            lblServicePrice.AutoSize = true;
            lblServicePrice.Location = new Point(54, 159);
            lblServicePrice.Name = "lblServicePrice";
            lblServicePrice.Size = new Size(58, 25);
            lblServicePrice.TabIndex = 7;
            lblServicePrice.Text = "Kaina:";
           


            ClientSize = new Size(547, 375);
            Controls.Add(lblAssignedHome);
            Controls.Add(cmbServices);
            Controls.Add(lblService);
            Controls.Add(txtServicePrice);
            Controls.Add(lblServicePrice);
            Controls.Add(btnUpdatePrice);
            Controls.Add(btnExit);
            Name = "ManagerForm";
            Text = "Valdyti paslaugas";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
