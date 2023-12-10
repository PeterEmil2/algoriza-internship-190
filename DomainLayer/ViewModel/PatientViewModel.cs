using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModel
{
    public class PatientViewModel
    {
        public IFormFile? image { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }
        public genderType Gender { get; set; }
        public string dateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string confirmpassword { get; set; }


    }
}
