using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class userCouponDiscount
    {

        public int id { get; set; }
        [ForeignKey("applicationUser")]
        public string applicationUserId { get; set; }
        public applicationUser Patient { get; set; }

        [ForeignKey("Coupon")]
        public string couponId { get; set; }
        public Coupon Coupons { get; set; }




}
}
