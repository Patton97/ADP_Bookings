//16007006 Andrew Patton
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Project Specific imports
// Required due to folder structure
using ADP_Bookings.Models;
using ADP_Bookings.Views;

namespace ADP_Bookings.Presenters
{
    public abstract class RecordPresenter<T> where T : IRecord
    {
        private IRecordGUI screen;
        protected List<T> records;
        protected T selectedRecord;
        protected bool ChangesPending = false; //Has the user begun editing the record yet

        protected RecordPresenter(IRecordGUI screen)
        {
            this.screen = screen;
            records = new List<T>();
        }

        // ********************************************************************************
        // Abstract Methods ***************************************************************
        // ********************************************************************************        

        //The following methods are left abstract as they require context-specific code
        //but are included to ensure any subclasses MUST include them

        //Initialise form & its components
        public abstract void InitialiseForm();

        //Load record for editing
        protected abstract void LoadRecord(T newRecord);

        //Create new record, load for editing 
        protected abstract void LoadNewRecord();

        //Delete record from database
        protected abstract void DeleteRecord();

        //Save record to database
        protected abstract void SaveRecord();

        //Clear record (from screen) - local only, no DB operations
        protected abstract void ClearCurrentRecord();

        // ********************************************************************************
        // Concrete Methods ***************************************************************
        // ********************************************************************************        

        //Create new record  (requests user approval)
        //NOTE: local, no DB operations
        protected void AddRecord()
        {
            //If a company is already being edited
            if (ChangesPending)
            {
                var confirmResult = screen.ShowMessageBox("Would you like to save your changes?", "Save Changes?",
                                                           MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                //Save changes, add new record
                if (confirmResult == DialogResult.Yes)
                    SaveRecord();

                //DO NOT add new record, continue previous behaviour
                if (confirmResult == DialogResult.Cancel)
                    return;
            }
            //If code has reached this point, either no changes
            //are pending or user has chosen to save/discard them
            LoadNewRecord();
        }

        //Discard any changes made by the user to the current record (requests user approval)
        protected void CancelChanges()
        {
            //If changes have been made, request approval
            if (ChangesPending)
            {
                var confirmResult = screen.ShowMessageBox("All changes will be lost!", "Are you sure?", 
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.No)
                    ClearCurrentRecord();
            }

            //If code reaches this point,  either no changes
            //are pending or user has agreed to discard them
            ClearCurrentRecord();
        }

        //Called whenever the user selects a new record from the list
        protected void SelectRecord(int[] selectedIndices)
        {
            //First, check if company is currently being edited
            if (ChangesPending)
            {
                var confirmResult = screen.ShowMessageBox("Would you like to save your changes?", "Save Changes?", 
                                                          MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                    SaveRecord();
                else if (confirmResult == DialogResult.No)
                    ClearCurrentRecord();
                else if (confirmResult == DialogResult.Cancel)
                    return; //Exit function, resume previous behaviour
            }
            //If code reaches this point, no company is currently being edited

            //When using ListView with FullRowSelect, if the user changes rows
            //the list view first deselects the old row, then selects the new row
            //Therefore, we ignore the first 'dud' call where no rows are selected
            if (selectedIndices.Length <= 0)
                return; //Exit function, resume previous behaviour

            //ListView also allows for multiple row selection. 
            //If this is the case, the company details display is wiped to avoid ambiguity
            if (selectedIndices.Length > 1)
                ClearCurrentRecord();
            else
                LoadRecord(records[selectedIndices[0]]);
        }

        //Completely close the current form window (requests user approval)
        protected void CloseForm(FormClosingEventArgs e)
        {
            //If no changes have been made, close form
            if (!ChangesPending)
                return;

            //If changes have been made, ask how to proceed
            var confirmResult = screen.ShowMessageBox("Would you like to save your changes?", "Save Changes?",
                                                      MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            //Save changes, close form
            if (confirmResult == DialogResult.Yes)
                SaveRecord();
            //Discard changes, close form
            if (confirmResult == DialogResult.No)
                ClearCurrentRecord();
            //DO NOT close form, continue previous behaviour
            if (confirmResult == DialogResult.Cancel)
                e.Cancel = true;
        }

        //Provides public access to flip the changer tracker bool, but only ever to true
        //only the presenter itself can reset the tracker to false
        public void NewChangePending() => ChangesPending = true;
    }
}
