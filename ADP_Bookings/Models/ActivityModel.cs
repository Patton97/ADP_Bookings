using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    //Used as a store for all calls to the UoW from Presenter
    //static class because this solely acts as a window into the UoW
    static class ActivityModel
    {
        public static List<Activity> GetAllActivities()
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Activities.GetAll().ToList();
            }
        }

        /*
        public static List<Activity> GetAllActivitiesFrom(Booking booking)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Activities.GetAllActivitiesFromBooking(booking, true).ToList();
            }
        }
        */

        public static Activity FindActivity(Activity activity)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                return unitOfWork.Activities.Get(activity.ActivityID);
            }
        }
        public static bool ActivityExists(Activity activity) => FindActivity(activity) != null;

        public static void InsertNewActivity(Activity activity)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                unitOfWork.Activities.Add(activity);
                unitOfWork.SaveChanges();
            }
        }

        public static void UpdateActivity(Activity activity)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                unitOfWork.Activities.Update(activity);
                unitOfWork.SaveChanges();
            }
        }

        public static void DeleteActivity(Activity activity)
        {
            using (var unitOfWork = new UnitOfWork(new ADP_DBContext()))
            {
                unitOfWork.Activities.Remove(unitOfWork.Activities.Get(activity.ActivityID));
                unitOfWork.SaveChanges();
            }
        }
    }
}
