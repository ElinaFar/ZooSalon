using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZooSalon2.Data;
using ZooSalon2.Models;

namespace ZooSalon2.Pages.Animals
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext db;
        public CreateModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public Animal Animal { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult OnGet(string userId=null)
        {
            if (userId == null)
            {  
                var claimIdentity = (ClaimsIdentity)User.Identity;            
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;
            }
            Animal = new Animal();
            Animal.UserId = userId;

            return Page();

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {             
                await db.Animals.AddAsync(Animal);            
                await db.SaveChangesAsync();
                StatusMessage = "Питомец успешно добавлен!";
                return RedirectToPage("Index", new { userId = Animal.UserId });
            }

            return Page();

        }


    }


}
