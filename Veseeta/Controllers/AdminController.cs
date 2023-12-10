using DomainLayer.Models;
using DomainLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.RepositoryFolder;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace Veseeta.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _repo;

         public AdminController(IAdminRepository repo)
        {
        
        _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet(nameof(getAllDoctors))]
        public IActionResult getAllDoctors(int? Page, int pageSize, genderType Search)
        {
            var res = _repo.GetAllDoctors( Page,  pageSize,  Search);
            if (res == null || !res.Any())
            {
                return BadRequest("There are no records");
            }
            else
            {
                return Ok(res);
            }



        }


        [HttpGet(nameof(getAllPatients))]
        public async Task<IActionResult> getAllPatients(int? Page, int pageSize, string Search)
        {
            var res = await _repo.GetAllPatients(Page, pageSize, Search);
            if (res == null)
            {
                return BadRequest("There are no records");
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpGet(nameof(numOfPatients))]
        public IActionResult numOfPatients()
        {
            var res = _repo.numOfPatients();
            if (res == 0)
            {
                return BadRequest("There are no records");
            }
            else
            {
                return Ok(res);
            }


        }

        [HttpGet(nameof(numOfDoctors))]
        public IActionResult numOfDoctors()
        {
            var res = _repo.numOfDoctors();
            if (res == 0)
            {
                return BadRequest("There are no records");
            }
            else
            {
                return Ok(res);
            }
        }





        [HttpGet(nameof(getDoctorById))]
        public IActionResult getDoctorById(int id)
        {
            var res = _repo.GetDoctorById(id);
            if (res == null)
            {
                return BadRequest("There are no records");
            }
            else
            {
                return Ok(res);
            }
        }

   
        [HttpGet(nameof(getPatientById))]
        public IActionResult getPatientById(string id)
        {
            var res = _repo.GetPatientById(id);
            if (res == null)
            {
                return BadRequest("There are no records");
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost(nameof(addCoupon))]
        public IActionResult addCoupon(CouponViewModel c)
        {

            Coupon coupon = new Coupon();
            coupon.value = c.value;
            coupon.discountCode = c.discountCode;
            coupon.isActive = true;
            coupon.numOfCompletedRequests = c.numOfCompletedRequests;
            coupon.valueType = c.valueType;

            var res = _repo.InsertCoupon(coupon);
            if (res == null)
            {
                return BadRequest("There are no records");
            }
            else
            {
                return Ok("Coupon is added...!");
            }
        }
        [HttpPost(nameof(updateCoupon))]
        public IActionResult updateCoupon(Coupon c)
        {
            var res = _repo.UpdateCoupon(c);
            if (res == null)
            {
                return BadRequest("There are no records");
            }
            else
            {
                return Ok("Coupon is updated...!");
            }
        }

        [HttpPost(nameof(deleteCoupon))]
        public IActionResult deleteCoupon(int id)
        {
            var res = _repo.DeleteCoupon(id);
            if (res == null)
            {
                return BadRequest("There are no records");
            }
            else
            {
                return Ok("Coupon is deleted...!");
            }
        }
        [HttpPost(nameof(DectiveCoupon))]
        public IActionResult DectiveCoupon(int id)
        {
            var res = _repo.DeactivateCoupon(id);
            if (res == null)
            {
                return BadRequest("Coupon still active...!");
            }
            else
            {
                return Ok("Coupon is deactivated");
            }
        }




        [HttpPost(nameof(InsertDoctor))]
        public async Task<IActionResult> InsertDoctor([FromForm]DoctorViewModel d)
        {
            int res = _repo.getSpetializeIdByName(d.specializeName);

            Doctor doctor = new Doctor();
            using (var streamReader = new StreamReader(d.Image.OpenReadStream()))
            {
                string fileContent = await streamReader.ReadToEndAsync();
                doctor.Image = fileContent;
            }
              
            doctor.firstName = d.firstName;
            doctor.lastName = d.lastName;
            doctor.Email = d.Email;
            doctor.phoneNumber = d.phoneNumber;
            doctor.dateOfBirth = d.dateOfBirth;
            doctor.specializeId = res;
            doctor.Gender = d.Gender;
            doctor.Password = d.Password;
          
            if (_repo.InsertDoctor(doctor))
            {
                return Ok("doctor is added");
            }
            else
            {
                return BadRequest("fail to add doctor");
            }

         



        }
        [HttpGet(nameof(numOfRequests))]
        public async Task<IActionResult> numOfRequests()
        {
            var res = await _repo.numOfRequests().ConfigureAwait(false);

            if (res == null)
            {
                return BadRequest("There are no requests...!");
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpGet(nameof(topTenDoctors))]
        public IActionResult topTenDoctors()
        {
            var res = _repo.topTenDoctors();
            if (res == null)
            {
                return BadRequest("Failed to get data..");
            }
            else
            {
                return Ok(res);
            }
        }
        [HttpGet(nameof(topFiveSpecialization))]
        public IActionResult topFiveSpecialization()
        {
            var res = _repo.topFiveSpecialization();
            if (res == null)
            {
                return BadRequest("Failed to get data..");
            }
            else
            {
                return Ok(res);
            }
        }


    }
}
