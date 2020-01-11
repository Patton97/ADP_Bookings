using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public static class CompanyModel
    {
        // Create new record in Companies table
        public static void InsertNewCompany(Company company, IUnitOfWork uow)
        {
            using (var unitOfWork = uow)
            {
                unitOfWork.Companies.Add(company);
                unitOfWork.SaveChanges();
            }
        }

        // Retrieve all records in Companies table
        public static List<Company> GetAllCompanies(IUnitOfWork uow)
        {
            using (var unitOfWork = uow)
            {
                return unitOfWork.Companies.GetAll(true).ToList(); //bool param specifies eager loading of FK data
            }
        }

        // Retrieve company from specified ID
        public static Company FindCompany(int companyID, IUnitOfWork uow)
        {
            using (var unitOfWork = uow)
            {
                return unitOfWork.Companies.Get(companyID);
            }
        }
        public static Company FindCompany(Company company, IUnitOfWork uow) => FindCompany(company.CompanyID, uow);

        // Reports purely success/failure of company retrieval
        public static bool CompanyExists(Company company, IUnitOfWork uow) => FindCompany(company, uow) != null;

        // Update existing record in Companies table
        public static void UpdateCompany(Company company, IUnitOfWork uow)
        {
            using (var unitOfWork = uow)
            {
                unitOfWork.Companies.Update(company);
                unitOfWork.SaveChanges();
            }
        }

        // Delete record in Companies table
        public static void DeleteCompany(Company company, IUnitOfWork uow)
        {
            using (var unitOfWork = uow)
            {
                //Ensure record actually exists before attempting to delete
                //Current uow var is sent so both operations occur within same UoW
                if (!CompanyExists(company,uow)) 
                {
                    Console.WriteLine("ERROR: Record delete failed!\n"
                                    + "       Company: #" + company.CompanyID + "could not be found.");
                }
                else
                {
                    unitOfWork.Companies.Remove(unitOfWork.Companies.Get(company.CompanyID));
                    unitOfWork.SaveChanges();
                }
            }
        }
    }
}
