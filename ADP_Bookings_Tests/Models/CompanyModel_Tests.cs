using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADP_Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Autofac.Extras.Moq;

namespace ADP_Bookings.Models.Tests
{
    [TestClass()]
    public class CompanyModel_Tests
    {
        // Retrieve all records in Companies table
        [TestMethod]
        public void GetAllCompanies_Test()
        {
            #region Arrange
            // Mock Data-Access Layer
            var mockUoWFactory = GetMockUoWFactory();

            // Create model to be tested, inject our mock uowfactory
            CompanyModel model = new CompanyModel(mockUoWFactory.Object);

            // Declare the expected return 
            List<Company> expected = Mock_GetAllCompanies().ToList();

            #endregion Arrange

            /**************************************************/

            #region Act

            List<Company> actual = model.GetAllCompanies();

            #endregion Act

            /**************************************************/

            #region Assert

            // Ensure size of list is as expected
            Assert.AreEqual(expected.Count, actual.Count);

            // Ensure contents of list is as expected
            // Assert.AreEqual is strict on instances, so we manually comb each record
            for (int i = 0; i < expected.Count; i++)
                Evaluate(expected[i], actual[i]);

            #endregion Assert
        }

        // Retrieve company from specified ID
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        public void FindCompany_Test(int companyID)
        {
            #region Arrange

            // Mock Data-Access Layer
            var mockUoWFactory = GetMockUoWFactory();

            // Create model to be tested, inject our mock uowfactory
            CompanyModel model = new CompanyModel(mockUoWFactory.Object);

            // Declare the expected return
            Company expected = Mock_GetCompany(companyID);

            #endregion Arrange

            /**************************************************/

            #region Act

            Company actual = model.FindCompany(companyID);

            #endregion Act

            /**************************************************/

            #region Assert

            Evaluate(expected, actual);            

            #endregion Assert
        }

        // Reports purely success/failure of company retrieval
        [TestMethod]
        [DataRow(1, true)]
        [DataRow(3, true)]
        [DataRow(5, false)]
        public void CompanyExists_Test(int companyID, bool exists)
        {
            #region Arrange

            // Mock Data-Access Layer
            var mockUoWFactory = GetMockUoWFactory();

            // Create model to be tested, inject our mock uowfactory
            CompanyModel model = new CompanyModel(mockUoWFactory.Object);

            // Declare the expected return
            bool expected = exists;

            // Get the Company we wish to look for
            Company company = Mock_GetCompany(companyID);

            #endregion Arrange

            /**************************************************/

            #region Act & Assert

            //For this test, act & assert overlap
            if(exists)
                Assert.AreEqual(expected, model.CompanyExists(company));
            else
                Assert.ThrowsException<NullReferenceException>(() => model.CompanyExists(company)); 

            #endregion Act & Assert
        }

        // Save record to Companies table - determine whether to Create/Update
        [TestMethod]
        [DataRow(1, true, "Asda_NewName")]
        [DataRow(3, true, "Costco_NewName")]
        [DataRow(5, false, "eBay")]
        public void SaveCompany_Test(int companyID, bool exists, string newName)
        {
            #region Arrange

            // Mock Data-Access Layer
            // Verbose in this test as we need a reference to mockRepo to verify its behaviour
            var mockRepo = GetMockCompanyRepo();
            var mockUoW = GetMockUoW(mockRepo.Object);
            var mockUoWFactory = GetMockUoWFactory(mockUoW.Object);

            // Create model to be tested, inject our mock uowfactory
            CompanyModel model = new CompanyModel(mockUoWFactory.Object);

            // Get a company to save
            // If test case is marked as existing, retrieve it from mock data list, otherwise create it
            Company company = exists ? Mock_GetCompany(companyID) : new Company(companyID, "");

            // Update the company's name
            company.Name = newName;

            #endregion Arrange

            /**************************************************/

            #region Act

            // Tell the model we wish to save the company
            model.SaveCompany(company);

            #endregion Act

            /**************************************************/

            #region Assert

            // Ensure the repo was told to update/add depending on the record's prior existance
            // NOTE: ONLY passes if the *exact* company was passed
            if (exists)
                mockRepo.Verify(x => x.Update(company));
            else
                mockRepo.Verify(x => x.Add(company));

            #endregion Assert
        }

