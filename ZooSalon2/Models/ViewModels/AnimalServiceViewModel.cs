using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooSalon2.Models.ViewModels
{
    public class AnimalServiceViewModel
    {
        public Animal Animal { get; set; }
        public Service Service { get; set; }
        public ServiceDetails ServiceDetails { get; set; }
        public List<ServiceType> ServiceTypes { get; set; }
        public List<ServiceShoppingCart> ServiceShoppingCarts { get; set; }


        public Record Records { get; set; }
        public List<RecordCart> RecordCarts { get; set; }
    }
}
