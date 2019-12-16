namespace ADP_Bookings.Forms
{
    partial class frm_companies
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgv_companies = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_companies)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_companies
            // 
            this.dgv_companies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_companies.Location = new System.Drawing.Point(107, 74);
            this.dgv_companies.Name = "dgv_companies";
            this.dgv_companies.Size = new System.Drawing.Size(377, 306);
            this.dgv_companies.TabIndex = 0;
            // 
            // frm_companies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgv_companies);
            this.Name = "frm_companies";
            this.Text = "frm_companies";
            this.Load += new System.EventHandler(this.frm_companies_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_companies)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_companies;
    }
}