using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooSalon2.Data;
using ZooSalon2.Models;

namespace ZooSalon2.Pages.Services
{
    [Authorize]
    public class HistoryModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public HistoryModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Service> Services { get; set; }
        public Animal Animal { get; set; }

        public IActionResult OnGet(int animalId)
        {
            Services = db.Services.Where(m => m.AnimalId == animalId).ToList();
            Animal = db.Animals.Include(m => m.ApplicationUser).Where(m => m.Id == animalId).FirstOrDefault();

            return Page();


        }
    }
}
