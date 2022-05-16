using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooSalon2.Data;
using ZooSalon2.Models;

namespace ZooSalon2.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ApplicationDbContext db; //поле для чтения

        public PrivacyModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<ServiceType> ServiceTypes { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ServiceTypes = await db.ServiceTypes.ToListAsync();
            return Page();

        }
    }
}
