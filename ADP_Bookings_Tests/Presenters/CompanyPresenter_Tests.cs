﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            // Mimic a frm_companies being loaded
            var screen = InitMockScreen();               

            // Send null screen param, automock will fill in for it later
            CompanyPresenter presenter = new CompanyPresenter(screen.Object);
            #endregion Arrange
            
            #region Act
            // Set the screen's CurrentCompanyName to "Foobar" so our
            // presenter can (attempt to) overwrite it with an empty string
            screen.Object.CurrentCompanyName = "FooBar";

            //Invoke the call being tested
            presenter.btn_AddCompany_Click();
            #endregion Act
            
            #region Assert
            // Declare expected values
            Dictionary<string, object> expected = new Dictionary<string, object>
            {
                { "CompanyID",   "0" },
                { "CompanyName", "" },
                { "CurrentCompany_Enabled", true }
            };

            // Request evaluation of screen properties
            EvaluateScreen(expected, screen);
            #endregion Assert
        }

        // Test ID: CP2
        // Purpose: Test the presenter's response to a new item being selected

        [TestMethod()]
        public void LoadRecord_Test()
        {
            #region Arrange
            // Mimic a frm_companies being loaded
            var screen = InitMockScreen();

            // Create presenter to be tested
            CompanyPresenter presenter = new CompanyPresenter(screen.Object);

            // This is the company we will be asking the presenter to load
            Company company = new Company() { CompanyID = 1, Name = "Asda", };

            // Place our test record amongst a list of other companies to ensure not
            // only the presenter both selects the correct company and then loads it
            // This is in place of the presenter collecting the list from the DB
            List<Company> records = Mock_GetAllCompanies().ToList();

            // Store position to ensure the test itself does not mimic selecting the wrong record
            int testCasePosition = 2;
            records[testCasePosition] = company;

            // NOTE: Accessing the presenter's private member(s) is poor practice 
            //       during testing and can be avoided but due to documented teamwork 
            //       issues I am short on time for this assignment..
            new PrivateObject(presenter).SetField("records", records);
            #endregion Arrange

            #region Act
            // Mimic a new item being selected from the form's lvw_companies
            presenter.lvw_companies_SelectedIndexChanged(new int[] { testCasePosition });
            #endregion Act

            #region Assert
            // Declare expected values
            Dictionary<string, object> expected = new Dictionary<string, object>
            {
                { "CompanyID",   records[testCasePosition].CompanyID.ToString() },
                { "CompanyName", records[testCasePosition].Name },
                { "CurrentCompany_Enabled", true }
            };

            // Request evaluation of screen properties
            EvaluateScreen(expected, screen);
            #endregion Assert
        }

        // Test ID: CP3
        // Purpose: Test the presenter's response to the "Confirm" button being  
        //          pressed when the selected record is an existing record
        [TestMethod()]
        public void SaveExistingRecord_Test()
        {
            using (var mock = AutoMock.GetLoose())
            {
                #region Arrange
                // Mimic a frm_companies being loaded
                var screen = InitMockScreen();

                // Create presenter to be tested
                CompanyPresenter presenter = new CompanyPresenter(screen.Object);

                // This is the company we will be asking the presenter to save
                Company company = new Company() { CompanyID = 1, Name = "Asda_NewName" };

                // This function attempts to touch the Model layer (UoW, DB), 
                // so we need to mock the required elements from that environment
                var mockSet = GetMockSet();
                var mockContext = GetMockContext(mockSet.Object);
                var mockRepo = GetMockRepo();
                var mockUoW = GetMockUoW(mockRepo.Object);
                var mockUoWFactory = GetMockUoWFactory(mockUoW.Object);

                // Inject mock UoW factory
                // Again, using PrivateObject is a poor practice way to go about this
                // and it should be done via the public API but I am short on time
                new PrivateObject(presenter).SetField("unitOfWorkFactory", mockUoWFactory.Object);
                
                // Inject our test case company as the presenter's currently selected record
                new PrivateObject(presenter).SetField("selectedRecord", company);
                #endregion Arrange

                #region Act
                //Invoke the call being tested
                presenter.btn_ConfirmChanges_Click();
                #endregion Act

                #region Assert
                // Ensure the presenter told the model to update the record
                mockRepo.Verify(x => x.Update(It.IsAny<Company>()), Times.Exactly(1));

                // Request evaluation of screen properties
                Dictionary<string, object> expected = new Dictionary<string, object>
                {
                    { "CompanyID",   "" },
                    { "CompanyName", "" },
                    { "CurrentCompany_Enabled", false }
                };                
                EvaluateScreen(expected, screen);

                #endregion Assert
            }
        }

        // Test ID: CP4
        // Purpose: Test the presenter's response to the "Confirm" button
        //          being pressed when the select record is a new record
        [TestMethod()]
        public void SaveNewRecord_Test()
        {
            using (var mock = AutoMock.GetLoose())
            {
                #region Arrange
                // Mimic a frm_companies being loaded
                var screen = InitMockScreen();

                // Create presenter to be tested
                CompanyPresenter presenter = new CompanyPresenter(screen.Object);

                // This is the company we will be asking the presenter to save
                Company company = new Company() { CompanyID = 5, Name = "eBay" };

                // This function attempts to touch the Model layer (UoW, DB), 
                // so we need to mock the required elements from that environment
                var mockSet = GetMockSet();
                var mockContext = GetMockContext(mockSet.Object);
                var mockRepo = GetMockRepo();
                var mockUoW = GetMockUoW(mockRepo.Object);
                var mockUoWFactory = GetMockUoWFactory(mockUoW.Object);

                // Inject mock UoW factory
                // Again, using PrivateObject is a poor practice way to go about this
                // and it should be done via the public API but I am short on time
                new PrivateObject(presenter).SetField("unitOfWorkFactory", mockUoWFactory.Object);

                // Inject our test case company as the presenter's currently selected record
                new PrivateObject(presenter).SetField("selectedRecord", company);
                #endregion Arrange

                #region Act
                //Invoke the call being tested
                presenter.btn_ConfirmChanges_Click();
                #endregion Act

                #region Assert

                // Ensure the presenter told the model to add the record
                mockRepo.Verify(x => x.Add(It.IsAny<Company>()), Times.Exactly(1));

                // Request evaluation of screen properties
                Dictionary<string, object> expected = new Dictionary<string, object>
                {
                    { "CompanyID",   "" },
                    { "CompanyName", "" },
                    { "CurrentCompany_Enabled", false }
                };
                EvaluateScreen(expected, screen);

                #endregion Assert
            }
        }

        // Test ID: CP4
        // Purpose: Test the presenter's response to the "Confirm" button
        //          being pressed when the select record is a new record
        [TestMethod()]
        public void DeleteRecord_Test()
        {
            using (var mock = AutoMock.GetLoose())
            {
                #region Arrange
                // Mimic a frm_companies being loaded
                var screen = InitMockScreen();

                // Create presenter to be tested
                CompanyPresenter presenter = new CompanyPresenter(screen.Object);

                // This is the company we will be asking the presenter to save
                Company company = new Company() { CompanyID = 1, Name = "Asda" };

                // This function attempts to touch the Model layer (UoW, DB), 
                // so we need to mock the required elements from that environment
                var mockSet = GetMockSet();
                var mockContext = GetMockContext(mockSet.Object);
                var mockRepo = GetMockRepo();
                var mockUoW = GetMockUoW(mockRepo.Object);
                var mockUoWFactory = GetMockUoWFactory(mockUoW.Object);

                // Inject mock UoW factory
                // Again, using PrivateObject is a poor practice way to go about this
                // and it should be done via the public API but I am short on time
                new PrivateObject(presenter).SetField("unitOfWorkFactory", mockUoWFactory.Object);

                // Inject our test case company as the presenter's currently selected record
                new PrivateObject(presenter).SetField("selectedRecord", company);
                #endregion Arrange

                #region Act
                //Invoke the call being tested
                presenter.btn_DeleteCompany_Click();
                #endregion Act

                #region Assert
                // Ensure the presenter told the model to delete the record
                mockRepo.Verify(x => x.Remove(It.IsAny<Company>()), Times.Exactly(1));

                // Request evaluation of screen properties
                Dictionary<string, object> expected = new Dictionary<string, object>
                {
                    { "CompanyID",   "" },
                    { "CompanyName", "" },
                    { "CurrentCompany_Enabled", false }
                };
                EvaluateScreen(expected, screen);

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
        void EvaluateScreen(Dictionary<string, object> expected, Moq.Mock<ICompanyGUI> screen)
        {
            // Declare actual values
            Dictionary<string, object> actual = new Dictionary<string, object>
            {
                { "CompanyID",   screen.Object.CurrentCompanyID },
                { "CompanyName", screen.Object.CurrentCompanyName },
                { "CurrentCompany_Enabled", screen.Object.CurrentCompany_Enabled }
            };

            // Compare expected values with actual values
            foreach (KeyValuePair<string, object> entry in actual)
            {
                // Only test properties if the calling test has included them
                if (expected.ContainsKey(entry.Key))
                    Assert.AreEqual(expected[entry.Key], entry.Value);
            }
        }

        // ********************************************************************************
        // View Layer Mocking *************************************************************
        // ********************************************************************************

        // Initialises the mock screen to what mimic a form that has just loaded
        Moq.Mock<ICompanyGUI> InitMockScreen()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var screen = mock.Mock<ICompanyGUI>();

                // Screen mock needs to provide facade for both ListViews because
                // presenter attempts to clear them when creating a new record
                screen.SetupGet(x => x.CompanyList)
                      .Returns(new ListView.ListViewItemCollection(new ListView()) { new ListViewItem() });
                screen.SetupGet(x => x.CurrentCompanyDepartments)
                      .Returns(new ListView.ListViewItemCollection(new ListView()) { new ListViewItem() });

                screen.SetupProperty(x => x.CurrentCompany_Enabled)
                          .SetReturnsDefault(false);

                // Screen mock needs to provide facade for ID & Name 
                // so the test can check they are correctly assigned            
                screen.SetupProperty(x => x.CurrentCompanyID)
                      .SetReturnsDefault("");
                screen.SetupProperty(x => x.CurrentCompanyName)
                      .SetReturnsDefault("");

                return screen;
            }
        }

        // ********************************************************************************
        // Model Layer Mocking ************************************************************
        // ********************************************************************************

        // This test class only cares about checking the presenter logic is correct.
        // The below functions provide a false data access layer. Presenter tests passing is
        // therefore directly tied to the integrity of the presenter logic, regardless of the model

        // Mock data storage
        Mock<DbSet<Company>> GetMockSet()
        {
            var mockSet = new Mock<DbSet<Company>>();
            return mockSet;
        }

        // Mock DB connection
        Mock<IADP_DBContext> GetMockContext(DbSet<Company> mockSet)
        {
            var mockContext = new Mock<IADP_DBContext>();
            mockContext.Setup(ctx => ctx.Companies).Returns(mockSet);
            return mockContext;
        }

        // Mock Repo pattern
        Mock<ICompanyRepository> GetMockRepo()
        {
            var mockRepo = new Mock<ICompanyRepository>();
            mockRepo.Setup(r => r.Add(It.IsAny<Company>()));
            mockRepo.Setup(r => r.GetAll()).Returns(Mock_GetAllCompanies());
            mockRepo.Setup(r => r.Get(It.IsAny<int>())).Returns<int>((id) => Mock_GetCompany(id));
            mockRepo.Setup(r => r.Update(It.IsAny<Company>()));
            mockRepo.Setup(r => r.Remove(It.IsAny<Company>()));


            return mockRepo;
        }

        // Mock Unit of Work pattern
        Mock<IUnitOfWork> GetMockUoW(ICompanyRepository mockRepo)
        {
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(uow => uow.Companies).Returns(mockRepo);
            return mockUoW;
        }

        Mock<IUnitOfWorkFactory> GetMockUoWFactory(IUnitOfWork mockUoW)
        {
            var mockUowFactory = new Mock<IUnitOfWorkFactory>();
            mockUowFactory.Setup(x => x.Create()).Returns(mockUoW);

            return mockUowFactory;
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