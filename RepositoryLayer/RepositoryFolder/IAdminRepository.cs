using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.RepositoryFolder
{
    public interface IAdminRepository
    {
        int numOfDoctors();
        int numOfPatients();
        Task<Dictionary<string, int>> numOfRequests();
        IEnumerable<topFiveSpecializatonViewModel> topFiveSpecialization();
        IEnumerable<topTenDoctorViewModel> topTenDoctors();


        IEnumerable<Doctor> GetAllDoctors(int? Page, int pageSize, genderType Search);
        Doctor GetDoctorById(int id);
        bool InsertDoctor(Doctor entitiy);
        bool UpdateDoctor(Doctor entitiy);
        bool DeleteDoctor(Doctor entitiy);


        int getSpetializeIdByName(string spetialize);


        Task<IEnumerable<applicationUser>> GetAllPatients(int? Page, int pageSize, string Search);
        Task<applicationUser> GetPatientById(string id);


        bool InsertCoupon(Coupon c);
        bool UpdateCoupon(Coupon c);
        bool DeleteCoupon(int id);
        bool DeactivateCoupon(int id);

    }
}
