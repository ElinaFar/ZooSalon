using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooSalon2.Models.ViewModels
{
    public class CustomerAndAnimalViewModel
    {
        public ApplicationUser UserObj { get; set; }
        public IEnumerable<Animal> Animals { get; set; }
    }
}
