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

namespace ADP_Bookings.Presenters.Tests
{
    [TestClass()]
    public class CompanyPresenter_Tests
    {
        //Pretend method
        List<Company> GetAllCompanies()
        {
            return new List<Company>
            {
                new Company()
                {
                    CompanyID=0,
                    Name = "Asda"
                },
                new Company()
                {
                    CompanyID=1,
                    Name = "Boots"
                },
                new Company()
                {
                    CompanyID=2,
                    Name = "Costco"
                },
                new Company()
                {
                    CompanyID=2,
                    Name = "Costco"
                }
            };
        }
        
        

        [TestMethod()]
        public void LoadNewRecord_Test()
        {
            // ********************************************************************************
            // Arrange ************************************************************************
            // ********************************************************************************
                
            // Mimic a frm_companies being loaded
            var screen = InitMockScreen();               

            // Send null screen param, automock will fill in for it later
            CompanyPresenter presenter = new CompanyPresenter(screen.Object);

            // ********************************************************************************
            // Act ****************************************************************************
            // ********************************************************************************

            // Set the screen's CurrentCompanyName to "Foobar" so our
            // presenter can (attempt to) overwrite it with an empty string
            screen.Object.CurrentCompanyName = "FooBar";

            //Invoke the call being tested
            presenter.btn_AddCompany_Click();
            
            // ********************************************************************************
            // Assert *************************************************************************
            // ********************************************************************************

            // Declare expected values
            Dictionary<string, object> expected = new Dictionary<string, object>
            {
                { "CompanyID",   "0" },
                { "CompanyName", "" },
                { "CurrentCompany_Enabled", true }
            };

            // Request evaluation of screen properties
            EvaluateScreen(expected, screen);
        }

        [TestMethod()]
        public void LoadRecord_Test()
        {
            // ********************************************************************************
            // Arrange ************************************************************************
            // ********************************************************************************

            // Mimic a frm_companies being loaded
            var screen = InitMockScreen();

            // Create presenter to be tested
            CompanyPresenter presenter = new CompanyPresenter(screen.Object);

            // This is the company we will be asking the presenter to load
            Company company = new Company()
            {
                CompanyID = 1,
                Name = "Asda",
            };                

            // Place our test record amongst a list of other companies to ensure not
            // only the presenter both selects the correct company and then loads it
            // This is in place of the presenter collecting the list from the DB
            List<Company> records = GetAllCompanies();

            // Store position to ensure the test itself does not mimic selecting the wrong record
            int testCasePosition = 2;
            records[testCasePosition] = company;

            // NOTE: Accessing the presenter's private member(s) is poor practice 
            //       during testing and can be avoided but due to documented teamwork 
            //       issues I am short on time for this assignment..
            new PrivateObject(presenter).SetField("records", records);

            // ********************************************************************************
            // Act ****************************************************************************
            // ********************************************************************************

            // Mimic a new item being selected from the form's lvw_companies
            presenter.lvw_companies_SelectedIndexChanged(new int[] { testCasePosition });

            // ********************************************************************************
            // Assert *************************************************************************
            // ********************************************************************************

            // Declare expected values
            Dictionary<string, object> expected = new Dictionary<string, object>
            {
                { "CompanyID",   records[testCasePosition].CompanyID.ToString() },
                { "CompanyName", records[testCasePosition].Name },
                { "CurrentCompany_Enabled", true }
            };

            // Request evaluation of screen properties
            EvaluateScreen(expected, screen);
        }

        // Evaluate the current properties on screen following test
        // NOTE: Reduces code duplication, but there is dispute 
        //       as to whether Unit Tests should be intra-dependant
        // NOTE: VS throws a warning about TestMethods using parameters.
        //       There's probably a much better way of doing this
        [TestMethod()]
        void EvaluateScreen(Dictionary<string,object> expected, Moq.Mock<ICompanyGUI> screen)
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

        [TestMethod()]
        public void SaveRecord_Test()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // ********************************************************************************
                // Arrange ************************************************************************
                // ********************************************************************************

                // Mimic a frm_companies being loaded
                var screen = InitMockScreen();

                // Create presenter to be tested
                CompanyPresenter presenter = new CompanyPresenter(screen.Object);

                // This is the company we will be asking the presenter to save
                Company company = new Company()
                {
                    CompanyID = 1,
                    Name = "Asda"
                };

                // This function attempts to touch the Model layer (UoW, DB), 
                // so we need to mock those elements
                mock.Mock<UnitOfWork>()
                    .SetupProperty(x => x.Companies)
                    .SetReturnsDefault(new IQueryable());

                // ********************************************************************************
                // Act ****************************************************************************
                // ********************************************************************************

                new PrivateObject(presenter).SetField("selectedRecord", company);

                //Invoke the call being tested
                presenter.btn_ConfirmChanges_Click();

                // ********************************************************************************
                // Assert *************************************************************************
                // ********************************************************************************

                // Declare expected values for screen properties
                Dictionary<string, object> expected = new Dictionary<string, object>
                {
                    { "CompanyID",   "" },
                    { "CompanyName", "" },
                    { "CurrentCompany_Enabled", false }
                };

                // Request evaluation of screen properties
                EvaluateScreen(expected, screen);
            }
        }
    }
}