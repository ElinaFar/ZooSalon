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
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext db;
        public EditModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public Animal Animal { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            Animal = await db.Animals.FindAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var animalObj = await db.Animals.FindAsync(Animal.Id);

                animalObj.Type = Animal.Type;
                animalObj.Kind = Animal.Kind;
                animalObj.Name = Animal.Name;
                animalObj.Birthday = Animal.Birthday;
                animalObj.Weight = Animal.Weight;

                await db.SaveChangesAsync();
                StatusMessage = "Информация о питомце успешно изменена! ";
                return RedirectToPage("Index", new { userId = Animal.UserId });
            }

            return Page();

        }

    }
}
