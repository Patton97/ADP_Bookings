//16007006 Andrew Patton
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public class ActivityModel : RecordModel, IActivityModel
    {
        public ActivityModel(IUnitOfWorkFactory unitOfWorkFactory = null) : base(unitOfWorkFactory) { }

        // Retrieve all records from Activities table
        public List<Activity> GetAllActivities()
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                return unitOfWork.Activities.GetAll().ToList();
            }
        }       

        // Retrieve activity from a specified ID
        public Activity FindActivity(int activityID)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                return unitOfWork.Activities.Get(activityID);
            }
        }
        public Activity FindActivity(Activity activity) => FindActivity(activity.ActivityID);

        // Reports purely on the success/failure of activity retrieval
        public bool ActivityExists(Activity activity) => FindActivity(activity) != null;

        // Save record in Activities table
        public void SaveActivity(Activity activity)
        {
            // Determine whether to Create/Update
            if (ActivityExists(activity))
                UpdateActivity(activity);
            else
                CreateActivity(activity);
        }

        // Delete record from Activities table
        public void DeleteActivity(Activity activity)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                unitOfWork.Activities.Remove(unitOfWork.Activities.Get(activity.ActivityID));
                unitOfWork.SaveChanges();
            }
        }

        // The below functions are private to force any Presenter to simply 
        // call SaveActivity(), and allow the Model to take over from there

        // Create new record in Activities table
        private void CreateActivity(Activity activity)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                //Force activity to use correct department - ambiguity can arise leading to record duplication
                unitOfWork.Activities.Add(activity);
                unitOfWork.SaveChanges();
            }
        }

        // Update existing record in Activities table
        private void UpdateActivity(Activity activity)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                //Force activity to use correct department - ambiguity can arise leading to record duplication
                unitOfWork.Activities.Update(activity);
                unitOfWork.SaveChanges();
            }
        }

        // The below functions access the ActivityRepo, which breaks SRP
        // but given time constraints this acts as a quick fix

        // Retrieve all activities belonging to a specified activity
        public List<Activity> GetAllActivitiesFrom(Booking booking)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                return unitOfWork.Bookings.Get(booking.BookingID, true).Activities.ToList();
            }
        }

        // Retrieve Activity from specified ID
        public Booking FindBooking(int bookingID)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                return unitOfWork.Bookings.Get(bookingID, true);
            }
        }
        public Booking FindBooking(Booking booking) => FindBooking(booking.BookingID);

        // Update booking with new activity selection
        public void UpdateBooking(Booking booking, List<int> newActivityList)
        {
            using (var unitOfWork = GetNewUnitOfWork())
            {
                List<Activity> oldActivityList = unitOfWork.Bookings.Get(booking.BookingID).Activities.ToList();
                //First remove all activities from DB record
                foreach (Activity a in oldActivityList)
                    unitOfWork.Bookings.Get(booking.BookingID).Activities.Remove(a);

                //Then add new selection
                foreach (int a_id in newActivityList)
                    unitOfWork.Bookings.Get(booking.BookingID).Activities.Add(unitOfWork.Activities.Get(a_id));

                unitOfWork.SaveChanges();
            }
        }
    }
}
