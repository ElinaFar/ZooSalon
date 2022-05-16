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
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public EditModel(ApplicationDbContext db)
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
            if (ModelState.IsValid)
            {
                var user = await db.ApplicationUsers.FindAsync(ApplicationUser.Id);
                user.LasttName = ApplicationUser.LasttName;
                user.FirstName = ApplicationUser.FirstName;
                user.PhoneNumber = ApplicationUser.PhoneNumber;
                user.Birthday = ApplicationUser.Birthday;
                user.Email = ApplicationUser.Email;


                await db.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            return Page();
        }

    }
}
