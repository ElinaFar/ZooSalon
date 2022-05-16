using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZooSalon2.Models
{
    public class ServiceShoppingCart
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        [ForeignKey("AnimalId")]
        public virtual Animal Animal { get; set; }
        public int ServiceTypeId { get; set; }
        [ForeignKey("ServiceTypeId")]
        public virtual ServiceType ServiceType { get; set; }
    }
}
