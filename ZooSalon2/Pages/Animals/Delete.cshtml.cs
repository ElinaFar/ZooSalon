using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZooSalon2.Data;
using ZooSalon2.Models;

namespace ZooSalon2.Pages.Animals
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext db;
        public DeleteModel(ApplicationDbContext db)
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
           
                var animalObj = await db.Animals.FindAsync(Animal.Id);

            db.Animals.Remove(animalObj);

                await db.SaveChangesAsync();
                StatusMessage = "Информация о питомце успешно удалена! ";
                return RedirectToPage("Index", new { userId = Animal.UserId });
          

        }
    }
}
