namespace WinFormsApp1123211
{
    partial class ResidentForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblAssignedHome;
        private System.Windows.Forms.DataGridView dgvServices;
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
            this.lblAssignedHome = new System.Windows.Forms.Label();
            this.dgvServices = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).BeginInit();
            this.SuspendLayout();

           
            this.lblAssignedHome.AutoSize = true;
            this.lblAssignedHome.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAssignedHome.Location = new System.Drawing.Point(12, 9);
            this.lblAssignedHome.Name = "lblAssignedHome";
            this.lblAssignedHome.Size = new System.Drawing.Size(50, 23);
            this.lblAssignedHome.TabIndex = 0;
            this.lblAssignedHome.Text = "Loading...";

           
            this.dgvServices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServices.Location = new System.Drawing.Point(12, 40);
            this.dgvServices.Name = "dgvServices";
            this.dgvServices.RowHeadersWidth = 51;
            this.dgvServices.RowTemplate.Height = 29;
            this.dgvServices.Size = new System.Drawing.Size(760, 400); // Adjust size to fit the form
            this.dgvServices.TabIndex = 1;
            this.dgvServices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(697, 460);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 29);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Išeiti";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

           
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dgvServices);
            this.Controls.Add(this.lblAssignedHome);
            this.Name = "ResidentForm";
            this.Text = "Peržiūrėti paslaugas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
