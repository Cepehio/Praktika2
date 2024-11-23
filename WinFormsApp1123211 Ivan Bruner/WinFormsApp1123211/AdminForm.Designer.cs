namespace WinFormsApp1123211
{
    partial class AdminForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button btnManageResidents;
        private System.Windows.Forms.Button btnManageManagers;
        private System.Windows.Forms.Button btnManageHomes;
        private System.Windows.Forms.Button btnExit;

  
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
            this.btnManageResidents = new System.Windows.Forms.Button();
            this.btnManageManagers = new System.Windows.Forms.Button();
            this.btnManageHomes = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();

          
            
            this.btnManageResidents.Location = new System.Drawing.Point(129, 61);
            this.btnManageResidents.Name = "btnManageResidents";
            this.btnManageResidents.Size = new System.Drawing.Size(230, 62);
            this.btnManageResidents.TabIndex = 0;
            this.btnManageResidents.Text = "Redaguoti gyventojus";
            this.btnManageResidents.UseVisualStyleBackColor = true;
            this.btnManageResidents.Click += new System.EventHandler(this.btnManageResidents_Click);

            
           
            this.btnManageManagers.Location = new System.Drawing.Point(129, 148);
            this.btnManageManagers.Name = "btnManageManagers";
            this.btnManageManagers.Size = new System.Drawing.Size(230, 59);
            this.btnManageManagers.TabIndex = 1;
            this.btnManageManagers.Text = "Redaguoti vadybininkus";
            this.btnManageManagers.UseVisualStyleBackColor = true;
            this.btnManageManagers.Click += new System.EventHandler(this.btnManageManagers_Click);

         
            this.btnManageHomes.Location = new System.Drawing.Point(129, 233);
            this.btnManageHomes.Name = "btnManageHomes";
            this.btnManageHomes.Size = new System.Drawing.Size(230, 60);
            this.btnManageHomes.TabIndex = 2;
            this.btnManageHomes.Text = "Redaguoti bendrijas";
            this.btnManageHomes.UseVisualStyleBackColor = true;
            this.btnManageHomes.Click += new System.EventHandler(this.btnManageHomes_Click);

            this.btnExit.Location = new System.Drawing.Point(129, 315);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(230, 64);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Išeiti iš programos";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

          
            this.ClientSize = new System.Drawing.Size(510, 444);
            this.Controls.Add(this.btnManageResidents);
            this.Controls.Add(this.btnManageManagers);
            this.Controls.Add(this.btnManageHomes);
            this.Controls.Add(this.btnExit);
            this.Name = "AdminForm";
            this.Text = "Administratoriaus valdymas";
            this.ResumeLayout(false);
        }
    }
}
