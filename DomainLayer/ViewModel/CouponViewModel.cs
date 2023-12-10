using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModel
{
    public class CouponViewModel
    {
        public string discountCode { get; set; }

        public double value { get; set; }

        public discountType valueType { get; set; }

        public int numOfCompletedRequests { get; set; }
    }
}
