//16007006 Andrew Patton
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
    public class BookingPresenter_Tests
    {
        // ********************************************************************************
        // Test Methods *******************************************************************
        // ********************************************************************************

        // Test ID: BP1
        // Purpose: Test the presenter's response to the "Add Booking" button being pressed
        [TestMethod()]
        public void LoadNewRecord_Test()
        {
            #region Arrange

            // Create mock layers
            var mockView = GetMockView();
            var mockModel = GetMockModel();

            // Create presenter to be tested, inject our mock view & model
            BookingPresenter presenter = new BookingPresenter(mockView.Object, mockModel.Object, 1);
            
            // Set the screen's CurrentBookingName to "Foobar"
            // Our presenter will reset this to an empty string
            mockView.Object.CurrentBookingName = "FooBar";            

            #endregion Arrange

            #region Act            

            //Invoke the call being tested
            presenter.btn_AddBooking_Click();

            #endregion Act

            #region Assert
            // Request evaluation of screen properties
            Dictionary<string, object> expected = new Dictionary<string, object>
            {
                { "BookingID",   "0" },
                { "BookingName", "" },
                { "CurrentBooking_Enabled", true }
            };
            EvaluateScreen(expected, mockView);
            #endregion Assert
        }

        // Test ID: BP2
        // Purpose: Test the presenter's response to a new item being selected
        [TestMethod()]
        public void LoadRecord_Test()
        {
            #region Arrange

            // Create mock layers
            var mockView = GetMockView();
            var mockModel = GetMockModel();

            // Create presenter to be tested, inject our mock view & model
            BookingPresenter presenter = new BookingPresenter(mockView.Object, mockModel.Object, 1);

            // Keep a copy of the facade records list our presenter will be working with
            List<Booking> records = Mock_GetAllBookings().ToList();

            // Declare which record shall be selected from the list
            int index = 2;

            // Keep a copy of which record the presenter SHOULD select & push to view
            Booking booking = records[index];

            #endregion Arrange

            /**************************************************/

            #region Act

            // Select our test case department, mimicking the user selecting it from a ListView
            presenter.lvw_Bookings_SelectedIndexChanged(new int[] { index });

            #endregion Act

            /**************************************************/

            #region Assert

            // Declare expected values
            Dictionary<string, object> expected = new Dictionary<string, object>
            {
                { "BookingID",   booking.BookingID.ToString() },
                { "BookingName", booking.Name },
                { "CurrentBooking_Enabled", true }
            };

            // Request evaluation of screen properties
            EvaluateScreen(expected, mockView);

            #endregion Assert
        }

        // Test ID: BP3
        // Purpose: Test the presenter's response to the "Confirm" button being pressed
        [TestMethod()]
        [DataRow(true)]  // BP3a | Changes pending
        [DataRow(false)] // BP3b | No changes pending
        public void SaveRecord_Test(bool makeChanges)
        {
            using (var mock = AutoMock.GetLoose())
            {
                #region Arrange
                // Create mock layers
                var mockView = GetMockView();
                var mockModel = GetMockModel();

                // Create presenter to be tested, inject our mock model
                BookingPresenter presenter = new BookingPresenter(mockView.Object, mockModel.Object, 1);

                // Keep a copy of the facade records list our presenter will be working with
                List<Booking> records = Mock_GetAllBookings().ToList();

                // Declare which record shall be selected from the list
                int index = 0;

                // Keep a copy of which record the presenter SHOULD select & push to view
                Booking booking = records[index];

                // Select a department record
                presenter.lvw_Bookings_SelectedIndexChanged(new int[] { index });

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
                    mockModel.Verify(x => x.SaveBooking(It.Is<Booking>(d => d.BookingID == booking.BookingID)), Times.Once);
                else
                    mockModel.Verify(x => x.SaveBooking(It.IsAny<Booking>()), Times.Never);

                // Request evaluation of screen properties
                Dictionary<string, object> expected = GetClearedScreen();
                EvaluateScreen(expected, mockView);

                #endregion Assert
            }
        }

        // Test ID: BP4
        // Purpose: Test the presenter's response to the "Delete Booking" button being pressed
        [TestMethod()]
        [DataRow(true)]  //BP4a | Record is currently selected
        [DataRow(false)] //BP4b | No record currently selected
        public void DeleteRecord_Test(bool recordSelected)
        {
            using (var mock = AutoMock.GetLoose())
            {
                #region Arrange

                // Create mock layers
                var mockView = GetMockView();
                var mockModel = GetMockModel();

                // Create presenter to be tested, inject our mock model
                BookingPresenter presenter = new BookingPresenter(mockView.Object, mockModel.Object, 1);

                // Declare that the user will select "Yes" on the confirmation messagebox
                mockView.Setup(x => x.ShowMessageBox(It.IsAny<string>(), It.IsAny<string>(),
                                                     It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                                                     .Returns(DialogResult.Yes);

                // Keep a copy of the facade records list our presenter will be working with
                List<Booking> records = Mock_GetAllBookings().ToList();

                // Declare which record shall be selected from the list
                int index = 0;

                // Keep a copy of which record the presenter SHOULD select & push to view
                Booking booking = records[index];

                // Select our test case department, mimicking the user selecting it from a ListView
                if (recordSelected)
                    presenter.lvw_Bookings_SelectedIndexChanged(new int[] { index });

                #endregion Arrange

                /**************************************************/

                #region Act

                //Invoke the call being tested
                presenter.btn_DeleteBooking_Click();

                #endregion Act

                /**************************************************/

                #region Assert

                // Ensure the presenter told the model to delete the record, only if a record was selected
                // Only passes if the CORRECT department was sent to the model
                if (recordSelected)
                {
                    mockModel.Verify(r => r.DeleteBooking(It.Is<Booking>(d => d.BookingID == booking.BookingID)), Times.Once);
                }
                else
                {
                    mockModel.Verify(r => r.DeleteBooking(It.IsAny<Booking>()), Times.Never);
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
        void EvaluateScreen(Dictionary<string, object> expected, Mock<IBookingGUI> screen)
        {
            // Declare actual values
            Dictionary<string, object> actual = new Dictionary<string, object>
            {
                { "BookingID",   screen.Object.CurrentBookingID },
                { "BookingName", screen.Object.CurrentBookingName },
                { "BookingDate", screen.Object.CurrentBookingDate },
                { "CurrentBooking_Enabled", screen.Object.CurrentBooking_Enabled }
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
                { "BookingID",   "" },
                { "BookingName", "" },
                { "BookingDate", DateTime.Today },
                { "CurrentBooking_Enabled", false }
            };
        }

        // ********************************************************************************
        // View Layer Mocking *************************************************************
        // ********************************************************************************

        // Initialises the mock screen to what mimic a form that has just loaded
        Mock<IBookingGUI> GetMockView()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var screen = mock.Mock<IBookingGUI>();
                screen.SetupAllProperties();
                // Screen mock needs to provide facade for properties changing
                // so the test can check they are correctly assigned
                screen.SetupProperty(x => x.CurrentBookingID)
                      .SetReturnsDefault("");
                screen.SetupProperty(x => x.CurrentBookingName)
                      .SetReturnsDefault("");
                screen.SetupProperty(x => x.CurrentBooking_Enabled)
                      .SetReturnsDefault(false);
                screen.SetupGet(x => x.BookingList)
                      .Returns(new ListView.ListViewItemCollection(new ListView()) { new ListViewItem() });
                screen.SetupGet(x => x.CurrentBookingActivities)
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
        Mock<IBookingModel> GetMockModel()
        {
            var mockModel = new Mock<IBookingModel>();
            mockModel.Setup(r => r.GetAllBookings()).Returns(Mock_GetAllBookings().ToList());
            mockModel.Setup(r => r.GetAllBookingsFrom(It.IsAny<Department>())).Returns(Mock_GetAllBookings().ToList());
            mockModel.Setup(r => r.FindBooking(It.IsAny<int>())).Returns<int>((id) => Mock_GetBooking(id));
            mockModel.Setup(r => r.FindBooking(It.IsAny<Booking>())).Returns<Booking>((b) => Mock_GetBooking(b.BookingID));
            mockModel.Setup(r => r.SaveBooking(It.IsAny<Booking>())).Verifiable();
            mockModel.Setup(r => r.DeleteBooking(It.IsAny<Booking>())).Verifiable();
            mockModel.Setup(r => r.FindDepartment(It.IsAny<int>())).Returns<int>((id) => Mock_GetDepartment(id));
            return mockModel;
        }

        // Mock Department Data
        IQueryable<Department> Mock_GetAllDepartments()
        {
            Company company = new Company { CompanyID = 1, Name = "Asda" };
            return new List<Department>
            {
                new Department { DepartmentID = 1, Name = "HR",        Company = company },
                new Department { DepartmentID = 2, Name = "Legal",     Company = company },
                new Department { DepartmentID = 3, Name = "Marketing", Company = company },
                new Department { DepartmentID = 4, Name = "IT",        Company = company }
            }.AsQueryable();
        }
        Department Mock_GetDepartment(int id) => Mock_GetAllDepartments().Where(c => c.DepartmentID == id).FirstOrDefault();

        // Mock Booking Data
        IQueryable<Booking> Mock_GetAllBookings()
        {
            return new List<Booking>
            {
                new Booking { BookingID = 1, Name = "Xmas Party"   },
                new Booking { BookingID = 2, Name = "Dave's 40th"  },
                new Booking { BookingID = 3, Name = "Team Building" }
            }.AsQueryable();
        }
        Booking Mock_GetBooking(int id) => Mock_GetAllBookings().Where(d => d.BookingID == id).FirstOrDefault();
    }
}