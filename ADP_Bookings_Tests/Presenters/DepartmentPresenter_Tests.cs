//16007006 Andrew Patton
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Testing inclusions
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac.Extras.Moq;
using Moq;

// Required due to folder/namespace structure
using ADP_Bookings.Views;
using ADP_Bookings.Models;

namespace ADP_Bookings.Presenters.Tests
{
    [TestClass()]
    public class DepartmentPresenter_Tests
    {
        // ********************************************************************************
        // Test Methods *******************************************************************
        // ********************************************************************************

        // Test ID: DP1
        // Purpose: Test the presenter's response to the "Add Department" button being pressed
        [TestMethod()]
        public void LoadNewRecord_Test()
        {
            #region Arrange

            // Create mock layers
            var mockView = GetMockView();
            var mockModel = GetMockModel();

            // Create presenter to be tested, inject our mock view & model
            DepartmentPresenter presenter = new DepartmentPresenter(mockView.Object, mockModel.Object, 1);
            
            // Set the screen's CurrentDepartmentName to "Foobar"
            // Our presenter will reset this to an empty string
            mockView.Object.CurrentDepartmentName = "FooBar";            

            #endregion Arrange

            #region Act            

            //Invoke the call being tested
            presenter.btn_AddDepartment_Click();

            #endregion Act

            #region Assert
            // Request evaluation of screen properties
            Dictionary<string, object> expected = new Dictionary<string, object>
            {
                { "DepartmentID",   "0" },
                { "DepartmentName", "" },
                { "CurrentDepartment_Enabled", true }
            };
            EvaluateScreen(expected, mockView);
            #endregion Assert
        }

        // Test ID: DP2
        // Purpose: Test the presenter's response to a new item being selected
        [TestMethod()]
        public void LoadRecord_Test()
        {
            #region Arrange

            // Create mock layers
            var mockView = GetMockView();
            var mockModel = GetMockModel();

            // Create presenter to be tested, inject our mock view & model
            DepartmentPresenter presenter = new DepartmentPresenter(mockView.Object, mockModel.Object, 1);

            // Keep a copy of the facade records list our presenter will be working with
            List<Department> records = Mock_GetAllDepartments().ToList();

            // Declare which record shall be selected from the list
            int index = 2;

            // Keep a copy of which record the presenter SHOULD select & push to view
            Department department = records[index];

            #endregion Arrange

            /**************************************************/

            #region Act

            // Select our test case company, mimicking the user selecting it from a ListView
            presenter.lvw_Departments_SelectedIndexChanged(new int[] { index });

            #endregion Act

            /**************************************************/

            #region Assert

            // Declare expected values
            Dictionary<string, object> expected = new Dictionary<string, object>
            {
                { "DepartmentID",   department.DepartmentID.ToString() },
                { "DepartmentName", department.Name },
                { "CurrentDepartment_Enabled", true }
            };

            // Request evaluation of screen properties
            EvaluateScreen(expected, mockView);

            #endregion Assert
        }

        // Test ID: DP3
        // Purpose: Test the presenter's response to the "Confirm" button being pressed
        [TestMethod()]
        [DataRow(true)]  // DP3a | Changes pending
        [DataRow(false)] // DP3b | No changes pending
        public void SaveRecord_Test(bool makeChanges)
        {
            using (var mock = AutoMock.GetLoose())
            {
                #region Arrange
                // Create mock layers
                var mockView = GetMockView();
                var mockModel = GetMockModel();

                // Create presenter to be tested, inject our mock model
                DepartmentPresenter presenter = new DepartmentPresenter(mockView.Object, mockModel.Object, 1);

                // Keep a copy of the facade records list our presenter will be working with
                List<Department> records = Mock_GetAllDepartments().ToList();

                // Declare which record shall be selected from the list
                int index = 0;

                // Keep a copy of which record the presenter SHOULD select & push to view
                Department department = records[index];

                // Select a company record
                presenter.lvw_Departments_SelectedIndexChanged(new int[] { index });

                // Tell the presenter there are changes pending
                if (makeChanges)
                    presenter.NewChangePending();

                #endregion Arrange

                /**************************************************/

                #region Act                

                // Save changes
                presenter.btn_ConfirmChanges_Click();

                #endregion Act

                /**************************************************/

                #region Assert               

                // Ensure the presenter told the model to save the record, only if changes were pending
                if (makeChanges)
                    mockModel.Verify(x => x.SaveDepartment(It.Is<Department>(d => d.DepartmentID == department.DepartmentID)), Times.Once);
                else
                    mockModel.Verify(x => x.SaveDepartment(It.IsAny<Department>()), Times.Never);

                // Request evaluation of screen properties
                Dictionary<string, object> expected = GetClearedScreen();
                EvaluateScreen(expected, mockView);

                #endregion Assert
            }
        }

