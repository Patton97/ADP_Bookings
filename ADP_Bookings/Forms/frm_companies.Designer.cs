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
            this.dgv_companyDepartments = new System.Windows.Forms.DataGridView();
            this.txt_CompanyName = new System.Windows.Forms.TextBox();
            this.lbl_CompanyName = new System.Windows.Forms.Label();
            this.lbl_CompanyID = new System.Windows.Forms.Label();
            this.txt_CompanyID = new System.Windows.Forms.TextBox();
            this.lbl_EditCompany = new System.Windows.Forms.Label();
            this.btn_CancelChanges = new System.Windows.Forms.Button();
            this.btn_ConfirmChanges = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_companies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_companyDepartments)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_companies
            // 
            this.dgv_companies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_companies.Location = new System.Drawing.Point(12, 12);
            this.dgv_companies.Name = "dgv_companies";
            this.dgv_companies.Size = new System.Drawing.Size(377, 306);
            this.dgv_companies.TabIndex = 0;
            this.dgv_companies.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_companies_CellContentDoubleClick);
            // 
            // dgv_companyDepartments
            // 
            this.dgv_companyDepartments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_companyDepartments.Location = new System.Drawing.Point(411, 126);
            this.dgv_companyDepartments.Name = "dgv_companyDepartments";
            this.dgv_companyDepartments.Size = new System.Drawing.Size(377, 145);
            this.dgv_companyDepartments.TabIndex = 1;
            // 
            // txt_CompanyName
            // 
            this.txt_CompanyName.Location = new System.Drawing.Point(499, 75);
            this.txt_CompanyName.Name = "txt_CompanyName";
            this.txt_CompanyName.Size = new System.Drawing.Size(100, 20);
            this.txt_CompanyName.TabIndex = 2;
            // 
            // lbl_CompanyName
            // 
            this.lbl_CompanyName.AutoSize = true;
            this.lbl_CompanyName.Location = new System.Drawing.Point(408, 78);
            this.lbl_CompanyName.Name = "lbl_CompanyName";
            this.lbl_CompanyName.Size = new System.Drawing.Size(85, 13);
            this.lbl_CompanyName.TabIndex = 3;
            this.lbl_CompanyName.Text = "Company Name:";
            // 
            // lbl_CompanyID
            // 
            this.lbl_CompanyID.AutoSize = true;
            this.lbl_CompanyID.Location = new System.Drawing.Point(408, 52);
            this.lbl_CompanyID.Name = "lbl_CompanyID";
            this.lbl_CompanyID.Size = new System.Drawing.Size(68, 13);
            this.lbl_CompanyID.TabIndex = 5;
            this.lbl_CompanyID.Text = "Company ID:";
            // 
            // txt_CompanyID
            // 
            this.txt_CompanyID.Location = new System.Drawing.Point(499, 49);
            this.txt_CompanyID.Name = "txt_CompanyID";
            this.txt_CompanyID.ReadOnly = true;
            this.txt_CompanyID.Size = new System.Drawing.Size(100, 20);
            this.txt_CompanyID.TabIndex = 4;
            // 
            // lbl_EditCompany
            // 
            this.lbl_EditCompany.AutoSize = true;
            this.lbl_EditCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EditCompany.Location = new System.Drawing.Point(548, 17);
            this.lbl_EditCompany.Name = "lbl_EditCompany";
            this.lbl_EditCompany.Size = new System.Drawing.Size(107, 17);
            this.lbl_EditCompany.TabIndex = 6;
            this.lbl_EditCompany.Text = "Edit Company";
            // 
            // btn_CancelChanges
            // 
            this.btn_CancelChanges.Location = new System.Drawing.Point(411, 278);
            this.btn_CancelChanges.Name = "btn_CancelChanges";
            this.btn_CancelChanges.Size = new System.Drawing.Size(128, 40);
            this.btn_CancelChanges.TabIndex = 7;
            this.btn_CancelChanges.Text = "Cancel";
            this.btn_CancelChanges.UseVisualStyleBackColor = true;
            // 
            // btn_ConfirmChanges
            // 
            this.btn_ConfirmChanges.Location = new System.Drawing.Point(660, 277);
            this.btn_ConfirmChanges.Name = "btn_ConfirmChanges";
            this.btn_ConfirmChanges.Size = new System.Drawing.Size(128, 40);
            this.btn_ConfirmChanges.TabIndex = 8;
            this.btn_ConfirmChanges.Text = "Confirm";
            this.btn_ConfirmChanges.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(408, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Company Departments:";
            // 
            // frm_companies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ConfirmChanges);
            this.Controls.Add(this.btn_CancelChanges);
            this.Controls.Add(this.lbl_EditCompany);
            this.Controls.Add(this.lbl_CompanyID);
            this.Controls.Add(this.txt_CompanyID);
            this.Controls.Add(this.lbl_CompanyName);
            this.Controls.Add(this.txt_CompanyName);
            this.Controls.Add(this.dgv_companyDepartments);
            this.Controls.Add(this.dgv_companies);
            this.Name = "frm_companies";
            this.Text = "frm_companies";
            this.Load += new System.EventHandler(this.frm_companies_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_companies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_companyDepartments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_companies;
        private System.Windows.Forms.DataGridView dgv_companyDepartments;
        private System.Windows.Forms.TextBox txt_CompanyName;
        private System.Windows.Forms.Label lbl_CompanyName;
        private System.Windows.Forms.Label lbl_CompanyID;
        private System.Windows.Forms.TextBox txt_CompanyID;
        private System.Windows.Forms.Label lbl_EditCompany;
        private System.Windows.Forms.Button btn_CancelChanges;
        private System.Windows.Forms.Button btn_ConfirmChanges;
        private System.Windows.Forms.Label label1;
    }
}