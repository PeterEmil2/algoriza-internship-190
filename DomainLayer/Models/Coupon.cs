using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{


    public enum discountType{
        [EnumMember(Value = "Percentage")]
        Percentage,

        [EnumMember(Value = "Value")]
        Value
    }
    public class Coupon
    {
        public int id { get; set; }
        public string discountCode { get; set; }
        public double value { get; set; }
        public discountType valueType { get; set; }
        public int numOfCompletedRequests { get; set; }
        public bool isActive { get; set; }

    }
}
