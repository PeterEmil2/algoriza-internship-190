using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModel
{
    public class DoctorViewModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Email { get; set; }
        public genderType Gender { get; set; }
        public string dateOfBirth { get; set; }
        public IFormFile Image { get; set; }
        public string Password { get; set; }

        public string phoneNumber { get; set; }

        public string specializeName { get; set; }

    }
}
