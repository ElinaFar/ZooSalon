using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZooSalon2.Models
{
    public class Service
    {
        public int Id { get; set; } 
        public double TotalPrice { get; set; }
        public string Comments { get; set; }
        //[DisplayFormat(DataFormatString ="{0:MM dd yyyy}")]
        public DateTime Date { get; set; }
        public int AnimalId { get; set; }
        [ForeignKey("AnimalId")]
        public virtual Animal Animal { get; set; }
    }
}
