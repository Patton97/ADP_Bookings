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
    public class DepartmentModel_Tests
    {
        // Retrieve all records in Departments table
        [TestMethod]
        public void GetAllDepartments_Test()
        {
            #region Arrange
            // Mock Data-Access Layer
            var mockUoWFactory = GetMockUoWFactory();

            // Create model to be tested, inject our mock uowfactory
            DepartmentModel model = new DepartmentModel(mockUoWFactory.Object);

            // Declare the expected return 
            List<Department> expected = Mock_GetAllDepartments().ToList();

            #endregion Arrange

            /**************************************************/

            #region Act

            List<Department> actual = model.GetAllDepartments();

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

        // Retrieve all records in Departments table from a specific company
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void GetAllDepartmentsFromCompany_Test(int companyID)
        {
            #region Arrange
            // Mock Data-Access Layer
            var mockUoWFactory = GetMockUoWFactory();

            // Create model to be tested, inject our mock uowfactory
            DepartmentModel model = new DepartmentModel(mockUoWFactory.Object);

            // Create the company - only ID matters to the department, name is irrelevant
            Company company = new Company() { CompanyID = companyID, Name = "Test" };

            // Declare the expected return 
            List<Department> expected = Mock_GetAllDepartments().Where(d=>d.Company.CompanyID == companyID).ToList();

            #endregion Arrange

            /**************************************************/

            #region Act

            List<Department> actual = model.GetAllDepartmentsFrom(company);

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

        // Retrieve department from specified ID
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        public void FindDepartment_Test(int departmentID)
        {
            #region Arrange

            // Mock Data-Access Layer
            var mockUoWFactory = GetMockUoWFactory();

            // Create model to be tested, inject our mock uowfactory
            DepartmentModel model = new DepartmentModel(mockUoWFactory.Object);

            // Declare the expected return
            Department expected = Mock_GetDepartment(departmentID);

            #endregion Arrange

            /**************************************************/

            #region Act

            Department actual = model.FindDepartment(departmentID);

            #endregion Act

            /**************************************************/

            #region Assert

            Evaluate(expected, actual);

            #endregion Assert
        }

        // Reports purely success/failure of department retrieval
        [TestMethod]
        [DataRow(1, true)]
        [DataRow(3, true)]
        [DataRow(5, false)]
        public void DepartmentExists_Test(int departmentID, bool exists)
        {
            #region Arrange

            // Mock Data-Access Layer
            var mockUoWFactory = GetMockUoWFactory();

            // Create model to be tested, inject our mock uowfactory
            DepartmentModel model = new DepartmentModel(mockUoWFactory.Object);

            // Declare the expected return
            bool expected = exists;

            // Get the Department we wish to look for
            Department department = Mock_GetDepartment(departmentID);

            #endregion Arrange

            /**************************************************/

            #region Act & Assert

            //For this test, act & assert overlap
            if (exists)
                Assert.AreEqual(expected, model.DepartmentExists(department));
            else
                Assert.ThrowsException<NullReferenceException>(() => model.DepartmentExists(department));

            #endregion Act & Assert
        }

        // Save record to Departments table - determine whether to Create/Update
        [TestMethod]
        [DataRow(1, true, "HR_NewName")]
        [DataRow(3, true, "Legal_NewName")]
        [DataRow(5, false, "R&D")]
        public void SaveDepartment_Test(int departmentID, bool exists, string newName)
        {
            #region Arrange

            // Mock Data-Access Layer
            // Verbose in this test as we need a reference to mockRepo to verify its behaviour
            var mockRepo = GetMockDepartmentRepo();
            var mockUoW = GetMockUoW(mockRepo.Object);
            var mockUoWFactory = GetMockUoWFactory(mockUoW.Object);

            // Create model to be tested, inject our mock uowfactory
            DepartmentModel model = new DepartmentModel(mockUoWFactory.Object);

            // Get a department to save
            // If test case is marked as existing, retrieve it from mock data list, otherwise create it
            Department department = exists ? Mock_GetDepartment(departmentID) : new Department(departmentID, "", new Company(1, "Asda"));

            // Update the department's name
            department.Name = newName;

            #endregion Arrange

            /**************************************************/

            #region Act

            // Tell the model we wish to save the department
            model.SaveDepartment(department);

            #endregion Act

            /**************************************************/

            #region Assert

            // Ensure the repo was told to update/add depending on the record's prior existance
            // NOTE: ONLY passes if the *exact* department was passed
            if (exists)
                mockRepo.Verify(x => x.Update(department));
            else
                mockRepo.Verify(x => x.Add(department));

            #endregion Assert
        }

        // Delete record in Departments table
        [TestMethod]
        [DataRow(1, true)]
        [DataRow(2, true)]
        [DataRow(5, false)]
        public void DeleteDepartment_Test(int departmentID, bool exists)
        {
            #region Arrange

            // Mock Data-Access Layer
            // Verbose in this test as we need a reference to mockRepo to verify its behaviour
            var mockRepo = GetMockDepartmentRepo();
            var mockUoW = GetMockUoW(mockRepo.Object);
            var mockUoWFactory = GetMockUoWFactory(mockUoW.Object);

            // Create model to be tested, inject our mock uowfactory
            DepartmentModel model = new DepartmentModel(mockUoWFactory.Object);

            // Get a department to save
            // If test case is marked as existing, retrieve it from mock data list, otherwise create it
            Department department = exists ? Mock_GetDepartment(departmentID) : new Department(departmentID, "R&D", new Company(1, "Asda"));

            #endregion Arrange

            /**************************************************/

            #region Act

            // Tell the model we wish to save the department
            model.DeleteDepartment(department);

            #endregion Act

            /**************************************************/

            #region Assert

            // Ensure the repo was told to delete the record, only if it is marked as existing
            if (exists)
                mockRepo.Verify(x => x.Remove(It.IsAny<Department>()), Times.Once);
            else
                mockRepo.Verify(x => x.Remove(It.IsAny<Department>()), Times.Never);

            #endregion Assert
        }

        // ********************************************************************************
        // Utility Functions **************************************************************
        // ******************************************************************************** 

        // Evaluate the properties of two Department objects - entire function is "Assert"
        void Evaluate(Department expected, Department actual)
        {
            // Assert.AreEqual is strict on instances, so we manually comb each field
            Assert.AreEqual(expected.DepartmentID, actual.DepartmentID);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Bookings.Count, actual.Bookings.Count);
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
        Mock<IUnitOfWork> GetMockUoW(IDepartmentRepository mockRepo = null)
        {
            if (mockRepo == null)
                mockRepo = GetMockDepartmentRepo().Object;

            using (var mock = AutoMock.GetLoose())
            {
                var mockUoW = mock.Mock<IUnitOfWork>();
                mockUoW.Setup(x => x.Departments).Returns(mockRepo);
                return mockUoW;
            }
        }

        // ********************************************************************************
        // Repository Mocking *************************************************************
        // ********************************************************************************

        Mock<IDepartmentRepository> GetMockDepartmentRepo()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var mockDepartmentRepo = mock.Mock<IDepartmentRepository>();
                mockDepartmentRepo.Setup(x => x.GetAll(It.IsAny<bool>())).Returns(Mock_GetAllDepartments().ToList());
                mockDepartmentRepo.Setup(x => x.Get(It.IsAny<int>())).Returns<int>((id) => Mock_GetDepartment(id));
                mockDepartmentRepo.Setup(x => x.Add(It.IsAny<Department>()));
                mockDepartmentRepo.Setup(x => x.Update(It.IsAny<Department>()));
                mockDepartmentRepo.Setup(x => x.Remove(It.IsAny<Department>()));
                mockDepartmentRepo.Setup(x => x.GetDepartmentsFromCompany(It.IsAny<Company>(),It.IsAny<bool>()))
                                  .Returns<Company, bool>((c,b) => (Mock_GetAllDepartments().Where(d => d.Company.CompanyID == c.CompanyID)));
                return mockDepartmentRepo;
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

        IQueryable<Department> Mock_GetAllDepartments()
        {
            return new List<Department>
            {
                new Department { DepartmentID = 1, Name = "HR",        Company = Mock_GetCompany(1) },
                new Department { DepartmentID = 2, Name = "Legal",     Company = Mock_GetCompany(1) },
                new Department { DepartmentID = 3, Name = "Marketing", Company = Mock_GetCompany(1) },
                new Department { DepartmentID = 4, Name = "IT",        Company = Mock_GetCompany(1) },

                new Department { DepartmentID = 1, Name = "HR",        Company = Mock_GetCompany(2) },
                new Department { DepartmentID = 2, Name = "Legal",     Company = Mock_GetCompany(2) },
                new Department { DepartmentID = 3, Name = "Marketing", Company = Mock_GetCompany(2) },
                new Department { DepartmentID = 4, Name = "IT",        Company = Mock_GetCompany(2) }
            }.AsQueryable();
        }
        Department Mock_GetDepartment(int id) => Mock_GetAllDepartments().Where(c => c.DepartmentID == id).FirstOrDefault();
    }
}