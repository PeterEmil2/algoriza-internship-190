using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DomainLayer.Models
{
    public enum genderType {
        [EnumMember(Value = "Female")]
        Female,

        [EnumMember(Value = "Male")]
        Male
    }
    public class Doctor
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Email { get; set; }
        public genderType Gender { get; set; }
        public string dateOfBirth { get; set; }
        public string Image { get; set; }

        public string phoneNumber { get; set; }

        public string Password { get; set; }
        public int Price { get; set; }

        [ForeignKey("Specialization")]
        public int specializeId { get; set; }
        public Specialization Specialization { get; set; }
        public ICollection<Booking> Patients { get; set; }
    }
}
