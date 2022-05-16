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
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public DeleteModel(ZooSalon2.Data.ApplicationDbContext db)
        {
            this.db = db;

        }

        [BindProperty]
        public ServiceType ServiceType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceType = await db.ServiceTypes.FindAsync(id);

            if (ServiceType == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            db.ServiceTypes.Remove(ServiceType);

            await db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        //private bool ServiceTypeExists(int id)
        //{
        //    return db.ServiceTypes.Any(e => e.Id == id);
        //}
    }
        
}
