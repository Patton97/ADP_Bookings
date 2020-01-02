namespace ADP_Bookings.Forms
{
    partial class frm_bookings
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
            this.lvw_Bookings = new System.Windows.Forms.ListView();
            this.clm_CompanyID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_CompanyName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvw_BookingActivities = new System.Windows.Forms.ListView();
            this.clm_DepartmentID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_DepartmentName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_AddBooking = new System.Windows.Forms.Button();
            this.lbl_Bookings = new System.Windows.Forms.Label();
            this.lbl_BookingActivities = new System.Windows.Forms.Label();
            this.btn_ConfirmChanges = new System.Windows.Forms.Button();
            this.btn_CancelChanges = new System.Windows.Forms.Button();
            this.lbl_EditDepartment = new System.Windows.Forms.Label();
            this.lbl_BookingID = new System.Windows.Forms.Label();
            this.txt_BookingID = new System.Windows.Forms.TextBox();
            this.lbl_BookingName = new System.Windows.Forms.Label();
            this.txt_BookingName = new System.Windows.Forms.TextBox();
            this.lbl_BookingDate = new System.Windows.Forms.Label();
            this.dtp_BookingDate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // lvw_Bookings
            // 
            this.lvw_Bookings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clm_CompanyID,
            this.clm_CompanyName});
            this.lvw_Bookings.FullRowSelect = true;
            this.lvw_Bookings.HideSelection = false;
            this.lvw_Bookings.Location = new System.Drawing.Point(12, 52);
            this.lvw_Bookings.Name = "lvw_Bookings";
            this.lvw_Bookings.Size = new System.Drawing.Size(339, 214);
            this.lvw_Bookings.TabIndex = 40;
            this.lvw_Bookings.UseCompatibleStateImageBehavior = false;
            this.lvw_Bookings.View = System.Windows.Forms.View.Details;
            // 
            // clm_CompanyID
            // 
            this.clm_CompanyID.Text = "ID";
            // 
            // clm_CompanyName
            // 
            this.clm_CompanyName.Text = "Name";
            // 
            // lvw_BookingActivities
            // 
            this.lvw_BookingActivities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clm_DepartmentID,
            this.clm_DepartmentName});
            this.lvw_BookingActivities.HideSelection = false;
            this.lvw_BookingActivities.Location = new System.Drawing.Point(411, 120);
            this.lvw_BookingActivities.Name = "lvw_BookingActivities";
            this.lvw_BookingActivities.Size = new System.Drawing.Size(377, 146);
            this.lvw_BookingActivities.TabIndex = 39;
            this.lvw_BookingActivities.UseCompatibleStateImageBehavior = false;
            this.lvw_BookingActivities.View = System.Windows.Forms.View.Details;
            // 
            // clm_DepartmentID
            // 
            this.clm_DepartmentID.Text = "ID";
            // 
            // clm_DepartmentName
            // 
            this.clm_DepartmentName.Text = "Name";
            // 
            // btn_AddBooking
            // 
            this.btn_AddBooking.Location = new System.Drawing.Point(115, 278);
            this.btn_AddBooking.Name = "btn_AddBooking";
            this.btn_AddBooking.Size = new System.Drawing.Size(128, 40);
            this.btn_AddBooking.TabIndex = 38;
            this.btn_AddBooking.Text = "Add New Booking";
            this.btn_AddBooking.UseVisualStyleBackColor = true;
            // 
            // lbl_Bookings
            // 
            this.lbl_Bookings.AutoSize = true;
            this.lbl_Bookings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Bookings.Location = new System.Drawing.Point(134, 17);
            this.lbl_Bookings.Name = "lbl_Bookings";
            this.lbl_Bookings.Size = new System.Drawing.Size(74, 17);
            this.lbl_Bookings.TabIndex = 37;
            this.lbl_Bookings.Text = "Bookings";
            // 
            // lbl_BookingActivities
            // 
            this.lbl_BookingActivities.AutoSize = true;
            this.lbl_BookingActivities.Location = new System.Drawing.Point(408, 103);
            this.lbl_BookingActivities.Name = "lbl_BookingActivities";
            this.lbl_BookingActivities.Size = new System.Drawing.Size(94, 13);
            this.lbl_BookingActivities.TabIndex = 36;
            this.lbl_BookingActivities.Text = "Booking Activities:";
            // 
            // btn_ConfirmChanges
            // 
            this.btn_ConfirmChanges.Location = new System.Drawing.Point(660, 277);
            this.btn_ConfirmChanges.Name = "btn_ConfirmChanges";
            this.btn_ConfirmChanges.Size = new System.Drawing.Size(128, 40);
            this.btn_ConfirmChanges.TabIndex = 35;
            this.btn_ConfirmChanges.Text = "Confirm";
            this.btn_ConfirmChanges.UseVisualStyleBackColor = true;
            // 
            // btn_CancelChanges
            // 
            this.btn_CancelChanges.Location = new System.Drawing.Point(411, 278);
            this.btn_CancelChanges.Name = "btn_CancelChanges";
            this.btn_CancelChanges.Size = new System.Drawing.Size(128, 40);
            this.btn_CancelChanges.TabIndex = 34;
            this.btn_CancelChanges.Text = "Cancel";
            this.btn_CancelChanges.UseVisualStyleBackColor = true;
            // 
            // lbl_EditDepartment
            // 
            this.lbl_EditDepartment.AutoSize = true;
            this.lbl_EditDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EditDepartment.Location = new System.Drawing.Point(548, 17);
            this.lbl_EditDepartment.Name = "lbl_EditDepartment";
            this.lbl_EditDepartment.Size = new System.Drawing.Size(99, 17);
            this.lbl_EditDepartment.TabIndex = 33;
            this.lbl_EditDepartment.Text = "Edit Booking";
            // 
            // lbl_BookingID
            // 
            this.lbl_BookingID.AutoSize = true;
            this.lbl_BookingID.Location = new System.Drawing.Point(408, 52);
            this.lbl_BookingID.Name = "lbl_BookingID";
            this.lbl_BookingID.Size = new System.Drawing.Size(63, 13);
            this.lbl_BookingID.TabIndex = 32;
            this.lbl_BookingID.Text = "Booking ID:";
            // 
            // txt_BookingID
            // 
            this.txt_BookingID.Location = new System.Drawing.Point(504, 49);
            this.txt_BookingID.Name = "txt_BookingID";
            this.txt_BookingID.ReadOnly = true;
            this.txt_BookingID.Size = new System.Drawing.Size(100, 20);
            this.txt_BookingID.TabIndex = 31;
            // 
            // lbl_BookingName
            // 
            this.lbl_BookingName.AutoSize = true;
            this.lbl_BookingName.Location = new System.Drawing.Point(408, 78);
            this.lbl_BookingName.Name = "lbl_BookingName";
            this.lbl_BookingName.Size = new System.Drawing.Size(80, 13);
            this.lbl_BookingName.TabIndex = 30;
            this.lbl_BookingName.Text = "Booking Name:";
            // 
            // txt_BookingName
            // 
            this.txt_BookingName.Location = new System.Drawing.Point(504, 75);
            this.txt_BookingName.Name = "txt_BookingName";
            this.txt_BookingName.Size = new System.Drawing.Size(100, 20);
            this.txt_BookingName.TabIndex = 29;
            // 
            // lbl_BookingDate
            // 
            this.lbl_BookingDate.AutoSize = true;
            this.lbl_BookingDate.Location = new System.Drawing.Point(610, 52);
            this.lbl_BookingDate.Name = "lbl_BookingDate";
            this.lbl_BookingDate.Size = new System.Drawing.Size(75, 13);
            this.lbl_BookingDate.TabIndex = 41;
            this.lbl_BookingDate.Text = "Booking Date:";
            // 
            // dtp_BookingDate
            // 
            this.dtp_BookingDate.Location = new System.Drawing.Point(613, 75);
            this.dtp_BookingDate.Name = "dtp_BookingDate";
            this.dtp_BookingDate.Size = new System.Drawing.Size(140, 20);
            this.dtp_BookingDate.TabIndex = 42;
            // 
            // frm_bookings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dtp_BookingDate);
            this.Controls.Add(this.lbl_BookingDate);
            this.Controls.Add(this.lvw_Bookings);
            this.Controls.Add(this.lvw_BookingActivities);
            this.Controls.Add(this.btn_AddBooking);
            this.Controls.Add(this.lbl_Bookings);
            this.Controls.Add(this.lbl_BookingActivities);
            this.Controls.Add(this.btn_ConfirmChanges);
            this.Controls.Add(this.btn_CancelChanges);
            this.Controls.Add(this.lbl_EditDepartment);
            this.Controls.Add(this.lbl_BookingID);
            this.Controls.Add(this.txt_BookingID);
            this.Controls.Add(this.lbl_BookingName);
            this.Controls.Add(this.txt_BookingName);
            this.Name = "frm_bookings";
            this.Text = "frm_bookings";
            this.Load += new System.EventHandler(this.frm_bookings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvw_Bookings;
        private System.Windows.Forms.ColumnHeader clm_CompanyID;
        private System.Windows.Forms.ColumnHeader clm_CompanyName;
        private System.Windows.Forms.ListView lvw_BookingActivities;
        private System.Windows.Forms.ColumnHeader clm_DepartmentID;
        private System.Windows.Forms.ColumnHeader clm_DepartmentName;
        private System.Windows.Forms.Button btn_AddBooking;
        private System.Windows.Forms.Label lbl_Bookings;
        private System.Windows.Forms.Label lbl_BookingActivities;
        private System.Windows.Forms.Button btn_ConfirmChanges;
        private System.Windows.Forms.Button btn_CancelChanges;
        private System.Windows.Forms.Label lbl_EditDepartment;
        private System.Windows.Forms.Label lbl_BookingID;
        private System.Windows.Forms.TextBox txt_BookingID;
        private System.Windows.Forms.Label lbl_BookingName;
        private System.Windows.Forms.TextBox txt_BookingName;
        private System.Windows.Forms.Label lbl_BookingDate;
        private System.Windows.Forms.DateTimePicker dtp_BookingDate;
    }
}