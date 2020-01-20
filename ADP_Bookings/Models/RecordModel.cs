//16007006 Andrew Patton
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public abstract class RecordModel
    {
        IUnitOfWorkFactory unitOfWorkFactory;
        public RecordModel(IUnitOfWorkFactory unitOfWorkFactory)
        {
            //In the case no uowfac was specified, create one
            if (unitOfWorkFactory == null)
                unitOfWorkFactory = new UnitOfWorkFactory();

            this.unitOfWorkFactory = unitOfWorkFactory;
        }
        // Centralises/Decouples any changes to the UoWFac - all models push their request upstream
        protected IUnitOfWork GetNewUnitOfWork() => unitOfWorkFactory.Create();
    }
}
