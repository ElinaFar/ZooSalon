using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooSalon2.Data;
using ZooSalon2.Models.ViewModels;

namespace ZooSalon2.Pages.Animals
{
    [Authorize ]
    public class IndexModel : PageModel
    {
        public readonly ApplicationDbContext db;

        public IndexModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        public CustomerAndAnimalViewModel CustomerAndAnimalVM { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGet(string userId = null)
        {
            if (userId == null)
            {
                var claimIdenity = (ClaimsIdentity)User.Identity;
                var claim = claimIdenity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;
            }

            CustomerAndAnimalVM = new CustomerAndAnimalViewModel
            {
                UserObj = db.ApplicationUsers.Find(userId),
                Animals = await db.Animals.Where(m=>m.UserId==userId).ToListAsync()             
            };

            return Page();


        }


    }
}
