namespace ADP_Bookings.Forms
{
    partial class frm_activities
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
            this.nud_ActivityCost = new System.Windows.Forms.NumericUpDown();
            this.lbl_BookingCost = new System.Windows.Forms.Label();
            this.btn_EditActivities = new System.Windows.Forms.Button();
            this.lbl_divider = new System.Windows.Forms.Label();
            this.lvw_ActivityList = new System.Windows.Forms.ListView();
            this.clm_ActivityID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_ActivityName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_ActivityCost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_ActivityNotes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_AddActivity = new System.Windows.Forms.Button();
            this.lbl_Activities = new System.Windows.Forms.Label();
            this.lbl_ActivityNotes = new System.Windows.Forms.Label();
            this.btn_ConfirmChanges = new System.Windows.Forms.Button();
            this.btn_CancelChanges = new System.Windows.Forms.Button();
            this.lbl_EditActivity = new System.Windows.Forms.Label();
            this.lbl_ActivityID = new System.Windows.Forms.Label();
            this.txt_ActivityID = new System.Windows.Forms.TextBox();
            this.lbl_ActivityName = new System.Windows.Forms.Label();
            this.txt_ActivityName = new System.Windows.Forms.TextBox();
            this.rtx_ActivityNotes = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ActivityCost)).BeginInit();
            this.SuspendLayout();
            // 
            // nud_ActivityCost
            // 
            this.nud_ActivityCost.DecimalPlaces = 2;
            this.nud_ActivityCost.Location = new System.Drawing.Point(691, 75);
            this.nud_ActivityCost.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_ActivityCost.Name = "nud_ActivityCost";
            this.nud_ActivityCost.Size = new System.Drawing.Size(97, 20);
            this.nud_ActivityCost.TabIndex = 67;
            this.nud_ActivityCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nud_ActivityCost.ThousandsSeparator = true;
            // 
            // lbl_BookingCost
            // 
            this.lbl_BookingCost.AutoSize = true;
            this.lbl_BookingCost.Location = new System.Drawing.Point(610, 78);
            this.lbl_BookingCost.Name = "lbl_BookingCost";
            this.lbl_BookingCost.Size = new System.Drawing.Size(68, 13);
            this.lbl_BookingCost.TabIndex = 66;
            this.lbl_BookingCost.Text = "Activity Cost:";
            // 
            // btn_EditActivities
            // 
            this.btn_EditActivities.Location = new System.Drawing.Point(411, 241);
            this.btn_EditActivities.Name = "btn_EditActivities";
            this.btn_EditActivities.Size = new System.Drawing.Size(377, 25);
            this.btn_EditActivities.TabIndex = 65;
            this.btn_EditActivities.Text = "Edit Activities";
            this.btn_EditActivities.UseVisualStyleBackColor = true;
            // 
            // lbl_divider
            // 
            this.lbl_divider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_divider.Location = new System.Drawing.Point(381, 17);
            this.lbl_divider.Name = "lbl_divider";
            this.lbl_divider.Size = new System.Drawing.Size(2, 300);
            this.lbl_divider.TabIndex = 64;
            // 
            // lvw_ActivityList
            // 
            this.lvw_ActivityList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clm_ActivityID,
            this.clm_ActivityName,
            this.clm_ActivityCost,
            this.clm_ActivityNotes});
            this.lvw_ActivityList.HideSelection = false;
            this.lvw_ActivityList.Location = new System.Drawing.Point(12, 37);
            this.lvw_ActivityList.Name = "lvw_ActivityList";
            this.lvw_ActivityList.Size = new System.Drawing.Size(339, 235);
            this.lvw_ActivityList.TabIndex = 60;
            this.lvw_ActivityList.UseCompatibleStateImageBehavior = false;
            this.lvw_ActivityList.View = System.Windows.Forms.View.Details;
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
            this.clm_ActivityNotes.Width = 190;
            // 
            // btn_AddActivity
            // 
            this.btn_AddActivity.Location = new System.Drawing.Point(223, 278);
            this.btn_AddActivity.Name = "btn_AddActivity";
            this.btn_AddActivity.Size = new System.Drawing.Size(128, 40);
            this.btn_AddActivity.TabIndex = 59;
            this.btn_AddActivity.Text = "Add New Activity";
            this.btn_AddActivity.UseVisualStyleBackColor = true;
            // 
            // lbl_Activities
            // 
            this.lbl_Activities.AutoSize = true;
            this.lbl_Activities.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Activities.Location = new System.Drawing.Point(134, 17);
            this.lbl_Activities.Name = "lbl_Activities";
            this.lbl_Activities.Size = new System.Drawing.Size(73, 17);
            this.lbl_Activities.TabIndex = 58;
            this.lbl_Activities.Text = "Activities";
            // 
            // lbl_ActivityNotes
            // 
            this.lbl_ActivityNotes.AutoSize = true;
            this.lbl_ActivityNotes.Location = new System.Drawing.Point(408, 103);
            this.lbl_ActivityNotes.Name = "lbl_ActivityNotes";
            this.lbl_ActivityNotes.Size = new System.Drawing.Size(75, 13);
            this.lbl_ActivityNotes.TabIndex = 57;
            this.lbl_ActivityNotes.Text = "Activity Notes:";
            // 
            // btn_ConfirmChanges
            // 
            this.btn_ConfirmChanges.Location = new System.Drawing.Point(660, 277);
            this.btn_ConfirmChanges.Name = "btn_ConfirmChanges";
            this.btn_ConfirmChanges.Size = new System.Drawing.Size(128, 40);
            this.btn_ConfirmChanges.TabIndex = 56;
            this.btn_ConfirmChanges.Text = "Confirm";
            this.btn_ConfirmChanges.UseVisualStyleBackColor = true;
            // 
            // btn_CancelChanges
            // 
            this.btn_CancelChanges.Location = new System.Drawing.Point(411, 278);
            this.btn_CancelChanges.Name = "btn_CancelChanges";
            this.btn_CancelChanges.Size = new System.Drawing.Size(128, 40);
            this.btn_CancelChanges.TabIndex = 55;
            this.btn_CancelChanges.Text = "Cancel";
            this.btn_CancelChanges.UseVisualStyleBackColor = true;
            // 
            // lbl_EditActivity
            // 
            this.lbl_EditActivity.AutoSize = true;
            this.lbl_EditActivity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EditActivity.Location = new System.Drawing.Point(548, 17);
            this.lbl_EditActivity.Name = "lbl_EditActivity";
            this.lbl_EditActivity.Size = new System.Drawing.Size(93, 17);
            this.lbl_EditActivity.TabIndex = 54;
            this.lbl_EditActivity.Text = "Edit Activity";
            // 
            // lbl_ActivityID
            // 
            this.lbl_ActivityID.AutoSize = true;
            this.lbl_ActivityID.Location = new System.Drawing.Point(408, 52);
            this.lbl_ActivityID.Name = "lbl_ActivityID";
            this.lbl_ActivityID.Size = new System.Drawing.Size(58, 13);
            this.lbl_ActivityID.TabIndex = 53;
            this.lbl_ActivityID.Text = "Activity ID:";
            // 
            // txt_ActivityID
            // 
            this.txt_ActivityID.Location = new System.Drawing.Point(504, 49);
            this.txt_ActivityID.Name = "txt_ActivityID";
            this.txt_ActivityID.ReadOnly = true;
            this.txt_ActivityID.Size = new System.Drawing.Size(100, 20);
            this.txt_ActivityID.TabIndex = 52;
            // 
            // lbl_ActivityName
            // 
            this.lbl_ActivityName.AutoSize = true;
            this.lbl_ActivityName.Location = new System.Drawing.Point(408, 78);
            this.lbl_ActivityName.Name = "lbl_ActivityName";
            this.lbl_ActivityName.Size = new System.Drawing.Size(75, 13);
            this.lbl_ActivityName.TabIndex = 51;
            this.lbl_ActivityName.Text = "Activity Name:";
            // 
            // txt_ActivityName
            // 
            this.txt_ActivityName.Location = new System.Drawing.Point(504, 75);
            this.txt_ActivityName.Name = "txt_ActivityName";
            this.txt_ActivityName.Size = new System.Drawing.Size(100, 20);
            this.txt_ActivityName.TabIndex = 50;
            // 
            // rtx_ActivityNotes
            // 
            this.rtx_ActivityNotes.Location = new System.Drawing.Point(411, 119);
            this.rtx_ActivityNotes.Name = "rtx_ActivityNotes";
            this.rtx_ActivityNotes.Size = new System.Drawing.Size(377, 116);
            this.rtx_ActivityNotes.TabIndex = 68;
            this.rtx_ActivityNotes.Text = "";
            // 
            // frm_activities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 330);
            this.Controls.Add(this.rtx_ActivityNotes);
            this.Controls.Add(this.nud_ActivityCost);
            this.Controls.Add(this.lbl_BookingCost);
            this.Controls.Add(this.btn_EditActivities);
            this.Controls.Add(this.lbl_divider);
            this.Controls.Add(this.lvw_ActivityList);
            this.Controls.Add(this.btn_AddActivity);
            this.Controls.Add(this.lbl_Activities);
            this.Controls.Add(this.lbl_ActivityNotes);
            this.Controls.Add(this.btn_ConfirmChanges);
            this.Controls.Add(this.btn_CancelChanges);
            this.Controls.Add(this.lbl_EditActivity);
            this.Controls.Add(this.lbl_ActivityID);
            this.Controls.Add(this.txt_ActivityID);
            this.Controls.Add(this.lbl_ActivityName);
            this.Controls.Add(this.txt_ActivityName);
            this.Name = "frm_activities";
            this.Text = "frm_activities";
            ((System.ComponentModel.ISupportInitialize)(this.nud_ActivityCost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nud_ActivityCost;
        private System.Windows.Forms.Label lbl_BookingCost;
        private System.Windows.Forms.Button btn_EditActivities;
        private System.Windows.Forms.Label lbl_divider;
        private System.Windows.Forms.ListView lvw_ActivityList;
        private System.Windows.Forms.ColumnHeader clm_ActivityID;
        private System.Windows.Forms.ColumnHeader clm_ActivityName;
        private System.Windows.Forms.ColumnHeader clm_ActivityCost;
        private System.Windows.Forms.ColumnHeader clm_ActivityNotes;
        private System.Windows.Forms.Button btn_AddActivity;
        private System.Windows.Forms.Label lbl_Activities;
        private System.Windows.Forms.Label lbl_ActivityNotes;
        private System.Windows.Forms.Button btn_ConfirmChanges;
        private System.Windows.Forms.Button btn_CancelChanges;
        private System.Windows.Forms.Label lbl_EditActivity;
        private System.Windows.Forms.Label lbl_ActivityID;
        private System.Windows.Forms.TextBox txt_ActivityID;
        private System.Windows.Forms.Label lbl_ActivityName;
        private System.Windows.Forms.TextBox txt_ActivityName;
        private System.Windows.Forms.RichTextBox rtx_ActivityNotes;
    }
}