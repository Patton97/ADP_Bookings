using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Project Specific imports
// Required due to folder structure
using ADP_Bookings.Models;
using ADP_Bookings.Views;

namespace ADP_Bookings.Presenters
{
    public class CompanyPresenter
    {
        private ICompanyGUI screen;
        private CompanyModel model;

        public CompanyPresenter(ICompanyGUI screen)
        {
            this.screen = screen;
            model = new CompanyModel();
            screen.Register(this);
            InitialiseForm();
        }

        void InitialiseForm()
        {
            //scrren.property = value;
        }

        
    }
}
