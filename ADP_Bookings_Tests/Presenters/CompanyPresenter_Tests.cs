using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADP_Bookings.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;

//Enable mocking of views/forms
using System.Windows.Forms;
using ADP_Bookings.Forms;
using ADP_Bookings.Views;
using ADP_Bookings.Models;//
using Moq;
using System.Data.Entity;

namespace ADP_Bookings.Presenters.Tests
{
    [TestClass()]
    public class CompanyPresenter_Tests
    {
        // ********************************************************************************
        // Test Methods *******************************************************************
        // ********************************************************************************

        // Test ID: CP1
        // Purpose: Test the presenter's response to the "Add Company" button being pressed
        [TestMethod()]
        public void LoadNewRecord_Test()
        {
            #region Arrange

            // Create mock layers
            var mockView = GetMockView();
            var mockModel = GetMockModel();

            // Set the screen's CurrentCompanyName to "Foobar"
            // Our presenter will reset this to an empty string
            mockView.Object.CurrentCompanyName = "FooBar";

            // Create presenter to be tested, inject our mock view & model
            CompanyPresenter presenter = new CompanyPresenter(mockView.Object, mockModel.Object);

            #endregion Arrange

            /**************************************************/

            #region Act            

            //Invoke the call being tested
            presenter.btn_AddCompany_Click();
            #endregion Act

            /**************************************************/

            #region Assert
            // Request evaluation of screen properties
            Dictionary<string, object> expected = new Dictionary<string, object>
            {
                { "CompanyID",   "0" },
                { "CompanyName", "" },
                { "CurrentCompany_Enabled", true }
            };
            EvaluateScreen(expected, mockView);
            #endregion Assert
        }

        // Test ID: CP2
        // Purpose: Test the presenter's response to a new item being selected
        [TestMethod()]
        public void LoadRecord_Test()
        {
            #region Arrange

            // Create mock layers
            var mockView = GetMockView();
            var mockModel = GetMockModel();

            // Create presenter to be tested, inject our mock view & model
            CompanyPresenter presenter = new CompanyPresenter(mockView.Object, mockModel.Object);

            // Keep a copy of the facade records list our presenter will be working with
            List<Company> records = Mock_GetAllCompanies().ToList();

            // Declare which record shall be selected from the list
            int index = 2;

            // Keep a copy of which record the presenter SHOULD select & push to view
            Company company = records[index];

            #endregion Arrange

            /**************************************************/

            #region Act

            // Select our test case company, mimicking the user selecting it from a ListView
            presenter.lvw_companies_SelectedIndexChanged(new int[] { index });

            #endregion Act

            /**************************************************/

            #region Assert

            // Declare expected values
            Dictionary<string, object> expected = new Dictionary<string, object>
            {
                { "CompanyID",   company.CompanyID.ToString() },
                { "CompanyName", company.Name },
                { "CurrentCompany_Enabled", true }
            };

            // Request evaluation of screen properties
            EvaluateScreen(expected, mockView);

            #endregion Assert
        }

        // Test ID: CP3
        // Purpose: Test the presenter's response to the "Confirm" button being pressed
        [TestMethod()]
        [DataRow(true)]  // CP3a | Changes pending
        [DataRow(false)] // CP3b | No changes pending
        public void SaveRecord_Test(bool makeChanges)
        {
            using (var mock = AutoMock.GetLoose())
            {
                #region Arrange
                // Create mock layers
                var mockView = GetMockView();
                var mockModel = GetMockModel();

                // Create presenter to be tested, inject our mock model
                CompanyPresenter presenter = new CompanyPresenter(mockView.Object, mockModel.Object);

                // Keep a copy of the facade records list our presenter will be working with
                List<Company> records = Mock_GetAllCompanies().ToList();

                // Declare which record shall be selected from the list
                int index = 0;

                // Keep a copy of which record the presenter SHOULD select & push to view
                Company company = records[index];

                // Select a company record
                presenter.lvw_companies_SelectedIndexChanged(new int[] { index });

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
                    mockModel.Verify(x => x.SaveCompany(It.Is<Company>(c=>c.CompanyID==company.CompanyID)), Times.Once);                 
                else
                    mockModel.Verify(x => x.SaveCompany(It.IsAny<Company>()), Times.Never);

                // Request evaluation of screen properties
                Dictionary<string, object> expected = GetClearedScreen();
                EvaluateScreen(expected, mockView);

                #endregion Assert
            }
        }

