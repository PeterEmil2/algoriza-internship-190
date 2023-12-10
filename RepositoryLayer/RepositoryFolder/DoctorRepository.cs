using DomainLayer.Models;
using DomainLayer.ViewModel;
using Microsoft.AspNetCore.Identity;
using Repository.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using PagedList.Mvc;

namespace RepositoryLayer.RepositoryFolder
{
    public class DoctorRepository : IDoctorRepository
    {
        appcontext _context;
        public DoctorRepository(appcontext context)
        {
            _context = context;  
        }
        bool IDoctorRepository.addAppointment(int doctorId ,int Price, DayOfWeek days, List<timeViewModel> times)
        {

           
            var d = _context.Doctors.FirstOrDefault(x => x.id==doctorId);
            if (d != null)
            {
                d.Price = Price;
                _context.Doctors.Update(d);
                Appointement appointement = new Appointement();
                appointement.Availability = (DaysOfWeek)days;
                appointement.doctorId = doctorId;
                _context.Appointements.Add(appointement);

                _context.SaveChanges();

                if (times != null && times.Any())
                {

                    foreach (timeViewModel t in times)
                    {
                        Console.WriteLine(appointement.id);
                        Time tme = new Time();
                        tme.appointementId = appointement.id;

                        tme.time = t.time;
                    
                        Console.WriteLine(tme.time);
                        _context.Times.Add(tme);
                 
                    }
                    _context.SaveChanges();
                }
                return true;

            }
            return false;
        }

        bool IDoctorRepository.confirmCheckUp(int bookingId)
        {

            var b = _context.Bookings.First(x => x.id == bookingId);
            if (b != null)
            {
                b.bookingStatus = "Confirmed";
                _context.Bookings.Update(b);
                _context.SaveChanges();
                return true;
            }
            return false;


       }

        bool IDoctorRepository.deleteAppointment(int appointementId, int timeId)
        {
            var isFound = _context.Times.FirstOrDefault(x => x.appointementId == appointementId && x.id == timeId);
            if (isFound != null)
            {
                _context.Times.Remove(isFound);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    

        IEnumerable<Booking> IDoctorRepository.GetAllDoctorPatients(string SearchBy, int? Page, int pageSize, string Search)
        {
            if (SearchBy == "Date")
            {
                return _context.Bookings.Where(x => x.bookingDay.ToString() == Search || Search == null).ToList().ToPagedList(Page ?? 1, pageSize);
            }
            return null;
        }

        bool IDoctorRepository.Login(string userName, string Password)
        {
          var user=  _context.Doctors.FirstOrDefault(x => x.firstName == userName & x.Password == Password);
                if (user != null)
                {           
                    return true;
                }
            return false;
        }

       
        bool IDoctorRepository.updateAppointment(int doctorId,int appointementId,DayOfWeek fromDay,string fromTimeValue, DayOfWeek toDay, string toTimeValue)
        {
            var isFormDayTrue = _context.Appointements.FirstOrDefault(x => x.Availability.ToString() == fromDay.ToString() && x.id==appointementId);
            if (isFormDayTrue != null)
            {
                var isTimeTrue = _context.Times.FirstOrDefault(x => x.appointementId == appointementId &&x.time==fromTimeValue);
                if (isTimeTrue != null)
                {
                    var isFound = _context.Bookings.FirstOrDefault(x => x.DoctorId == doctorId && x.bookingDay == fromDay && x.bookingTime == fromTimeValue);
                    if (isFound == null)
                    {
                        try
                        {
                            var appointement = _context.Appointements.FirstOrDefault(a => a.id == appointementId && a.doctorId == doctorId);

                            if (appointement != null)
                            {
                                appointement.Availability = (DaysOfWeek)toDay;

                                var times = _context.Times.Where(t => t.appointementId == appointementId);
                                foreach (var time in times)
                                {
                                    time.time = toTimeValue;
                                }
                                _context.SaveChanges();
                                return true;
                            }

                            return false;
                        }
                        catch (Exception ex)
                        {
                            // Handle exceptions appropriately (log or throw)
                            Console.WriteLine($"Error updating appointment: {ex.Message}");
                            return false;
                        }
                    }
                    else { return false; }
            }
                else { return false; }
            }
            else { return false; }
        }
    }
}
