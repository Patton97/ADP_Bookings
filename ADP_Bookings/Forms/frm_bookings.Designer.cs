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
            this.clm_BookingID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_BookingName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_BookingDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_BookingNumAttendees = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_BookingEstimatedCost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_BookingActualCost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvw_BookingActivities = new System.Windows.Forms.ListView();
            this.clm_ActivityID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_ActivityName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_ActivityCost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_ActivityNotes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_AddBooking = new System.Windows.Forms.Button();
            this.lbl_Bookings = new System.Windows.Forms.Label();
            this.lbl_BookingActivities = new System.Windows.Forms.Label();
            this.btn_ConfirmChanges = new System.Windows.Forms.Button();
            this.btn_CancelChanges = new System.Windows.Forms.Button();
            this.lbl_EditBooking = new System.Windows.Forms.Label();
            this.lbl_BookingID = new System.Windows.Forms.Label();
            this.txt_BookingID = new System.Windows.Forms.TextBox();
            this.lbl_BookingName = new System.Windows.Forms.Label();
            this.txt_BookingName = new System.Windows.Forms.TextBox();
            this.lbl_BookingDate = new System.Windows.Forms.Label();
            this.dtp_BookingDate = new System.Windows.Forms.DateTimePicker();
            this.lbl_divider = new System.Windows.Forms.Label();
            this.btn_EditBookings = new System.Windows.Forms.Button();
            this.lbl_BookingCost = new System.Windows.Forms.Label();
            this.nud_BookingCost = new System.Windows.Forms.NumericUpDown();
            this.btn_DeleteBooking = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nud_BookingCost)).BeginInit();
            this.SuspendLayout();
            // 
            // lvw_Bookings
            // 
            this.lvw_Bookings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clm_BookingID,
            this.clm_BookingName,
            this.clm_BookingDate,
            this.clm_BookingNumAttendees,
            this.clm_BookingEstimatedCost,
            this.clm_BookingActualCost});
            this.lvw_Bookings.FullRowSelect = true;
            this.lvw_Bookings.HideSelection = false;
            this.lvw_Bookings.Location = new System.Drawing.Point(12, 52);
            this.lvw_Bookings.Name = "lvw_Bookings";
            this.lvw_Bookings.Size = new System.Drawing.Size(339, 214);
            this.lvw_Bookings.TabIndex = 40;
            this.lvw_Bookings.UseCompatibleStateImageBehavior = false;
            this.lvw_Bookings.View = System.Windows.Forms.View.Details;
            this.lvw_Bookings.SelectedIndexChanged += new System.EventHandler(this.lvw_Bookings_SelectedIndexChanged);
            // 
            // clm_BookingID
            // 
            this.clm_BookingID.Text = "ID";
            this.clm_BookingID.Width = 28;
            // 
            // clm_BookingName
            // 
            this.clm_BookingName.Text = "Name";
            // 
            // clm_BookingDate
            // 
            this.clm_BookingDate.Text = "Date";
            // 
            // clm_BookingNumAttendees
            // 
            this.clm_BookingNumAttendees.Text = "Party Size";
            // 
            // clm_BookingEstimatedCost
            // 
            this.clm_BookingEstimatedCost.Text = "Cost (Est)";
            // 
            // clm_BookingActualCost
            // 
            this.clm_BookingActualCost.Text = "Cost (Final)";
            this.clm_BookingActualCost.Width = 66;
            // 
            // lvw_BookingActivities
            // 
            this.lvw_BookingActivities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clm_ActivityID,
            this.clm_ActivityName,
            this.clm_ActivityCost,
            this.clm_ActivityNotes});
            this.lvw_BookingActivities.HideSelection = false;
            this.lvw_BookingActivities.Location = new System.Drawing.Point(411, 120);
            this.lvw_BookingActivities.Name = "lvw_BookingActivities";
            this.lvw_BookingActivities.Size = new System.Drawing.Size(377, 115);
            this.lvw_BookingActivities.TabIndex = 39;
            this.lvw_BookingActivities.UseCompatibleStateImageBehavior = false;
            this.lvw_BookingActivities.View = System.Windows.Forms.View.Details;
            // 
            // clm_ActivityID
            // 
            this.clm_ActivityID.Text = "ID";
            this.clm_ActivityID.Width = 25;
            // 
            // clm_ActivityName
            // 
            this.clm_ActivityName.Text = "Name";
            // 
            // clm_ActivityCost
            // 
            this.clm_ActivityCost.Text = "Cost";
            // 
            // clm_ActivityNotes
            // 
            this.clm_ActivityNotes.Text = "Notes";
            this.clm_ActivityNotes.Width = 226;
            // 
            // btn_AddBooking
            // 
            this.btn_AddBooking.Location = new System.Drawing.Point(223, 278);
            this.btn_AddBooking.Name = "btn_AddBooking";
            this.btn_AddBooking.Size = new System.Drawing.Size(128, 40);
            this.btn_AddBooking.TabIndex = 38;
            this.btn_AddBooking.Text = "Add New Booking";
            this.btn_AddBooking.UseVisualStyleBackColor = true;
            this.btn_AddBooking.Click += new System.EventHandler(this.btn_AddBooking_Click);
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
            this.btn_ConfirmChanges.Click += new System.EventHandler(this.btn_ConfirmChanges_Click);
            // 
            // btn_CancelChanges
            // 
            this.btn_CancelChanges.Location = new System.Drawing.Point(411, 278);
            this.btn_CancelChanges.Name = "btn_CancelChanges";
            this.btn_CancelChanges.Size = new System.Drawing.Size(128, 40);
            this.btn_CancelChanges.TabIndex = 34;
            this.btn_CancelChanges.Text = "Cancel";
            this.btn_CancelChanges.UseVisualStyleBackColor = true;
            this.btn_CancelChanges.Click += new System.EventHandler(this.btn_CancelChanges_Click);
            // 
            // lbl_EditBooking
            // 
            this.lbl_EditBooking.AutoSize = true;
            this.lbl_EditBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EditBooking.Location = new System.Drawing.Point(548, 17);
            this.lbl_EditBooking.Name = "lbl_EditBooking";
            this.lbl_EditBooking.Size = new System.Drawing.Size(99, 17);
            this.lbl_EditBooking.TabIndex = 33;
            this.lbl_EditBooking.Text = "Edit Booking";
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
            this.lbl_BookingDate.Location = new System.Drawing.Point(610, 51);
            this.lbl_BookingDate.Name = "lbl_BookingDate";
            this.lbl_BookingDate.Size = new System.Drawing.Size(75, 13);
            this.lbl_BookingDate.TabIndex = 41;
            this.lbl_BookingDate.Text = "Booking Date:";
            // 
            // dtp_BookingDate
            // 
            this.dtp_BookingDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_BookingDate.Location = new System.Drawing.Point(691, 49);
            this.dtp_BookingDate.Name = "dtp_BookingDate";
            this.dtp_BookingDate.Size = new System.Drawing.Size(97, 20);
            this.dtp_BookingDate.TabIndex = 42;
            // 
            // lbl_divider
            // 
            this.lbl_divider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_divider.Location = new System.Drawing.Point(381, 17);
            this.lbl_divider.Name = "lbl_divider";
            this.lbl_divider.Size = new System.Drawing.Size(2, 300);
            this.lbl_divider.TabIndex = 43;
            // 
            // btn_EditBookings
            // 
            this.btn_EditBookings.Location = new System.Drawing.Point(411, 241);
            this.btn_EditBookings.Name = "btn_EditBookings";
            this.btn_EditBookings.Size = new System.Drawing.Size(377, 25);
            this.btn_EditBookings.TabIndex = 46;
            this.btn_EditBookings.Text = "Edit Activities";
            this.btn_EditBookings.UseVisualStyleBackColor = true;
            this.btn_EditBookings.Click += new System.EventHandler(this.btn_EditActivities_Click);
            // 
            // lbl_BookingCost
            // 
            this.lbl_BookingCost.AutoSize = true;
            this.lbl_BookingCost.Location = new System.Drawing.Point(610, 78);
            this.lbl_BookingCost.Name = "lbl_BookingCost";
            this.lbl_BookingCost.Size = new System.Drawing.Size(73, 13);
            this.lbl_BookingCost.TabIndex = 47;
            this.lbl_BookingCost.Text = "Booking Cost:";
            // 
            // nud_BookingCost
            // 
            this.nud_BookingCost.DecimalPlaces = 2;
            this.nud_BookingCost.Location = new System.Drawing.Point(691, 75);
            this.nud_BookingCost.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_BookingCost.Name = "nud_BookingCost";
            this.nud_BookingCost.Size = new System.Drawing.Size(97, 20);
            this.nud_BookingCost.TabIndex = 49;
            this.nud_BookingCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nud_BookingCost.ThousandsSeparator = true;
            this.nud_BookingCost.Click += new System.EventHandler(this.nud_BookingCost_Click);
            this.nud_BookingCost.Enter += new System.EventHandler(this.nud_BookingCost_Enter);
            // 
            // btn_DeleteBooking
            // 
            this.btn_DeleteBooking.Location = new System.Drawing.Point(12, 278);
            this.btn_DeleteBooking.Name = "btn_DeleteBooking";
            this.btn_DeleteBooking.Size = new System.Drawing.Size(128, 40);
            this.btn_DeleteBooking.TabIndex = 50;
            this.btn_DeleteBooking.Text = "Delete Booking";
            this.btn_DeleteBooking.UseVisualStyleBackColor = true;
            this.btn_DeleteBooking.Click += new System.EventHandler(this.btn_DeleteBooking_Click);
            // 
            // frm_bookings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 330);
            this.Controls.Add(this.btn_DeleteBooking);
            this.Controls.Add(this.nud_BookingCost);
            this.Controls.Add(this.lbl_BookingCost);
            this.Controls.Add(this.btn_EditBookings);
            this.Controls.Add(this.lbl_divider);
            this.Controls.Add(this.dtp_BookingDate);
            this.Controls.Add(this.lbl_BookingDate);
            this.Controls.Add(this.lvw_Bookings);
            this.Controls.Add(this.lvw_BookingActivities);
            this.Controls.Add(this.btn_AddBooking);
            this.Controls.Add(this.lbl_Bookings);
            this.Controls.Add(this.lbl_BookingActivities);
            this.Controls.Add(this.btn_ConfirmChanges);
            this.Controls.Add(this.btn_CancelChanges);
            this.Controls.Add(this.lbl_EditBooking);
            this.Controls.Add(this.lbl_BookingID);
            this.Controls.Add(this.txt_BookingID);
            this.Controls.Add(this.lbl_BookingName);
            this.Controls.Add(this.txt_BookingName);
            this.Name = "frm_bookings";
            this.Text = "ADP > Companies > Departments > Bookings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_bookings_FormClosing);
            this.Load += new System.EventHandler(this.frm_bookings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nud_BookingCost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvw_Bookings;
        private System.Windows.Forms.ColumnHeader clm_BookingID;
        private System.Windows.Forms.ColumnHeader clm_BookingName;
        private System.Windows.Forms.ListView lvw_BookingActivities;
        private System.Windows.Forms.ColumnHeader clm_ActivityID;
        private System.Windows.Forms.ColumnHeader clm_ActivityName;
        private System.Windows.Forms.Button btn_AddBooking;
        private System.Windows.Forms.Label lbl_Bookings;
        private System.Windows.Forms.Label lbl_BookingActivities;
        private System.Windows.Forms.Button btn_ConfirmChanges;
        private System.Windows.Forms.Button btn_CancelChanges;
        private System.Windows.Forms.Label lbl_EditBooking;
        private System.Windows.Forms.Label lbl_BookingID;
        private System.Windows.Forms.TextBox txt_BookingID;
        private System.Windows.Forms.Label lbl_BookingName;
        private System.Windows.Forms.TextBox txt_BookingName;
        private System.Windows.Forms.Label lbl_BookingDate;
        private System.Windows.Forms.DateTimePicker dtp_BookingDate;
        private System.Windows.Forms.Label lbl_divider;
        private System.Windows.Forms.Button btn_EditBookings;
        private System.Windows.Forms.ColumnHeader clm_BookingDate;
        private System.Windows.Forms.ColumnHeader clm_BookingEstimatedCost;
        private System.Windows.Forms.ColumnHeader clm_BookingActualCost;
        private System.Windows.Forms.ColumnHeader clm_BookingNumAttendees;
        private System.Windows.Forms.ColumnHeader clm_ActivityCost;
        private System.Windows.Forms.ColumnHeader clm_ActivityNotes;
        private System.Windows.Forms.Label lbl_BookingCost;
        private System.Windows.Forms.NumericUpDown nud_BookingCost;
        private System.Windows.Forms.Button btn_DeleteBooking;
    }
}