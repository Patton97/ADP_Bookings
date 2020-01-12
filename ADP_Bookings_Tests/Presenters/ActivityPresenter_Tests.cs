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

// Required to create mocks of these layers
using ADP_Bookings.Views;
using ADP_Bookings.Models;

namespace ADP_Bookings.Presenters.Tests
{
    [TestClass()]
    public class ActivityPresenter_Tests
    {
        // ********************************************************************************
        // Test Methods *******************************************************************
        // ********************************************************************************

        // Test ID: AP1
        // Purpose: Test the presenter's response to the "Add Activity" button being pressed
        [TestMethod()]
        public void LoadNewRecord_Test()
        {
            #region Arrange

            // Create mock layers
            var mockView = GetMockView();
            var mockModel = GetMockModel();

            // Create presenter to be tested, inject our mock view & model
            ActivityPresenter presenter = new ActivityPresenter(mockView.Object, mockModel.Object, 1);

            // Set the screen's CurrentActivityName to "Foobar"
            // Our presenter will reset this to an empty string
            mockView.Object.CurrentActivityName = "FooBar";

            #endregion Arrange

            /**************************************************/

            #region Act            

            //Invoke the call being tested
            presenter.btn_AddActivity_Click();

            #endregion Act

            /**************************************************/

            #region Assert

            // Request evaluation of screen properties
            Dictionary<string, object> expected = new Dictionary<string, object>
            {
                { "ActivityID",    "0" },
                { "ActivityName",  ""  },
                { "ActivityCost",  0m  },
                { "ActivityNotes", ""  },
                { "CurrentDepartment_Enabled", true }
            };
            EvaluateScreen(expected, mockView);

            #endregion Assert
        }

        // Test ID: AP2
        // Purpose: Test the presenter's response to a new item being selected
        [TestMethod()]
        public void LoadRecord_Test()
        {
            #region Arrange

            // Create mock layers
            var mockView = GetMockView();
            var mockModel = GetMockModel();

            // Create presenter to be tested, inject our mock view & model
            ActivityPresenter presenter = new ActivityPresenter(mockView.Object, mockModel.Object, 1);

            // Keep a copy of the facade records list our presenter will be working with
            List<Activity> records = Mock_GetAllActivities().ToList();

            // Declare which record shall be selected from the list
            int index = 2;

            // Keep a copy of which record the presenter SHOULD select & push to view
            Activity activity = records[index];

            #endregion Arrange

            /**************************************************/

            #region Act

            // Select our test case company, mimicking the user selecting it from a ListView
            presenter.lvw_Activities_SelectedIndexChanged(new int[] { index });

            #endregion Act

            /**************************************************/

            #region Assert

            // Declare expected values
            Dictionary<string, object> expected = new Dictionary<string, object>
            {
                { "ActivityID",    activity.ActivityID.ToString() },
                { "ActivityName",  activity.Name  },
                { "ActivityCost",  (decimal)activity.Cost },
                { "ActivityNotes", activity.Notes },
                { "CurrentDepartment_Enabled", true }
            };

            // Request evaluation of screen properties
            EvaluateScreen(expected, mockView);

            #endregion Assert
        }

        // Test ID: AP3
        // Purpose: Test the presenter's response to the "Confirm" button being pressed
        [TestMethod()]
        [DataRow(true)]  // AP3a | Changes pending
        [DataRow(false)] // AP3b | No changes pending
        public void SaveRecord_Test(bool makeChanges)
        {
            using (var mock = AutoMock.GetLoose())
            {
                #region Arrange
                // Create mock layers
                var mockView = GetMockView();
                var mockModel = GetMockModel();

                // Create presenter to be tested, inject our mock model
                ActivityPresenter presenter = new ActivityPresenter(mockView.Object, mockModel.Object, 1);

                // Keep a copy of the facade records list our presenter will be working with
                List<Activity> records = Mock_GetAllActivities().ToList();

                // Declare which record shall be selected from the list
                int index = 0;

                // Keep a copy of which record the presenter SHOULD select & push to view
                Activity activity = records[index];

                // Select a record
                presenter.lvw_Activities_SelectedIndexChanged(new int[] { index });

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
                    mockModel.Verify(x => x.SaveActivity(It.Is<Activity>(a => a.ActivityID == activity.ActivityID)), Times.Once);
                else
                    mockModel.Verify(x => x.SaveActivity(It.IsAny<Activity>()), Times.Never);

                // Request evaluation of screen properties
                Dictionary<string, object> expected = GetClearedScreen();
                EvaluateScreen(expected, mockView);

                #endregion Assert
            }
        }

