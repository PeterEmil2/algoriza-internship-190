using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class AdminRepository:IAdminRepository
    {
        private readonly appcontext _applicationDbContext;
        private readonly UserManager<applicationUser> _userManager;


        public AdminRepository(appcontext applicationDbContext, UserManager<applicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;

        }

        int IAdminRepository.numOfDoctors()
        {
            return _applicationDbContext.Doctors.ToList().Count;
        }

        int IAdminRepository.numOfPatients()
        {
            return _applicationDbContext.Doctors.ToList().Count;
        }
        async Task<Dictionary<string, int>> IAdminRepository.numOfRequests()
        {
            Dictionary<string, int> Requests = new Dictionary<string, int>();
            int numOfRequests = _applicationDbContext.Bookings.ToList().Count;

            var numOfPendingRequests = await _applicationDbContext.Bookings
           .Where(x => x.bookingStatus == "Pending")
           .CountAsync();

            var numOfCompletedRequests = await _applicationDbContext.Bookings
                .Where(x => x.bookingStatus == "Completed")
                .CountAsync();

            var numOfCancelledRequests = await _applicationDbContext.Bookings
                .Where(x => x.bookingStatus == "Cancelled")
                .CountAsync();
            if(numOfPendingRequests!=0)
            { Requests.Add("PendingRequests", numOfPendingRequests); }
            if (numOfCompletedRequests != 0)
            { Requests.Add("CompletedRequests", numOfCompletedRequests); }
            if (numOfCancelledRequests != 0)
            { Requests.Add("CancelledRequests ", numOfCancelledRequests); }
            if (numOfRequests != 0)
            { Requests.Add("Requests ", numOfRequests); }

            return Requests;
        }

        IEnumerable<topFiveSpecializatonViewModel> IAdminRepository.topFiveSpecialization()
        {
            var topSpecializations = _applicationDbContext.Bookings
                .GroupBy(b => b.DoctorId)
                .OrderByDescending(group => group.Count())
                .Take(5)
                .Select(group => new topFiveSpecializatonViewModel
                {
                    specializationName = _applicationDbContext.Doctors
                        .Where(doctor => doctor.id == group.Key)
                        .Select(doctor => _applicationDbContext.Specializations
                            .Where(s => s.id == doctor.specializeId)
                            .Select(s => s.name)
                            .FirstOrDefault()
                        )
                        .FirstOrDefault(),
                    numOfRequests = group.Count()
                })
                .ToList();

            return topSpecializations;
        }

            IEnumerable<topTenDoctorViewModel> IAdminRepository.topTenDoctors()
        {
            var topDoctors = _applicationDbContext.Bookings
                      .GroupBy(b => b.DoctorId)
                      .OrderByDescending(group => group.Count())
                      .Take(10)
                      .Select(group => new topTenDoctorViewModel
                      {
                          FullName = group.First().Doctor.firstName + group.First().Doctor.lastName,
                          SpecializationName = _applicationDbContext.Specializations
                          .Where(s => s.id == group.First().Doctor.specializeId)
                          .Select(s => s.name)
                          .FirstOrDefault(),
                          numOfRequests = group.Count(),
                          Image = group.First().Doctor.Image
                      })
                      .ToList();

            return topDoctors;

        }

        IEnumerable<Doctor> IAdminRepository.GetAllDoctors(int? Page, int pageSize, genderType Search)
        {

         return _applicationDbContext.Doctors.Where(x=>x.Gender == Search || Search.ToString() ==null).ToList().ToPagedList(Page ?? 1,pageSize);



        }

        Doctor IAdminRepository.GetDoctorById(int id)
        {

            var user = _applicationDbContext.Doctors.FirstOrDefault(e => e.id == id);
            if (user != null)
                return user;
            else
                return null;
        }

        bool IAdminRepository.InsertDoctor(Doctor entity)
        {




            try
            {
                _applicationDbContext.Doctors.Add(entity);
                _applicationDbContext.SaveChanges();
                return true; 
            }
            catch (Exception)
            {
                return false; 
            }
        }

        bool IAdminRepository.UpdateDoctor(Doctor entity)
        {
            try
            {
                var existingDoctor = _applicationDbContext.Doctors.Find(entity.id);

                if (existingDoctor != null)
                {
                    // Update properties of the existing doctor with the new values
                    existingDoctor.firstName = entity.firstName;
                    existingDoctor.lastName = entity.lastName;
                    existingDoctor.dateOfBirth = entity.dateOfBirth;
                    existingDoctor.Email = entity.Email;
                    existingDoctor.phoneNumber = entity.phoneNumber;
                    existingDoctor.Gender = entity.Gender;
                    existingDoctor.Price = entity.Price;



                    _applicationDbContext.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false; 
            }

        }

   







        bool IAdminRepository.DeleteDoctor(Doctor entity)
        {
            try
            {
                var doctorToDelete = _applicationDbContext.Doctors.Find(entity.id);

                if (doctorToDelete != null)
                {
                    _applicationDbContext.Doctors.Remove(doctorToDelete);
                    _applicationDbContext.SaveChanges();
                    return true;
                }
                return false; 
            }
            catch (Exception)
            {
                return false; 
            }
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<applicationUser>> GetAllPatients(int? page, int pageSize, string search)
        {
            var users = await _userManager.Users
                .Where(x => x.UserName == search || search == null)
                .ToListAsync();
            var pagedList = users.ToList().ToPagedList(page ?? 1, pageSize);


            return pagedList;
        }
        async Task<applicationUser>  IAdminRepository.GetPatientById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
                return user;
            else
                return null;
        }

        bool IAdminRepository.InsertCoupon(Coupon c)
        {
            if (c != null)
            {
                _applicationDbContext.Coupons.Add(c);
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;

        }

        bool IAdminRepository.UpdateCoupon(Coupon c)
        {
            var coupon = _applicationDbContext.Coupons.FirstOrDefault(x => x.id == c.id);
            if (coupon != null)
            {
                coupon.discountCode = c.discountCode;
                coupon.value = c.value;
                coupon.numOfCompletedRequests = c.numOfCompletedRequests;
                coupon.valueType = c.valueType;
                _applicationDbContext.Update(coupon);
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        bool IAdminRepository.DeleteCoupon(int id)
        {
            var c = _applicationDbContext.Coupons.FirstOrDefault(x => x.id == id);
            if (c != null)
            {

                _applicationDbContext.Coupons.Remove(c);
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
       
        }

        bool IAdminRepository.DeactivateCoupon(int id)
        {
            var c = _applicationDbContext.Coupons.FirstOrDefault(x => x.id == id);
            if (c != null)
            {
                c.isActive = false;
                _applicationDbContext.SaveChanges();
                return true;
            
            }
            return false;
            }

        int IAdminRepository.getSpetializeIdByName(string spetialize)
        {
           var s = _applicationDbContext.Specializations.FirstOrDefault(x => x.name == spetialize);
            if (s != null)
            {
                return s.id;
            }
            return 0;
        }

       
    }
}
