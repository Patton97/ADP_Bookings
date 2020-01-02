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
            this.txt_CompanyName = new System.Windows.Forms.TextBox();
            this.lbl_CompanyName = new System.Windows.Forms.Label();
            this.lbl_CompanyID = new System.Windows.Forms.Label();
            this.txt_CompanyID = new System.Windows.Forms.TextBox();
            this.lbl_EditCompany = new System.Windows.Forms.Label();
            this.btn_CancelChanges = new System.Windows.Forms.Button();
            this.btn_ConfirmChanges = new System.Windows.Forms.Button();
            this.lbl_CompanyDepartments = new System.Windows.Forms.Label();
            this.lbl_test = new System.Windows.Forms.Label();
            this.lbl_Companies = new System.Windows.Forms.Label();
            this.btn_AddCompany = new System.Windows.Forms.Button();
            this.lvw_CompanyDepartments = new System.Windows.Forms.ListView();
            this.clm_DepartmentID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_DepartmentName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvw_companies = new System.Windows.Forms.ListView();
            this.clm_CompanyID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_CompanyName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_EditDepartments = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_CompanyName
            // 
            this.txt_CompanyName.Location = new System.Drawing.Point(504, 75);
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
            this.txt_CompanyID.Location = new System.Drawing.Point(504, 49);
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
            this.btn_CancelChanges.Click += new System.EventHandler(this.btn_CancelChanges_Click);
            // 
            // btn_ConfirmChanges
            // 
            this.btn_ConfirmChanges.Location = new System.Drawing.Point(566, 278);
            this.btn_ConfirmChanges.Name = "btn_ConfirmChanges";
            this.btn_ConfirmChanges.Size = new System.Drawing.Size(128, 40);
            this.btn_ConfirmChanges.TabIndex = 8;
            this.btn_ConfirmChanges.Text = "Confirm";
            this.btn_ConfirmChanges.UseVisualStyleBackColor = true;
            this.btn_ConfirmChanges.Click += new System.EventHandler(this.btn_ConfirmChanges_Click);
            // 
            // lbl_CompanyDepartments
            // 
            this.lbl_CompanyDepartments.AutoSize = true;
            this.lbl_CompanyDepartments.Location = new System.Drawing.Point(408, 103);
            this.lbl_CompanyDepartments.Name = "lbl_CompanyDepartments";
            this.lbl_CompanyDepartments.Size = new System.Drawing.Size(117, 13);
            this.lbl_CompanyDepartments.TabIndex = 9;
            this.lbl_CompanyDepartments.Text = "Company Departments:";
            // 
            // lbl_test
            // 
            this.lbl_test.AutoSize = true;
            this.lbl_test.Location = new System.Drawing.Point(9, 428);
            this.lbl_test.Name = "lbl_test";
            this.lbl_test.Size = new System.Drawing.Size(28, 13);
            this.lbl_test.TabIndex = 10;
            this.lbl_test.Text = "Test";
            // 
            // lbl_Companies
            // 
            this.lbl_Companies.AutoSize = true;
            this.lbl_Companies.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Companies.Location = new System.Drawing.Point(134, 17);
            this.lbl_Companies.Name = "lbl_Companies";
            this.lbl_Companies.Size = new System.Drawing.Size(87, 17);
            this.lbl_Companies.TabIndex = 13;
            this.lbl_Companies.Text = "Companies";
            // 
            // btn_AddCompany
            // 
            this.btn_AddCompany.Location = new System.Drawing.Point(115, 278);
            this.btn_AddCompany.Name = "btn_AddCompany";
            this.btn_AddCompany.Size = new System.Drawing.Size(128, 40);
            this.btn_AddCompany.TabIndex = 14;
            this.btn_AddCompany.Text = "Add New Company";
            this.btn_AddCompany.UseVisualStyleBackColor = true;
            this.btn_AddCompany.Click += new System.EventHandler(this.btn_AddCompany_Click);
            // 
            // lvw_CompanyDepartments
            // 
            this.lvw_CompanyDepartments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clm_DepartmentID,
            this.clm_DepartmentName});
            this.lvw_CompanyDepartments.HideSelection = false;
            this.lvw_CompanyDepartments.Location = new System.Drawing.Point(411, 120);
            this.lvw_CompanyDepartments.Name = "lvw_CompanyDepartments";
            this.lvw_CompanyDepartments.Size = new System.Drawing.Size(283, 146);
            this.lvw_CompanyDepartments.TabIndex = 15;
            this.lvw_CompanyDepartments.UseCompatibleStateImageBehavior = false;
            this.lvw_CompanyDepartments.View = System.Windows.Forms.View.Details;
            // 
            // clm_DepartmentID
            // 
            this.clm_DepartmentID.Text = "ID";
            // 
            // clm_DepartmentName
            // 
            this.clm_DepartmentName.Text = "Name";
            // 
            // lvw_companies
            // 
            this.lvw_companies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clm_CompanyID,
            this.clm_CompanyName});
            this.lvw_companies.FullRowSelect = true;
            this.lvw_companies.HideSelection = false;
            this.lvw_companies.Location = new System.Drawing.Point(12, 52);
            this.lvw_companies.Name = "lvw_companies";
            this.lvw_companies.Size = new System.Drawing.Size(339, 214);
            this.lvw_companies.TabIndex = 16;
            this.lvw_companies.UseCompatibleStateImageBehavior = false;
            this.lvw_companies.View = System.Windows.Forms.View.Details;
            this.lvw_companies.SelectedIndexChanged += new System.EventHandler(this.lvw_companies_SelectedIndexChanged);
            // 
            // clm_CompanyID
            // 
            this.clm_CompanyID.Text = "ID";
            // 
            // clm_CompanyName
            // 
            this.clm_CompanyName.Text = "Name";
            // 
            // btn_EditDepartments
            // 
            this.btn_EditDepartments.Location = new System.Drawing.Point(700, 120);
            this.btn_EditDepartments.Name = "btn_EditDepartments";
            this.btn_EditDepartments.Size = new System.Drawing.Size(88, 146);
            this.btn_EditDepartments.TabIndex = 17;
            this.btn_EditDepartments.Text = "Edit Departments";
            this.btn_EditDepartments.UseVisualStyleBackColor = true;
            this.btn_EditDepartments.Click += new System.EventHandler(this.btn_EditDepartments_Click);
            // 
            // frm_companies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_EditDepartments);
            this.Controls.Add(this.lvw_companies);
            this.Controls.Add(this.lvw_CompanyDepartments);
            this.Controls.Add(this.btn_AddCompany);
            this.Controls.Add(this.lbl_Companies);
            this.Controls.Add(this.lbl_test);
            this.Controls.Add(this.lbl_CompanyDepartments);
            this.Controls.Add(this.btn_ConfirmChanges);
            this.Controls.Add(this.btn_CancelChanges);
            this.Controls.Add(this.lbl_EditCompany);
            this.Controls.Add(this.lbl_CompanyID);
            this.Controls.Add(this.txt_CompanyID);
            this.Controls.Add(this.lbl_CompanyName);
            this.Controls.Add(this.txt_CompanyName);
            this.Name = "frm_companies";
            this.Text = "ADP Bookings System: Companies";
            this.Load += new System.EventHandler(this.frm_companies_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_CompanyName;
        private System.Windows.Forms.Label lbl_CompanyName;
        private System.Windows.Forms.Label lbl_CompanyID;
        private System.Windows.Forms.TextBox txt_CompanyID;
        private System.Windows.Forms.Label lbl_EditCompany;
        private System.Windows.Forms.Button btn_CancelChanges;
        private System.Windows.Forms.Button btn_ConfirmChanges;
        private System.Windows.Forms.Label lbl_CompanyDepartments;
        private System.Windows.Forms.Label lbl_test;
        private System.Windows.Forms.Label lbl_Companies;
        private System.Windows.Forms.Button btn_AddCompany;
        private System.Windows.Forms.ListView lvw_CompanyDepartments;
        private System.Windows.Forms.ListView lvw_companies;
        private System.Windows.Forms.ColumnHeader clm_CompanyID;
        private System.Windows.Forms.ColumnHeader clm_CompanyName;
        private System.Windows.Forms.ColumnHeader clm_DepartmentID;
        private System.Windows.Forms.ColumnHeader clm_DepartmentName;
        private System.Windows.Forms.Button btn_EditDepartments;
    }
}