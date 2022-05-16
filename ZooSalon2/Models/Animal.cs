using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZooSalon2.Models
{
    public class Animal
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Kind { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public double Weight { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}