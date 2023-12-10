using DomainLayer.Models;
using DomainLayer.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.RepositoryFolder;

namespace Veseeta.Controllers
{
    public class DoctorController : Controller
    {


        private readonly IDoctorRepository _repo;

        public DoctorController(IDoctorRepository repo)
        {

            _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost(nameof(Login))]
        public IActionResult Login(LoginViewModel loginuser)
        {

            if ( _repo.Login(loginuser.username, loginuser.password)) {


                return Ok("Login Successfully");
            
            
            
            }
            return BadRequest("Failed to login");
        }

        [HttpPost(nameof(GetAll))]
        public IActionResult GetAll(string SearchBy, int? Page, int pageSize, string Search)
        {
            var doctorPatients = _repo.GetAllDoctorPatients(SearchBy, Page, pageSize, Search);
            if (doctorPatients != null)
            {
                return Ok(doctorPatients);
            }
            return BadRequest("there is no bookings");
        }

        [HttpGet(nameof(ConfirmCkeckUp))]
        public IActionResult ConfirmCkeckUp(int bookingId)
        {
             if (_repo.confirmCheckUp(bookingId))
            {
                return Ok("Booking is confirmed...!");
            }
            return BadRequest("there is no booking with this id");
        }


        [HttpPost(nameof(addAppointment))]
        public IActionResult addAppointment(int doctorId, int Price, DaysOfWeek Availability,[FromBody] List<timeViewModel> times )
        {
         
            if (_repo.addAppointment(doctorId,Price,(DayOfWeek)Availability,times))
            {
                return Ok("Appointment is added....!");
            }
            return BadRequest("there is no appointement added");
        }


        [HttpPost(nameof(updateAppointment))]
        public IActionResult updateAppointment(int doctorId, int appointementId, DayOfWeek FromDay, string fromTimeValue, DayOfWeek toDay, string toTimeValue)
        {

            if (_repo.updateAppointment(doctorId, appointementId,FromDay,fromTimeValue,toDay,toTimeValue))
            {
                return Ok("Appointment is updated....!");
            }
            return BadRequest("there is no appointement updated");
        }



        [HttpGet(nameof(deleteAppointment))]
        public IActionResult deleteAppointment(int appointementId, int timeId)
        {

            if (_repo.deleteAppointment( appointementId,timeId))
            {
                return Ok("Appointment is deleted....!");
            }
            return BadRequest("there is no appointement deleted");
        }



    }
}