        // Delete record in Companies table
        [TestMethod]
        [DataRow(1, true)]
        [DataRow(2, true)]
        [DataRow(5, false)]
        public void DeleteCompany_Test(int companyID, bool exists)
        {
            #region Arrange

            // Mock Data-Access Layer
            // Verbose in this test as we need a reference to mockRepo to verify its behaviour
            var mockRepo = GetMockCompanyRepo();
            var mockUoW = GetMockUoW(mockRepo.Object);
            var mockUoWFactory = GetMockUoWFactory(mockUoW.Object);

            // Create model to be tested, inject our mock uowfactory
            CompanyModel model = new CompanyModel(mockUoWFactory.Object);

            // Get a company to save
            // If test case is marked as existing, retrieve it from mock data list, otherwise create it
            Company company = exists ? Mock_GetCompany(companyID) : new Company(companyID, "eBay");

            #endregion Arrange

            /**************************************************/

            #region Act

            // Tell the model we wish to save the company
            model.DeleteCompany(company);

            #endregion Act

            /**************************************************/

            #region Assert

            // Ensure the repo was told to delete the record, only if it is marked as existing
            if (exists)
                mockRepo.Verify(x => x.Remove(It.IsAny<Company>()), Times.Once);
            else
                mockRepo.Verify(x => x.Remove(It.IsAny<Company>()), Times.Never);

            #endregion Assert
        }

        // ********************************************************************************
        // Utility Functions **************************************************************
        // ******************************************************************************** 

        // Evaluate the properties of two Company objects - entire function is "Assert"
        void Evaluate(Company expected, Company actual)
        {
            // Assert.AreEqual is strict on instances, so we manually comb each field
            Assert.AreEqual(expected.CompanyID, actual.CompanyID);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Departments.Count, actual.Departments.Count);
        }

        // ********************************************************************************
        // Unit of Work Mocking ***********************************************************
        // ********************************************************************************

        Mock<IUnitOfWorkFactory> GetMockUoWFactory(IUnitOfWork mockUoW = null)
        {
            if (mockUoW == null)
                mockUoW = GetMockUoW().Object;

            using (var mock = AutoMock.GetLoose())
            {
                var mockUoWFactory = mock.Mock<IUnitOfWorkFactory>();
                mockUoWFactory.Setup(x => x.Create()).Returns(mockUoW);
                return mockUoWFactory;
            }
        }
        Mock<IUnitOfWork> GetMockUoW(ICompanyRepository mockRepo = null)
        {
            if (mockRepo == null)
                mockRepo = GetMockCompanyRepo().Object;

            using (var mock = AutoMock.GetLoose())
            {
                var mockUoW = mock.Mock<IUnitOfWork>();
                mockUoW.Setup(x => x.Companies).Returns(mockRepo);
                return mockUoW;
            }
        }

        // ********************************************************************************
        // Repository Mocking *************************************************************
        // ********************************************************************************

        Mock<ICompanyRepository> GetMockCompanyRepo()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var mockCompanyRepo = mock.Mock<ICompanyRepository>();
                mockCompanyRepo.Setup(x => x.GetAll(It.IsAny<bool>())).Returns(Mock_GetAllCompanies().ToList());
                mockCompanyRepo.Setup(x => x.Get(It.IsAny<int>())).Returns<int>((id) => Mock_GetCompany(id));
                mockCompanyRepo.Setup(x => x.Add(It.IsAny<Company>()));
                mockCompanyRepo.Setup(x => x.Update(It.IsAny<Company>()));
                mockCompanyRepo.Setup(x => x.Remove(It.IsAny<Company>()));
                return mockCompanyRepo;
            }            
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