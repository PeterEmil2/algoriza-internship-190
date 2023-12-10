using DomainLayer.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public enum DaysOfWeek
    {
        [EnumMember(Value = "Saturday")]
        Saturday,
        [EnumMember(Value = "Sunday")]
        Sunday,
        [EnumMember(Value = "Monday")]
        Monday,
        [EnumMember(Value = "Tuesday")]
        Tuesday,
        [EnumMember(Value = "Wednesday")]
        Wednesday,
        [EnumMember(Value = "Thursday")]
        Thursday,
        [EnumMember(Value = "Friday")]
        Friday
    }
    public class Appointement
    {
        public int id { get; set; }

        [ForeignKey("Doctor")]
        public int doctorId { get; set; }
        public Doctor doctor { get; set; }
        public DaysOfWeek Availability { get; set; }
        public List<Time> times { get; set; }


    }
}
