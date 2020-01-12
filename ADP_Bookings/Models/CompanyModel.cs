using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public class CompanyModel : RecordModel, ICompanyModel
    {
        public CompanyModel(IUnitOfWorkFactory unitOfWorkFactory = null) : base(unitOfWorkFactory) { }

        // Retrieve all records in Companies table
        public List<Company> GetAllCompanies()
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                return unitOfWork.Companies.GetAll(true).ToList(); //bool param specifies eager loading of FK data
            }
        }

        // Retrieve company from specified ID
        public Company FindCompany(int companyID)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                return unitOfWork.Companies.Get(companyID);
            }
        }
        public Company FindCompany(Company company) => FindCompany(company.CompanyID);

        // Reports purely success/failure of company retrieval
        public bool CompanyExists(Company company) => FindCompany(company) != null;
        
        // Save record in Companies table
        public void SaveCompany(Company company)
        {
            // Determine whether to Create/Update
            if (CompanyExists(company))
                UpdateCompany(company);
            else
                CreateCompany(company);
        }

        // Delete record in Companies table
        public void DeleteCompany(Company company)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                //Ensure record actually exists before attempting to delete
                if (!CompanyExists(company)) 
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

        // The below functions are private to force any Presenter to simply 
        // call SaveCompany(), and allow the Model to take over from there

        // Create new record in Companies table
        private void CreateCompany(Company company)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                unitOfWork.Companies.Add(company);
                unitOfWork.SaveChanges();
            }
        }

        // Update existing record in Companies table
        private void UpdateCompany(Company company)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                unitOfWork.Companies.Update(company);
                unitOfWork.SaveChanges();
            }
        }
    }
}
