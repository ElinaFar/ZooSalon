using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooSalon2.Data;
using ZooSalon2.Models;
using Microsoft.AspNetCore.Authorization;
using ZooSalon2.Utility;

namespace ZooSalon2.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext db;
        public IndexModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<ApplicationUser> ApplicationUsersList { get; set; }
        public async Task<IActionResult> OnGet(string searchLastName, string searchFirstName, string searchPhone)
        {           
            if (searchLastName == null)
            {
                searchLastName = "";
            }
            if (searchFirstName == null)
            {
                searchFirstName = "";
            }
            if (searchPhone == null)
            {
                searchPhone = "";
            }

            ApplicationUsersList = await db.ApplicationUsers
                .Where(
                m => m.LasttName.ToLower().Contains(searchLastName.ToLower()) &&
                m.FirstName.ToLower().Contains(searchFirstName.ToLower()) &&
                m.PhoneNumber.ToLower().Contains(searchPhone.ToLower())
                )
                .ToListAsync();

            return Page();
        }
    }
}
