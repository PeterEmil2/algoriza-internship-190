using Microsoft.AspNetCore.Identity;

namespace DomainLayer.Models
{
    public class applicationUser:IdentityUser
    {
        public ICollection<Booking> Doctors { get; set; }
        public string? Image { get; set; }

    }
}