        // Test ID: CP4
        // Purpose: Test the presenter's response to the "Delete Company" button being pressed
        [TestMethod()]
        [DataRow(true)]  //CP4a | Record is currently selected
        [DataRow(false)] //CP4b | No record currently selected
        public void DeleteRecord_Test(bool recordSelected)
        {
            using (var mock = AutoMock.GetLoose())
            {
                #region Arrange

                // Create mock layers
                var mockView = GetMockView();
                var mockModel = GetMockModel();

                // Create presenter to be tested, inject our mock model
                CompanyPresenter presenter = new CompanyPresenter(mockView.Object, mockModel.Object);

                // Declare that the user will select "Yes" on the confirmation messagebox
                mockView.Setup(x => x.ShowMessageBox(It.IsAny<string>(), It.IsAny<string>(),
                                                     It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                                                     .Returns(DialogResult.Yes);

                // Keep a copy of the facade records list our presenter will be working with
                List<Company> records = Mock_GetAllCompanies().ToList();

                // Declare which record shall be selected from the list
                int index = 0;

                // Keep a copy of which record the presenter SHOULD select & push to view
                Company company = records[index];

                // Select our test case company, mimicking the user selecting it from a ListView
                if(recordSelected)
                    presenter.lvw_companies_SelectedIndexChanged(new int[] { index });

                #endregion Arrange

                /**************************************************/

                #region Act

                //Invoke the call being tested
                presenter.btn_DeleteCompany_Click();

                #endregion Act

                /**************************************************/

                #region Assert

                // Ensure the presenter told the model to delete the record, only if a record was selected
                // Only passes if the CORRECT company was sent to the model
                if (recordSelected)
                {
                    mockModel.Verify(r => r.DeleteCompany(It.Is<Company>(c => c.CompanyID == company.CompanyID)), Times.Once);
                }
                else
                {
                    mockModel.Verify(r => r.DeleteCompany(It.IsAny<Company>()), Times.Never);
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
        void EvaluateScreen(Dictionary<string, object> expected, Mock<ICompanyGUI> mockView)
        {
            // Declare actual values
            Dictionary<string, object> actual = new Dictionary<string, object>
            {
                { "CompanyID",   mockView.Object.CurrentCompanyID },
                { "CompanyName", mockView.Object.CurrentCompanyName },
                { "CurrentCompany_Enabled", mockView.Object.CurrentCompany_Enabled }
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
                { "CompanyID",   "" },
                { "CompanyName", "" },
                { "CurrentCompany_Enabled", false }
            };
        }

        // ********************************************************************************
        // View Layer Mocking *************************************************************
        // ********************************************************************************

        // Initialises the mock screen to what mimic a form that has just loaded
        Moq.Mock<ICompanyGUI> GetMockView()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var screen = mock.Mock<ICompanyGUI>();
                screen.SetupAllProperties();
                // Screen mock needs to provide facade for properties changing
                // so the test can check they are correctly assigned
                screen.SetupProperty(x => x.CurrentCompanyID)
                      .SetReturnsDefault("");
                screen.SetupProperty(x => x.CurrentCompanyName)
                      .SetReturnsDefault("");
                screen.SetupProperty(x => x.CurrentCompany_Enabled)
                      .SetReturnsDefault(false);
                screen.SetupGet(x => x.CompanyList)
                      .Returns(new ListView.ListViewItemCollection(new ListView()) { new ListViewItem() });
                screen.SetupGet(x => x.CurrentCompanyDepartments)
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
        Mock<ICompanyModel> GetMockModel()
        {
            var mockModel = new Mock<ICompanyModel>();
            mockModel.Setup(r => r.GetAllCompanies()).Returns(Mock_GetAllCompanies().ToList());
            mockModel.Setup(r => r.FindCompany(It.IsAny<int>())).Returns<int>((id) => Mock_GetCompany(id));
            mockModel.Setup(r => r.SaveCompany(It.IsAny<Company>())).Verifiable();
            mockModel.Setup(r => r.DeleteCompany(It.IsAny<Company>())).Verifiable();
            return mockModel;
        }

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
    }
}