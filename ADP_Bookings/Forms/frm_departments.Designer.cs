﻿namespace ADP_Bookings.Forms
{
    partial class frm_departments
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
            this.lvw_Departments = new System.Windows.Forms.ListView();
            this.clm_CompanyID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_CompanyName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvw_DepartmentBookings = new System.Windows.Forms.ListView();
            this.clm_DepartmentID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_DepartmentName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_AddDepartment = new System.Windows.Forms.Button();
            this.lbl_Departments = new System.Windows.Forms.Label();
            this.lbl_DepartmentBookings = new System.Windows.Forms.Label();
            this.btn_ConfirmChanges = new System.Windows.Forms.Button();
            this.btn_CancelChanges = new System.Windows.Forms.Button();
            this.lbl_EditDepartment = new System.Windows.Forms.Label();
            this.lbl_DepartmentID = new System.Windows.Forms.Label();
            this.txt_DepartmentID = new System.Windows.Forms.TextBox();
            this.lbl_DepartmentName = new System.Windows.Forms.Label();
            this.txt_DepartmentName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lvw_Departments
            // 
            this.lvw_Departments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clm_CompanyID,
            this.clm_CompanyName});
            this.lvw_Departments.FullRowSelect = true;
            this.lvw_Departments.HideSelection = false;
            this.lvw_Departments.Location = new System.Drawing.Point(12, 52);
            this.lvw_Departments.Name = "lvw_Departments";
            this.lvw_Departments.Size = new System.Drawing.Size(339, 214);
            this.lvw_Departments.TabIndex = 28;
            this.lvw_Departments.UseCompatibleStateImageBehavior = false;
            this.lvw_Departments.View = System.Windows.Forms.View.Details;
            this.lvw_Departments.SelectedIndexChanged += new System.EventHandler(this.lvw_Departments_SelectedIndexChanged);
            // 
            // clm_CompanyID
            // 
            this.clm_CompanyID.Text = "ID";
            // 
            // clm_CompanyName
            // 
            this.clm_CompanyName.Text = "Name";
            // 
            // lvw_DepartmentBookings
            // 
            this.lvw_DepartmentBookings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clm_DepartmentID,
            this.clm_DepartmentName});
            this.lvw_DepartmentBookings.HideSelection = false;
            this.lvw_DepartmentBookings.Location = new System.Drawing.Point(411, 120);
            this.lvw_DepartmentBookings.Name = "lvw_DepartmentBookings";
            this.lvw_DepartmentBookings.Size = new System.Drawing.Size(377, 146);
            this.lvw_DepartmentBookings.TabIndex = 27;
            this.lvw_DepartmentBookings.UseCompatibleStateImageBehavior = false;
            this.lvw_DepartmentBookings.View = System.Windows.Forms.View.Details;
            // 
            // clm_DepartmentID
            // 
            this.clm_DepartmentID.Text = "ID";
            // 
            // clm_DepartmentName
            // 
            this.clm_DepartmentName.Text = "Name";
            // 
            // btn_AddDepartment
            // 
            this.btn_AddDepartment.Location = new System.Drawing.Point(115, 278);
            this.btn_AddDepartment.Name = "btn_AddDepartment";
            this.btn_AddDepartment.Size = new System.Drawing.Size(128, 40);
            this.btn_AddDepartment.TabIndex = 26;
            this.btn_AddDepartment.Text = "Add New Department";
            this.btn_AddDepartment.UseVisualStyleBackColor = true;
            this.btn_AddDepartment.Click += new System.EventHandler(this.btn_AddDepartment_Click);
            // 
            // lbl_Departments
            // 
            this.lbl_Departments.AutoSize = true;
            this.lbl_Departments.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Departments.Location = new System.Drawing.Point(134, 17);
            this.lbl_Departments.Name = "lbl_Departments";
            this.lbl_Departments.Size = new System.Drawing.Size(100, 17);
            this.lbl_Departments.TabIndex = 25;
            this.lbl_Departments.Text = "Departments";
            // 
            // lbl_DepartmentBookings
            // 
            this.lbl_DepartmentBookings.AutoSize = true;
            this.lbl_DepartmentBookings.Location = new System.Drawing.Point(408, 103);
            this.lbl_DepartmentBookings.Name = "lbl_DepartmentBookings";
            this.lbl_DepartmentBookings.Size = new System.Drawing.Size(106, 13);
            this.lbl_DepartmentBookings.TabIndex = 24;
            this.lbl_DepartmentBookings.Text = "Deparment Bookings";
            // 
            // btn_ConfirmChanges
            // 
            this.btn_ConfirmChanges.Location = new System.Drawing.Point(660, 277);
            this.btn_ConfirmChanges.Name = "btn_ConfirmChanges";
            this.btn_ConfirmChanges.Size = new System.Drawing.Size(128, 40);
            this.btn_ConfirmChanges.TabIndex = 23;
            this.btn_ConfirmChanges.Text = "Confirm";
            this.btn_ConfirmChanges.UseVisualStyleBackColor = true;
            this.btn_ConfirmChanges.Click += new System.EventHandler(this.btn_ConfirmChanges_Click);
            // 
            // btn_CancelChanges
            // 
            this.btn_CancelChanges.Location = new System.Drawing.Point(411, 278);
            this.btn_CancelChanges.Name = "btn_CancelChanges";
            this.btn_CancelChanges.Size = new System.Drawing.Size(128, 40);
            this.btn_CancelChanges.TabIndex = 22;
            this.btn_CancelChanges.Text = "Cancel";
            this.btn_CancelChanges.UseVisualStyleBackColor = true;
            this.btn_CancelChanges.Click += new System.EventHandler(this.btn_CancelChanges_Click);
            // 
            // lbl_EditDepartment
            // 
            this.lbl_EditDepartment.AutoSize = true;
            this.lbl_EditDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EditDepartment.Location = new System.Drawing.Point(548, 17);
            this.lbl_EditDepartment.Name = "lbl_EditDepartment";
            this.lbl_EditDepartment.Size = new System.Drawing.Size(125, 17);
            this.lbl_EditDepartment.TabIndex = 21;
            this.lbl_EditDepartment.Text = "Edit Department";
            // 
            // lbl_DepartmentID
            // 
            this.lbl_DepartmentID.AutoSize = true;
            this.lbl_DepartmentID.Location = new System.Drawing.Point(408, 52);
            this.lbl_DepartmentID.Name = "lbl_DepartmentID";
            this.lbl_DepartmentID.Size = new System.Drawing.Size(79, 13);
            this.lbl_DepartmentID.TabIndex = 20;
            this.lbl_DepartmentID.Text = "Department ID:";
            // 
            // txt_DepartmentID
            // 
            this.txt_DepartmentID.Location = new System.Drawing.Point(504, 49);
            this.txt_DepartmentID.Name = "txt_DepartmentID";
            this.txt_DepartmentID.ReadOnly = true;
            this.txt_DepartmentID.Size = new System.Drawing.Size(100, 20);
            this.txt_DepartmentID.TabIndex = 19;
            // 
            // lbl_DepartmentName
            // 
            this.lbl_DepartmentName.AutoSize = true;
            this.lbl_DepartmentName.Location = new System.Drawing.Point(408, 78);
            this.lbl_DepartmentName.Name = "lbl_DepartmentName";
            this.lbl_DepartmentName.Size = new System.Drawing.Size(96, 13);
            this.lbl_DepartmentName.TabIndex = 18;
            this.lbl_DepartmentName.Text = "Department Name:";
            // 
            // txt_DepartmentName
            // 
            this.txt_DepartmentName.Location = new System.Drawing.Point(504, 75);
            this.txt_DepartmentName.Name = "txt_DepartmentName";
            this.txt_DepartmentName.Size = new System.Drawing.Size(100, 20);
            this.txt_DepartmentName.TabIndex = 17;
            // 
            // frm_departments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lvw_Departments);
            this.Controls.Add(this.lvw_DepartmentBookings);
            this.Controls.Add(this.btn_AddDepartment);
            this.Controls.Add(this.lbl_Departments);
            this.Controls.Add(this.lbl_DepartmentBookings);
            this.Controls.Add(this.btn_ConfirmChanges);
            this.Controls.Add(this.btn_CancelChanges);
            this.Controls.Add(this.lbl_EditDepartment);
            this.Controls.Add(this.lbl_DepartmentID);
            this.Controls.Add(this.txt_DepartmentID);
            this.Controls.Add(this.lbl_DepartmentName);
            this.Controls.Add(this.txt_DepartmentName);
            this.Name = "frm_departments";
            this.Text = "frm_departments";
            this.Load += new System.EventHandler(this.frm_departments_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvw_Departments;
        private System.Windows.Forms.ColumnHeader clm_CompanyID;
        private System.Windows.Forms.ColumnHeader clm_CompanyName;
        private System.Windows.Forms.ListView lvw_DepartmentBookings;
        private System.Windows.Forms.ColumnHeader clm_DepartmentID;
        private System.Windows.Forms.ColumnHeader clm_DepartmentName;
        private System.Windows.Forms.Button btn_AddDepartment;
        private System.Windows.Forms.Label lbl_Departments;
        private System.Windows.Forms.Label lbl_DepartmentBookings;
        private System.Windows.Forms.Button btn_ConfirmChanges;
        private System.Windows.Forms.Button btn_CancelChanges;
        private System.Windows.Forms.Label lbl_EditDepartment;
        private System.Windows.Forms.Label lbl_DepartmentID;
        private System.Windows.Forms.TextBox txt_DepartmentID;
        private System.Windows.Forms.Label lbl_DepartmentName;
        private System.Windows.Forms.TextBox txt_DepartmentName;
    }
}