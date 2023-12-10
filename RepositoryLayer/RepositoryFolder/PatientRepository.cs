using DomainLayer.Models;
using DomainLayer.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
namespace RepositoryLayer.RepositoryFolder
{



    public class PatientRepository : IPatientRepository
    {

        appcontext _context;
        UserManager<applicationUser> _usermanger { get; set; }
        SignInManager<applicationUser> _SignInManager { get; set; }


        public PatientRepository(UserManager<applicationUser> UserManager, SignInManager<applicationUser> signInManager, appcontext context)
        {
            _usermanger = UserManager;
            _SignInManager = signInManager;
            _context = context;

        }
        async Task<bool> IPatientRepository.Login(string email, string Password)
        {
            try
            {
                IdentityUser user = await _usermanger.FindByEmailAsync(email.ToUpper());

                if (user != null)
                {

                    var hasher = new PasswordHasher<IdentityUser>();
                    var result = hasher.VerifyHashedPassword(user,user.PasswordHash , Password);


                    if (result == PasswordVerificationResult.Success)
                    {




                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"An error occurred during login: {ex.Message}");
                return false;
            }
        }

        async Task<bool> IPatientRepository.Register([FromForm]PatientViewModel p)
        {

            try
            {
                applicationUser user = new applicationUser();
                user.UserName = p.firstName;
                user.PhoneNumber = p.Phone;
                user.Email = p.Email;
                user.NormalizedEmail = p.Email.ToUpper();
                using (var streamReader = new StreamReader(p.image.OpenReadStream()))
                {
                    string fileContent = await streamReader.ReadToEndAsync();
                    user.Image =fileContent;
                }
                
                IdentityResult res = await _usermanger.CreateAsync(user, p.password);

                if (res.Succeeded)
                {
                 

                    await _usermanger.AddToRoleAsync(user, "Patient");

                    await _SignInManager.SignInAsync(user, false);

                    return true;
                }
                else
                {
                    // Log or handle errors
                    foreach (var error in res.Errors)
                    {
                        // Log or handle each error
                        Console.WriteLine($"Error: {error.Code}, Description: {error.Description}");
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log or handle unexpected exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

       Task<bool> IPatientRepository.cancelBooking(int bookingId)
        {

            var b = _context.Bookings.FirstOrDefault(x => x.id == bookingId);

            if (b!= null)
            {
                b.bookingStatus = "Cancelled";
                _context.Bookings.Update(b);

                _context.SaveChanges();

                return Task.FromResult(true);

                
            }
            return Task.FromResult(false);
        }

        Task<bool> IPatientRepository.Booking(int timeId, string? discountCodeCoupon, ClaimsPrincipal user)
        {
            var time = _context.Times.SingleOrDefault(x => x.id == timeId);

            if (time != null)
            {
                var appointement = _context.Appointements.SingleOrDefault(x => x.id == time.appointementId);

                if (appointement != null)
                {
                    Booking b = new Booking
                    {
                        DoctorId = appointement.doctorId,
                        bookingStatus = "Pending",
                        bookingDay = (DayOfWeek)appointement.Availability,
                        bookingTime = time.time,
                        discountCode = discountCodeCoupon
                    };

                    var userId = _usermanger.GetUserId(user);

                    if (userId != null)
                    {
                        b.applicationUserId = userId;
                    }
                    else
                    {
                        return Task.FromResult(false);
                    }

                    var couponIsFound = _context.Coupons.SingleOrDefault(x => x.discountCode == discountCodeCoupon);
                    var doctor = _context.Doctors.SingleOrDefault(x => x.id == appointement.doctorId);

                    if (couponIsFound != null && doctor != null)
                    {
                        double totalPrice = 0;

                        if (couponIsFound.valueType == discountType.Percentage)
                        {
                            totalPrice = doctor.Price - ((couponIsFound.value / 100) * doctor.Price);
                        }
                        else
                        {
                            totalPrice = doctor.Price - couponIsFound.value;
                        }

                        b.finalPrice = totalPrice;

                        _context.Bookings.Add(b);
                        _context.SaveChanges();

                        return Task.FromResult(true);
                    }

                    return Task.FromResult(false);
                }

                return Task.FromResult(false);
            }

            return Task.FromResult(false);
        }

        public IEnumerable<PatientBookingsViewModel> getAllBooking(ClaimsPrincipal user)
        {
            var userId = _usermanger.GetUserId(user);

            var bookings = _context.Bookings
                .Where(x => x.applicationUserId == userId)
                .Join(
                    _context.Doctors,
                    booking => booking.DoctorId,
                    doctor => doctor.id,
                    (booking, doctor) => new PatientBookingsViewModel
                    {
                        Image = doctor.Image,
                        doctorName = doctor.firstName + " " + doctor.lastName,
                        Specialize = doctor.Specialization.name, // Assuming Specialization has a 'Name' property
                        Day = booking.bookingDay.ToString(),
                        Time = booking.bookingTime,
                        Price = doctor.Price,
                        discountCode = booking.discountCode,
                        finalPrice = booking.finalPrice,
                        bookingStatus = booking.bookingStatus
                    })
                .ToList();

            return bookings;
        }


        public IEnumerable<DoctorInformationViewModel> getAllDoctors(int Page, int pageSize, genderType Search)
        {
            var query = _context.Doctors
                .Where(d => string.IsNullOrEmpty(Search.ToString()) || d.Gender== Search)
                .Skip((Page - 1) * pageSize)
                .Take(pageSize)
                .Join(
                    _context.Appointements,
                    doctor => doctor.id,
                    appointment => appointment.doctorId,
                    (doctor, appointment) => new { Doctor = doctor, Appointment = appointment }
                )
                .Join(
                    _context.Times,
                    x => x.Appointment.id,
                    time => time.appointementId,
                    (x, time) => new DoctorInformationViewModel
                    {
                        Image = x.Doctor.Image,
                        doctorName = $"{x.Doctor.firstName} {x.Doctor.lastName}",
                        Specialize = x.Doctor.Specialization.name,
                        Day = x.Appointment.Availability.ToString(),
                        Time = time.time.ToString(),
                        Price = x.Doctor.Price,
                        Gender = x.Doctor.Gender
                    }
                )
                .ToList();

            return query;
        }


    }

}
