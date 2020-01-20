//16007006 Andrew Patton
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP_Bookings.Models
{
    public interface IActivityModel
    {
        // Retrieve all records from Activities table
        List<Activity> GetAllActivities();

        // Retrieve activity from a specified ID
        Activity FindActivity(int activityID);
        Activity FindActivity(Activity activity);

        // Reports purely on the success/failure of activity retrieval
        bool ActivityExists(Activity activity);

        // Save record in Activities table
        void SaveActivity(Activity activity);

        // Delete record from Activities table
        void DeleteActivity(Activity activity);

        // The below functions access the BookingRepo, which breaks SRP
        // but given time constraints this acts as a quick fix

        // Retrieve all activities belonging to a specified booking
        List<Activity> GetAllActivitiesFrom(Booking booking);

        // Retrieve Booking from specified ID
        Booking FindBooking(int bookingID);
        Booking FindBooking(Booking booking);

        // Update Booking with new activity selection
        void UpdateBooking(Booking booking, List<int> newActivityList);
    }
}
