using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADP_Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Moq;

namespace ADP_Bookings.Models.Tests
{
    [TestClass()]
    public class UnitOfWork_Tests
    {
        // Ensure UoW class passes call to SaveChanges() through to DBContext
        [TestMethod()]
        public void SaveChanges_Test()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<ADP_DBContext>()
                    .Setup(x => x.SaveChanges());
                var uow = mock.Create<UnitOfWork>();

                //Act
                uow.SaveChanges();


                //Assert
                mock.Mock<ADP_DBContext>()
                    .Verify(x => x.SaveChanges(),Times.Exactly(1));
            }                
        }
    }
}