        // Test ID: DP4
        // Purpose: Test the presenter's response to the "Delete Department" button being pressed
        [TestMethod()]
        [DataRow(true)]  //DP4a | Record is currently selected
        [DataRow(false)] //DP4b | No record currently selected
        public void DeleteRecord_Test(bool recordSelected)
        {
            using (var mock = AutoMock.GetLoose())
            {
                #region Arrange

                // Create mock layers
                var mockView = GetMockView();
                var mockModel = GetMockModel();

                // Create presenter to be tested, inject our mock model
                DepartmentPresenter presenter = new DepartmentPresenter(mockView.Object, mockModel.Object, 1);

                // Declare that the user will select "Yes" on the confirmation messagebox
                mockView.Setup(x => x.ShowMessageBox(It.IsAny<string>(), It.IsAny<string>(),
                                                     It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                                                     .Returns(DialogResult.Yes);

                // Keep a copy of the facade records list our presenter will be working with
                List<Department> records = Mock_GetAllDepartments().ToList();

                // Declare which record shall be selected from the list
                int index = 0;

                // Keep a copy of which record the presenter SHOULD select & push to view
                Department department = records[index];

                // Select our test case company, mimicking the user selecting it from a ListView
                if (recordSelected)
                    presenter.lvw_Departments_SelectedIndexChanged(new int[] { index });

                #endregion Arrange

                /**************************************************/

                #region Act

                //Invoke the call being tested
                presenter.btn_DeleteDepartment_Click();

                #endregion Act

                /**************************************************/

                #region Assert

                // Ensure the presenter told the model to delete the record, only if a record was selected
                // Only passes if the CORRECT company was sent to the model
                if (recordSelected)
                {
                    mockModel.Verify(r => r.DeleteDepartment(It.Is<Department>(d => d.DepartmentID == department.DepartmentID)), Times.Once);
                }
                else
                {
                    mockModel.Verify(r => r.DeleteDepartment(It.IsAny<Department>()), Times.Never);
                    mockView.Verify(r => r.ShowMessageBox(It.IsAny<string>(), It.IsAny<string>(),
                                                          It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()), Times.Once);
                }

                // Request evaluation of screen properties
                Dictionary<string, object> expected = GetClearedScreen();
                EvaluateScreen(expected, mockView);

                #endregion Assert
            }
        }

        // ********************************************************************************
        // Utility Functions **************************************************************
        // ******************************************************************************** 

        // Evaluate the current properties on screen following test - entire function is "Assert"
        // NOTE: Reduces code duplication, but there is dispute 
        //       as to whether Unit Tests should be intra-dependant
        // NOTE: VS throws a warning about TestMethods using parameters.
        //       There's probably a much better way of doing this
        void EvaluateScreen(Dictionary<string, object> expected, Moq.Mock<IDepartmentGUI> screen)
        {
            // Declare actual values
            Dictionary<string, object> actual = new Dictionary<string, object>
            {
                { "DepartmentID",   screen.Object.CurrentDepartmentID },
                { "DepartmentName", screen.Object.CurrentDepartmentName },
                { "CurrentDepartment_Enabled", screen.Object.CurrentDepartment_Enabled }
            };

            // Compare expected values with actual values
            foreach (KeyValuePair<string, object> entry in actual)
            {
                // Only test properties if the calling test has included them
                if (expected.ContainsKey(entry.Key))
                    Assert.AreEqual(expected[entry.Key], entry.Value);
            }
        }
        // Get a dictionary of screen properties for when the presenter 
        // is expected to have cleared the view
        // Purely to reduce code duplication
        Dictionary<string, object> GetClearedScreen()
        {
            return new Dictionary<string, object>
            {
                { "DepartmentID",   "" },
                { "DepartmentName", "" },
                { "CurrentDepartment_Enabled", false }
            };
        }

        // ********************************************************************************
        // View Layer Mocking *************************************************************
        // ********************************************************************************

        // Initialises the mock screen to what mimic a form that has just loaded
        Mock<IDepartmentGUI> GetMockView()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var screen = mock.Mock<IDepartmentGUI>();
                screen.SetupAllProperties();
                // Screen mock needs to provide facade for properties changing
                // so the test can check they are correctly assigned
                screen.SetupProperty(x => x.CurrentDepartmentID)
                      .SetReturnsDefault("");
                screen.SetupProperty(x => x.CurrentDepartmentName)
                      .SetReturnsDefault("");
                screen.SetupProperty(x => x.CurrentDepartment_Enabled)
                      .SetReturnsDefault(false);
                screen.SetupGet(x => x.DepartmentList)
                      .Returns(new ListView.ListViewItemCollection(new ListView()) { new ListViewItem() });
                screen.SetupGet(x => x.CurrentDepartmentBookings)
                      .Returns(new ListView.ListViewItemCollection(new ListView()) { new ListViewItem() });

                return screen;
            }
        }

