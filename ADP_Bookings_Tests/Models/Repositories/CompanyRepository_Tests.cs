using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADP_Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Autofac.Extras.Moq;

namespace ADP_Bookings.Models.Tests
{
    [TestClass()]
    public class CompanyRepository_Tests
    {
        [TestMethod()]
        public void GetAll_Test()
        {
            using (var mock = AutoMock.GetLoose())
            {
                /*mock.Mock<ADP_DBContext>()
                    .Setup(x=>x.Set<Company>())
                    .Returns(GetAllCompanies());*/

                var repo = mock.Create<CompanyRepository>();

                var actual = repo.GetAll();

                var expected = GetAllCompanies();
            }
        }

        IQueryable<Company> GetAllCompanies()
        {
            return new List<Company>
            {
                new Company()
                {
                    CompanyID = 1,
                    Name = "Asda",
                    Departments = new List<Department>()
                },
                new Company()
                {
                    CompanyID = 2,
                    Name = "Boots",
                    Departments = new List<Department>()
                },
                new Company()
                {
                    CompanyID = 3,
                    Name = "Costco",
                    Departments = new List<Department>()
                },
            }.AsQueryable();
        }
    }
}