using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZooSalon2.Data;
using ZooSalon2.Models;
using Microsoft.AspNetCore.Authorization;
using ZooSalon2.Utility;

namespace ZooSalon2.Pages.ServiceTypes
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public CreateModel(ApplicationDbContext db)
        {
            this.db = db;

        }
        [BindProperty]
        public ServiceType ServiceType { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await db.ServiceTypes.AddAsync(ServiceType);
                await db.SaveChangesAsync();
                return RedirectToPage("Index");


            }
            return Page();
        }
    }
}
