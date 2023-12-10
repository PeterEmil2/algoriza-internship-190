using DomainLayer.Models;
using DomainLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.RepositoryFolder;

namespace Veseeta.Controllers
{
    public class PatientController : Controller
    {


        private readonly IPatientRepository _repo;

        public PatientController(IPatientRepository repo)
        {

            _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost(nameof(LoginAsync))]
        public async Task<IActionResult> LoginAsync(string Email, string Password)
        {

            bool loginResult = await _repo.Login(Email, Password);
            if (loginResult)
            {
                // Login succeeded
                return Ok("Login successful");
            }
            else
            {
                // Login failed
                return BadRequest("Login failed");
            }

        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromForm] PatientViewModel p)
        {


            if (await _repo.Register(p))
            {
                return Ok("Registered Successfully...!");
            }
            else
            {
                return BadRequest();

            }

        }

        [HttpGet(nameof(Cancel))]
        public async Task<IActionResult> Cancel(int id)
        {    
            bool isTrue = await _repo.cancelBooking(id);
            if (isTrue)
            {
                return Ok("Canceled Successfully...!");
            }
            else
            {
                return BadRequest();

            }

        }

        [HttpGet(nameof(Booking))]
        public async  Task<IActionResult> Booking(int timeId, string? discountCodeCoupon)
        {

            bool isTrue = await _repo.Booking(timeId, discountCodeCoupon, HttpContext.User);
            if (isTrue)
            {
                return Ok("Booking Successfully...!");
            }
            else
            {
                return BadRequest("Can't Book");

            }

        }
        [HttpGet(nameof(getAllBooking))]
        public async Task<IActionResult> getAllBooking()
        {

           var Bookings =  _repo.getAllBooking(HttpContext.User);
            if (Bookings != null)
            {
                return Ok(Bookings);
            }
            else
            {
                return BadRequest("There is No Bookings !!");

            }

        }
        [HttpGet(nameof(getAlldoctorsToPatient))]
        public async Task<IActionResult> getAlldoctorsToPatient(int Page, int pageSize, genderType Search)
        {

            var doctors = _repo.getAllDoctors(Page,pageSize,Search);
            if (doctors != null)
            {
                return Ok(doctors);
            }
            else
            {
                return BadRequest("There is No Doctors !!");

            }

        }



    }
}
