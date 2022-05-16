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
using ZooSalon2.Utility;

namespace ZooSalon2.Pages.ServiceTypes
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext db; //поле для чтения

        public IndexModel(ApplicationDbContext db)
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
