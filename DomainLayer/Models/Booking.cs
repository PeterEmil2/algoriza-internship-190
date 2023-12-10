using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Booking
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("applicationUser")]
        public string applicationUserId { get; set; }
        public applicationUser Patient { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string bookingStatus { set; get; }
        public DayOfWeek bookingDay{ set; get; }
        public string bookingTime { set; get; }
        public string? discountCode { set; get; }
        public double finalPrice { set; get; }
    }
}
