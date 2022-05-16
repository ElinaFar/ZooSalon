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
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public DetailsModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Service Service { get; set; }
        public List<ServiceDetails> ServicesDetails { get; set; }

        public void OnGet(int serviceId)
        {
            Service = db.Services.Include(m => m.Animal).Include(m => m.Animal.ApplicationUser)
                .Where(m => m.Id == serviceId).FirstOrDefault();

            ServicesDetails = db.ServiceDetails.Where(m => m.ServiceId == serviceId).ToList();
        }
    }
}
