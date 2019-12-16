using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADP_Bookings.Forms
{
    public partial class frm_companies : Form
    {
        public frm_companies()
        {
            InitializeComponent();
        }

        private void frm_companies_Load(object sender, EventArgs e)
        {
            dgv_companies_init();
        }

        void dgv_companies_init()
        {
            dgv_companies.ReadOnly = true;
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                List<Company> companies = new List<Company>();
                companies.AddRange(unitOfWork.Companies.GetAll(true));
                dgv_companies.DataSource = companies;
                for(int i = 0; i < companies.Count; i++)
                {
                    dgv_companies.Rows[i].Cells["Departments"].Value = companies[i].Departments;
                }
            }
        }
    }
}
