using Hospital_Management_System.Data;
using Hospital_Management_System.Entities;

namespace Hospital_Management_System.Manager
{
    public class AppointmentManager
    {
        private readonly HMSDBContext _dbcontext;

        public AppointmentManager(HMSDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void ScheduleAppointment(Appointment appointment)
        {

        }
    }
}
