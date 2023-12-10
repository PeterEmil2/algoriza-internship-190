using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Time
    {
        public int id { get; set; }


        [ForeignKey("Appointement")]
        public int? appointementId { get; set; }
        public Appointement appointement { get; set; }


    
        public string time { get; set; }

    }
}
