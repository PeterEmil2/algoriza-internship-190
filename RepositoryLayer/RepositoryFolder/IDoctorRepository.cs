using DomainLayer.Models;
using DomainLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.RepositoryFolder
{
    public interface IDoctorRepository
    {
        bool Login(string userName, string Password);
        IEnumerable<Booking> GetAllDoctorPatients(string SearchBy,int? Page, int pageSize, string Search);

        bool confirmCheckUp(int bookingId);
        bool addAppointment(int doctorId ,int Price, DayOfWeek days,List<timeViewModel> times);
        bool updateAppointment(int doctorId, int appointementId, DayOfWeek fromDay, string fromTimeValue, DayOfWeek toDay, string toTimeValue);

        bool deleteAppointment(int appointementId ,int timeId);
      
    }
}
