using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModel
{
    public class PatientBookingsViewModel
    {
        public string Image { get; set; }
        public string doctorName{ get; set; }
        public string Specialize { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public Double Price { get; set; }
        public string discountCode { get; set; }
        public Double finalPrice { get; set; }
        public string bookingStatus { get; set; }

    }
}
