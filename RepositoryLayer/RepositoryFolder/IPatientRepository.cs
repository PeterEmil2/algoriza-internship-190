using DomainLayer.Models;
using DomainLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RepositoryLayer.RepositoryFolder
{
    public interface IPatientRepository
    {
     
    public Task<bool> Register(PatientViewModel p);

        public Task<bool> Login(string email, string Password);
        //public  getAllDoctors(int? Page, int pageSize, string Search);
        public Task<bool> cancelBooking(int p);
        public Task<bool> Booking(int timeId,string? discountCodeCoupon, ClaimsPrincipal user );
        public IEnumerable<PatientBookingsViewModel> getAllBooking(ClaimsPrincipal user);
        IEnumerable<DoctorInformationViewModel> getAllDoctors(int Page, int pageSize, genderType Search);


    }
}
