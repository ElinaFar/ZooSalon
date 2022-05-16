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
using ZooSalon2.Models.ViewModels;
using ZooSalon2.Utility;

namespace ZooSalon2.Pages.Record
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
        public AnimalServiceViewModel AnimalServiceVM { get; set; }
        public IActionResult OnGet(int animalId)
        {
            AnimalServiceVM = new AnimalServiceViewModel()
            {
                Animal = db.Animals.Include(m => m.ApplicationUser).Where(m => m.Id == animalId).FirstOrDefault(),
                Record = new Record()
            };
            List<string> ListServiceTypesInShoppingCart = db.ServiceShoppingCarts
                .Include(m => m.ServiceType).Where(m => m.AnimalId == animalId)
                .Select(m => m.ServiceType.Name).ToList();

            IQueryable<ServiceType> ListServiceType = from s in db.ServiceTypes
                                                      where !(ListServiceTypesInShoppingCart.Contains(s.Name))
                                                      select s;
            AnimalServiceVM.ServiceTypes = ListServiceType.ToList();

            AnimalServiceVM.ServiceShoppingCarts = db.ServiceShoppingCarts.Include(m => m.ServiceType)
                .Where(m => m.AnimalId == animalId).ToList();

            foreach (var item in AnimalServiceVM.ServiceShoppingCarts)
            {
                AnimalServiceVM.Service.TotalPrice += item.ServiceType.Price;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCart()
        {
            ServiceShoppingCart serviceShoppingCart = new ServiceShoppingCart()
            {
                AnimalId = AnimalServiceVM.Animal.Id,
                ServiceTypeId = AnimalServiceVM.ServiceDetails.ServiceTypeId

            };

            await db.ServiceShoppingCarts.AddAsync(serviceShoppingCart);
            await db.SaveChangesAsync();
            return RedirectToPage("Create", new { animalId = AnimalServiceVM.Animal.Id });
        }

        public async Task<IActionResult> OnPostRemoveFromCart(int serviceCartId)
        {
            ServiceShoppingCart serviceShoppingCart = await db.ServiceShoppingCarts.FindAsync(serviceCartId);
            db.ServiceShoppingCarts.Remove(serviceShoppingCart);
            await db.SaveChangesAsync();
            return RedirectToPage("Create", new { animalId = AnimalServiceVM.Animal.Id });
        }

        public async Task<IActionResult> OnPostComplete()
        {
            AnimalServiceVM.Service.Date = DateTime.Now;
            AnimalServiceVM.Service.AnimalId = AnimalServiceVM.Animal.Id;

            AnimalServiceVM.ServiceShoppingCarts = db.ServiceShoppingCarts.Include(m => m.ServiceType)
                .Where(m => m.AnimalId == AnimalServiceVM.Animal.Id).ToList();

            foreach (var item in AnimalServiceVM.ServiceShoppingCarts)
            {
                AnimalServiceVM.Service.TotalPrice += item.ServiceType.Price;

            }
            await db.Services.AddAsync(AnimalServiceVM.Service);
            await db.SaveChangesAsync();

            foreach (var item in AnimalServiceVM.ServiceShoppingCarts)
            {
                ServiceDetails serviceDetails = new ServiceDetails()
                {
                    ServiceId = AnimalServiceVM.Service.Id,
                    ServiceName = item.ServiceType.Name,
                    ServicePrice = item.ServiceType.Price,
                    ServiceTypeId = item.ServiceTypeId
                };
                db.ServiceDetails.Add(serviceDetails);
            }


            db.ServiceShoppingCarts.RemoveRange(AnimalServiceVM.ServiceShoppingCarts);

            await db.SaveChangesAsync();

            return RedirectToPage("../Animals/Index", new { userId = AnimalServiceVM.Animal.UserId });
        }

    }
}
