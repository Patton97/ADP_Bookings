using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADP_Bookings.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Autofac.Extras.Moq;
using Moq;

using ADP_Bookings.Models;
using ADP_Bookings.Views;
using System.Data.Entity;
using System.Windows.Forms;

namespace ADP_Bookings.Presenters.Tests
{
    [TestClass()]
    public class DepartmentPresenter_Tests
    {
        // ********************************************************************************
        // Test Methods *******************************************************************
        // ********************************************************************************

        // Test ID: CP1
        // Purpose: Test the presenter's response to the "Add Department" button being pressed
        [TestMethod()]
        public void LoadNewRecord_Test()
        {
            #region Arrange
            // Mimic a frm_companies being loaded
            var screen = InitMockScreen();

            // Send null screen param, automock will fill in for it later
            DepartmentPresenter presenter = new DepartmentPresenter(screen.Object, 1);
            #endregion Arrange

            #region Act
            // Set the screen's CurrentDepartmentName to "Foobar" so our
            // presenter can (attempt to) overwrite it with an empty string
            screen.Object.CurrentDepartmentName = "FooBar";

            //Invoke the call being tested
            presenter.btn_AddDepartment_Click();
            #endregion Act

            #region Assert
            // Declare expected values
            Dictionary<string, object> expected = new Dictionary<string, object>
            {
                { "DepartmentID",   "0" },
                { "DepartmentName", "" },
                { "CurrentDepartment_Enabled", true }
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
            DepartmentPresenter presenter = new DepartmentPresenter(screen.Object, 1);

            // This is the department we will be asking the presenter to load
            Department department = new Department() { DepartmentID = 1, Name = "Legal", };

            // Place our test record amongst a list of other companies to ensure not
            // only the presenter both selects the correct department and then loads it
            // This is in place of the presenter collecting the list from the DB
            List<Department> records = Mock_GetAllDepartments().ToList();

            // Store position to ensure the test itself does not mimic selecting the wrong record
            int testCasePosition = 2;
            records[testCasePosition] = department;

            // NOTE: Accessing the presenter's private member(s) is poor practice 
            //       during testing and can be avoided but due to documented teamwork 
            //       issues I am short on time for this assignment..
            new PrivateObject(presenter).SetField("records", records);
            #endregion Arrange

            #region Act
            // Mimic a new item being selected from the form's lvw_companies
            presenter.lvw_Departments_SelectedIndexChanged(new int[] { testCasePosition });
            #endregion Act

            #region Assert
            // Declare expected values
            Dictionary<string, object> expected = new Dictionary<string, object>
            {
                { "DepartmentID",   records[testCasePosition].DepartmentID.ToString() },
                { "DepartmentName", records[testCasePosition].Name },
                { "CurrentDepartment_Enabled", true }
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
                DepartmentPresenter presenter = new DepartmentPresenter(screen.Object, 1);

                // This is the department we will be asking the presenter to save
                Department department = new Department() { DepartmentID = 1, Name = "Legal_NewName" };

                // This function attempts to touch the Model layer (UoW, DB), 
                // so we need to mock the required elements from that environment
                var mockSet_Departments = GetMockSet_Departments();
                var mockSet_Companies = GetMockSet_Companies();
                var mockContext = GetMockContext(mockSet_Departments.Object, mockSet_Companies.Object);
                var mockRepo = GetMockRepo();
                var mockUoW = GetMockUoW(mockRepo.Object);
                var mockUoWFactory = GetMockUoWFactory(mockUoW.Object);

                // Inject mock UoW factory
                // Again, using PrivateObject is a poor practice way to go about this
                // and it should be done via the public API but I am short on time
                new PrivateObject(presenter).SetField("unitOfWorkFactory", mockUoWFactory.Object);

                // Inject our test case department as the presenter's currently selected record
                new PrivateObject(presenter).SetField("selectedRecord", department);
                #endregion Arrange

                #region Act
                //Invoke the call being tested
                presenter.btn_ConfirmChanges_Click();
                #endregion Act

                #region Assert
                // Ensure the presenter told the model to update the record
                mockRepo.Verify(x => x.Update(It.IsAny<Department>()), Times.Exactly(1));

                // Request evaluation of screen properties
                Dictionary<string, object> expected = new Dictionary<string, object>
                {
                    { "DepartmentID",   "" },
                    { "DepartmentName", "" },
                    { "CurrentDepartment_Enabled", false }
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
                // We pass it CompanyID = 1 to satisfy the constructor, 
                // but this will have no affect on our tests
                DepartmentPresenter presenter = new DepartmentPresenter(screen.Object, 1);

                // This is the department we will be asking the presenter to save
                Department department = new Department() { DepartmentID = 5, Name = "Marketing" };

                // This function attempts to touch the Model layer (UoW, DB), 
                // so we need to mock the required elements from that environment
                var mockSet_Departments = GetMockSet_Departments();
                var mockSet_Companies = GetMockSet_Companies();
                var mockContext = GetMockContext(mockSet_Departments.Object, mockSet_Companies.Object);
                var mockRepo = GetMockRepo();
                var mockUoW = GetMockUoW(mockRepo.Object);
                var mockUoWFactory = GetMockUoWFactory(mockUoW.Object);

                // Inject mock UoW factory
                // Again, using PrivateObject is a poor practice way to go about this
                // and it should be done via the public API but I am short on time
                new PrivateObject(presenter).SetField("unitOfWorkFactory", mockUoWFactory.Object);

                // Inject our test case department as the presenter's currently selected record
                new PrivateObject(presenter).SetField("selectedRecord", department);
                #endregion Arrange

                #region Act
                //Invoke the call being tested
                presenter.btn_ConfirmChanges_Click();
                #endregion Act

                #region Assert

                // Ensure the presenter told the model to add the record
                mockRepo.Verify(x => x.Add(It.IsAny<Department>()), Times.Exactly(1));

                // Request evaluation of screen properties
                Dictionary<string, object> expected = new Dictionary<string, object>
                {
                    { "DepartmentID",   "" },
                    { "DepartmentName", "" },
                    { "CurrentDepartment_Enabled", false }
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
                DepartmentPresenter presenter = new DepartmentPresenter(screen.Object, 1);

                // This is the department we will be asking the presenter to save
                Department department = new Department() { DepartmentID = 1, Name = "Legal" };

                // This function attempts to touch the Model layer (UoW, DB), 
                // so we need to mock the required elements from that environment
                var mockSet_Departments = GetMockSet_Departments();
                var mockSet_Companies = GetMockSet_Companies();
                var mockContext = GetMockContext(mockSet_Departments.Object, mockSet_Companies.Object);
                var mockRepo = GetMockRepo();
                var mockUoW = GetMockUoW(mockRepo.Object);
                var mockUoWFactory = GetMockUoWFactory(mockUoW.Object);

                // Inject mock UoW factory
                // Again, using PrivateObject is a poor practice way to go about this
                // and it should be done via the public API but I am short on time
                new PrivateObject(presenter).SetField("unitOfWorkFactory", mockUoWFactory.Object);

                // Inject our test case department as the presenter's currently selected record
                new PrivateObject(presenter).SetField("selectedRecord", department);
                #endregion Arrange

                #region Act
                //Invoke the call being tested
                presenter.btn_DeleteDepartment_Click();
                #endregion Act

                #region Assert
                // Ensure the presenter told the model to delete the record
                mockRepo.Verify(x => x.Remove(It.IsAny<Department>()), Times.Exactly(1));

                // Request evaluation of screen properties
                Dictionary<string, object> expected = new Dictionary<string, object>
                {
                    { "DepartmentID",   "" },
                    { "DepartmentName", "" },
                    { "CurrentDepartment_Enabled", false }
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

        // ********************************************************************************
        // View Layer Mocking *************************************************************
        // ********************************************************************************

        // Initialises the mock screen to what mimic a form that has just loaded
        Moq.Mock<IDepartmentGUI> InitMockScreen()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var screen = mock.Mock<IDepartmentGUI>();

                // Screen mock needs to provide facade for both ListViews because
                // presenter attempts to clear them when creating a new record
                screen.SetupGet(x => x.DepartmentList)
                      .Returns(new ListView.ListViewItemCollection(new ListView()) { new ListViewItem() });
                screen.SetupGet(x => x.CurrentDepartmentBookings)
                      .Returns(new ListView.ListViewItemCollection(new ListView()) { new ListViewItem() });

                screen.SetupProperty(x => x.CurrentDepartment_Enabled)
                          .SetReturnsDefault(false);

                // Screen mock needs to provide facade for ID & Name 
                // so the test can check they are correctly assigned            
                screen.SetupProperty(x => x.CurrentDepartmentID)
                      .SetReturnsDefault("");
                screen.SetupProperty(x => x.CurrentDepartmentName)
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
        Mock<DbSet<Department>> GetMockSet_Departments() => new Mock<DbSet<Department>>();

        Mock<DbSet<Company>> GetMockSet_Companies() => new Mock<DbSet<Company>>();

        // Mock DB connection
        Mock<IADP_DBContext> GetMockContext(DbSet<Department> mockSet_Departments, DbSet<Company> mockSet_Companies)
        {
            var mockContext = new Mock<IADP_DBContext>();
            mockContext.Setup(ctx => ctx.Departments).Returns(mockSet_Departments);
            mockContext.Setup(ctx => ctx.Companies).Returns(mockSet_Companies);
            return mockContext;
        }

        // Mock Repo pattern
        Mock<IDepartmentRepository> GetMockRepo()
        {
            var mockRepo = new Mock<IDepartmentRepository>();
            mockRepo.Setup(r => r.Add(It.IsAny<Department>()));
            mockRepo.Setup(r => r.GetAll()).Returns(Mock_GetAllDepartments());
            mockRepo.Setup(r => r.Get(It.IsAny<int>())).Returns<int>((id) => Mock_GetDepartment(id));
            mockRepo.Setup(r => r.Update(It.IsAny<Department>()));
            mockRepo.Setup(r => r.Remove(It.IsAny<Department>()));

            return mockRepo;
        }

        // Mock Unit of Work pattern
        Mock<IUnitOfWork> GetMockUoW(IDepartmentRepository mockRepo)
        {
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(uow => uow.Departments).Returns(mockRepo);
            return mockUoW;
        }

        Mock<IUnitOfWorkFactory> GetMockUoWFactory(IUnitOfWork mockUoW)
        {
            var mockUowFactory = new Mock<IUnitOfWorkFactory>();
            mockUowFactory.Setup(x => x.Create()).Returns(mockUoW);

            return mockUowFactory;
        }

        IQueryable<Department> Mock_GetAllDepartments()
        {
            return new List<Department>
            {
                new Department { DepartmentID = 1, Name = "Legal"   },
                new Department { DepartmentID = 2, Name = "HR"  },
                new Department { DepartmentID = 3, Name = "Marketing" }
            }.AsQueryable();
        }

        Department Mock_GetDepartment(int id) => Mock_GetAllDepartments().Where(d => d.DepartmentID == id).FirstOrDefault();
    }
}