        // Test ID: AP4
        // Purpose: Test the presenter's response to the "Delete Activity" button being pressed
        [TestMethod()]
        [DataRow(true)]  //AP4a | Record is currently selected
        [DataRow(false)] //AP4b | No record currently selected
        public void DeleteRecord_Test(bool recordSelected)
        {
            using (var mock = AutoMock.GetLoose())
            {
                #region Arrange

                // Create mock layers
                var mockView = GetMockView();
                var mockModel = GetMockModel();

                // Create presenter to be tested, inject our mock model
                ActivityPresenter presenter = new ActivityPresenter(mockView.Object, mockModel.Object, 1);

                // Declare that the user will select "Yes" on the confirmation messagebox
                mockView.Setup(x => x.ShowMessageBox(It.IsAny<string>(), It.IsAny<string>(),
                                                     It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                                                     .Returns(DialogResult.Yes);

                // Keep a copy of the facade records list our presenter will be working with
                List<Activity> records = Mock_GetAllActivities().ToList();

                // Declare which record shall be selected from the list
                int index = 0;

                // Keep a copy of which record the presenter SHOULD select & push to view
                Activity activity = records[index];

                // Select our test case company, mimicking the user selecting it from a ListView
                if (recordSelected)
                    presenter.lvw_Activities_SelectedIndexChanged(new int[] { index });

                #endregion Arrange

                /**************************************************/

                #region Act

                //Invoke the call being tested
                presenter.btn_DeleteActivity_Click();

                #endregion Act

                /**************************************************/

                #region Assert

                // Ensure the presenter told the model to delete the record, only if a record was selected
                // Only passes if the CORRECT company was sent to the model
                if (recordSelected)
                {
                    mockModel.Verify(r => r.DeleteActivity(It.Is<Activity>(a => a.ActivityID == activity.ActivityID)), Times.Once);
                }
                else
                {
                    mockModel.Verify(r => r.DeleteActivity(It.IsAny<Activity>()), Times.Never);
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
        void EvaluateScreen(Dictionary<string, object> expected, Mock<IActivityGUI> screen)
        {
            // Declare actual values
            Dictionary<string, object> actual = new Dictionary<string, object>
            {
                { "ActivityID",    screen.Object.CurrentActivityID    },
                { "ActivityName",  screen.Object.CurrentActivityName  },
                { "ActivityCost",  screen.Object.CurrentActivityCost  },
                { "ActivityNotes", screen.Object.CurrentActivityNotes },
                { "CurrentDepartment_Enabled", screen.Object.CurrentActivity_Enabled }
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
                { "ActivityID",    "" },
                { "ActivityName",  "" },
                { "ActivityCost",  0m },
                { "ActivityNotes", "" },
                { "CurrentDepartment_Enabled", false }
            };
        }

        // ********************************************************************************
        // View Layer Mocking *************************************************************
        // ********************************************************************************

        // Initialises the mock screen to what mimic a form that has just loaded
        Mock<IActivityGUI> GetMockView()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var screen = mock.Mock<IActivityGUI>();
                screen.SetupAllProperties();
                // Screen mock needs to provide facade for properties changing
                // so the test can check they are correctly assigned
                screen.SetupProperty(x => x.CurrentActivityID)
                      .SetReturnsDefault("");
                screen.SetupProperty(x => x.CurrentActivityName)
                      .SetReturnsDefault("");
                screen.SetupProperty(x => x.CurrentActivityCost)
                      .SetReturnsDefault(decimal.Zero);
                screen.SetupProperty(x => x.CurrentActivityNotes)
                      .SetReturnsDefault("");
                screen.SetupProperty(x => x.CurrentActivity_Enabled)
                      .SetReturnsDefault(false);
                screen.SetupGet(x => x.ActivityList)
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
        Mock<IActivityModel> GetMockModel()
        {
            var mockModel = new Mock<IActivityModel>();
            mockModel.Setup(r => r.GetAllActivities()).Returns(Mock_GetAllActivities().ToList());
            mockModel.Setup(r => r.GetAllActivitiesFrom(It.IsAny<Booking>())).Returns(Mock_GetAllActivities().ToList());
            mockModel.Setup(r => r.FindActivity(It.IsAny<int>())).Returns<int>((id) => Mock_GetActivity(id));
            mockModel.Setup(r => r.FindActivity(It.IsAny<Activity>())).Returns<Activity>((a) => Mock_GetActivity(a.ActivityID));
            mockModel.Setup(r => r.SaveActivity(It.IsAny<Activity>())).Verifiable();
            mockModel.Setup(r => r.DeleteActivity(It.IsAny<Activity>())).Verifiable();
            mockModel.Setup(r => r.FindBooking(It.IsAny<int>())).Returns<int>((id) => Mock_GetBooking(id));
            mockModel.Setup(r => r.FindBooking(It.IsAny<Booking>())).Returns<Booking>((b) => Mock_GetBooking(b.BookingID));
            mockModel.Setup(r => r.UpdateBooking(It.IsAny<Booking>(), It.IsAny<List<int>>())).Verifiable();
            return mockModel;
        }

        // Mock Booking Data
        IQueryable<Booking> Mock_GetAllBookings()
        {
            Company    c = new Company    { CompanyID    = 1, Name = "Asda" };
            Department d = new Department { DepartmentID = 1, Name = "HR", Company = c };
            return new List<Booking>
            {
                new Booking { BookingID = 1, Name = "Xmas Party",    Department = d },
                new Booking { BookingID = 2, Name = "Dave's 40th",   Department = d },
                new Booking { BookingID = 3, Name = "Team Building", Department = d }
            }.AsQueryable();
        }
        Booking Mock_GetBooking(int id) => Mock_GetAllBookings().Where(d => d.BookingID == id).FirstOrDefault();

        // Mock Booking Data
        IQueryable<Activity> Mock_GetAllActivities()
        {
            return new List<Activity>
            {
                new Activity { ActivityID = 1, Name = "Chocolate producing and marketing",     Cost =  750, Notes = "" },
                new Activity { ActivityID = 2, Name = "Team building outdoor problem solving", Cost =  850, Notes = "" },
                new Activity { ActivityID = 3, Name = "Meditation and mindfulness workshop",   Cost =  500, Notes = "" },
                new Activity { ActivityID = 4, Name = "Wall climbing experience",              Cost =  500, Notes = "" },
                new Activity { ActivityID = 5, Name = "Go-cart Experience",                    Cost = 1400, Notes = "" }
            }.AsQueryable();
        }
        Activity Mock_GetActivity(int id) => Mock_GetAllActivities().Where(a => a.ActivityID == id).FirstOrDefault();
    }
}