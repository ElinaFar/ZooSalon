using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZooSalon2.Models
{
    public class ServiceType
    {
        public int Id { get; set; }
        [Required] //долнжо быть обязательно написано
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
