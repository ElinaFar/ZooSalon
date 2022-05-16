using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ZooSalon2.Models;

namespace ZooSalon2.Models
{
    public class ServiceDetails
    {
        public int Id { get; set; }
        public int ServiceTypeId { get; set; }
        [ForeignKey("ServiceTypeId")]
        public virtual ServiceType ServiceType { get; set; }

        public string ServiceName { get; set; }
        public double ServicePrice { get; set; }
        public int ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }


    }
}