        // ********************************************************************************
        // Model Layer Mocking ************************************************************
        // ********************************************************************************

        // This test class only focuses on verifying the presenter logic is correct.
        // The below functions provide a false model layer. Presenter tests results
        // are therefore solely tied to the integrity of the presenter logic.

        // Create mock model
        Mock<IDepartmentModel> GetMockModel()
        {
            var mockModel = new Mock<IDepartmentModel>();
            mockModel.Setup(r => r.GetAllDepartments()).Returns(Mock_GetAllDepartments().ToList());
            mockModel.Setup(r => r.GetAllDepartmentsFrom(It.IsAny<Company>())).Returns(Mock_GetAllDepartments().ToList());
            mockModel.Setup(r => r.FindDepartment(It.IsAny<int>())).Returns<int>((id) => Mock_GetDepartment(id));
            mockModel.Setup(r => r.SaveDepartment(It.IsAny<Department>())).Verifiable();
            mockModel.Setup(r => r.DeleteDepartment(It.IsAny<Department>())).Verifiable();
            mockModel.Setup(r => r.FindCompany(It.IsAny<int>())).Returns<int>((id) => Mock_GetCompany(id));
            return mockModel;
        }

        // Mock Company Data
        IQueryable<Company> Mock_GetAllCompanies()
        {
            return new List<Company>
            {
                new Company { CompanyID = 1, Name = "Asda"   },
                new Company { CompanyID = 2, Name = "Boots"  },
                new Company { CompanyID = 3, Name = "Costco" },
                new Company { CompanyID = 4, Name = "Dixons" }
            }.AsQueryable();
        }
        Company Mock_GetCompany(int id) => Mock_GetAllCompanies().Where(c => c.CompanyID == id).FirstOrDefault();

        // Mock Department Data
        IQueryable<Department> Mock_GetAllDepartments()
        {
            return new List<Department>
            {
                new Department { DepartmentID = 1, Name = "HR"   },
                new Department { DepartmentID = 2, Name = "Legal"  },
                new Department { DepartmentID = 3, Name = "Marketing" },
                new Department { DepartmentID = 4, Name = "IT" }
            }.AsQueryable();
        }
        Department Mock_GetDepartment(int id) => Mock_GetAllDepartments().Where(d => d.DepartmentID == id).FirstOrDefault();
    }


}