//16007006 Andrew Patton
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public interface ICompanyModel
    {
        // Retrieve all records in Companies table
        List<Company> GetAllCompanies();

        // Retrieve company from specified ID
        Company FindCompany(int companyID);
        Company FindCompany(Company company);

        // Reports purely success/failure of company retrieval
        bool CompanyExists(Company company);

        // Save record to Companies table - determine whether to Create/Update
        void SaveCompany(Company company);
        
        // Delete record in Companies table
        void DeleteCompany(Company company);        
    }
}
