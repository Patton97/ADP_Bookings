using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADP_Bookings.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;

using ADP_Bookings.Views; //Enable mocking of views/forms
using ADP_Bookings.Models;//

namespace ADP_Bookings.Presenters.Tests
{
    [TestClass()]
    public class CompanyPresenter_Tests
    {
        [TestMethod()]
        public void CreateCompany_Test()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ICompanyRepository>()
                    .Setup(x => x.Add(company);
                CompanyPresenter presenter = new CompanyPresenter(mock);
            }
                
        }
    }
}