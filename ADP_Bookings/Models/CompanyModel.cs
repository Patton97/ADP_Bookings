using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    //Used as a store for all calls to the UoW from Presenter
    //static class because this solely acts as a window into the UoW
    static class CompanyModel
    {
        //Gets all companies in DB
        public static List<Company> GetAllCompanies()
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Companies.GetAll(true).ToList(); //bool param specifies eager loading of FK data
            }
        }

        //NOTE: Might be better only in DepartmentModel
        //Get all departments belonging to specified company
        public static List<Department> GetAllDepartmentsFrom(Company company)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Departments.GetDepartmentsFromCompany(company).ToList();
            }
        }

        //Retrieve company from specified ID
        public static Company FindCompany(Company company)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Companies.Get(company.CompanyID);
            }
        }
        //Reports purely success/failure of company retrieval
        public static bool CompanyExists(Company company) => FindCompany(company) != null;

        //
        public static void InsertNewCompany(Company company)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                unitOfWork.Companies.Add(company);
                unitOfWork.SaveChanges();
            }
        }

        public static void UpdateCompany(Company company)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                unitOfWork.Companies.Update(company);
                unitOfWork.SaveChanges();
            }
        }

        public static void DeleteCompany(Company company)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                unitOfWork.Companies.Remove(unitOfWork.Companies.Get(company.CompanyID));
                unitOfWork.SaveChanges();
            }
        }
    }
}
