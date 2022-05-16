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

namespace ZooSalon2.Pages.Users
{
    
    [Authorize(Roles = SD.AdminEndUser)]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public DeleteModel(ApplicationDbContext db)
        {
            this.db = db;

        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            ApplicationUser = await db.ApplicationUsers.FindAsync(id);
            return Page();


        }

        public async Task<IActionResult> OnPost()
        {

            var user = await db.ApplicationUsers.FindAsync(ApplicationUser.Id);

            db.ApplicationUsers.Remove(user);


            await db.SaveChangesAsync();
            return RedirectToPage("Index");

        }
    }
}
