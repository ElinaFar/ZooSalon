using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using ZooSalon2.Models;
using ZooSalon2.Utility;

namespace ZooSalon2.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        private readonly RoleManager<IdentityRole> _roleManager;
        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,

            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;


        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Почта")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Подтвердите пароль")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            ///
            [Required]
            [Display(Name = "Имя")]
            public string FirstName { get; set; }
            [Display(Name = "Фамилия")]
            public string LasttName { get; set; }
            [Display(Name = "Дата рождения")]
            public DateTime Birthday { get; set; }

            [Required]
            [Display(Name = "Номер телефона")]
            public string PhoneNumber { get; set; }

            public bool IsAdmin { set; get; }

            ///
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LasttName = Input.LasttName,
                    Birthday = Input.Birthday,
                    PhoneNumber = Input.PhoneNumber

                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {

                    if (!await _roleManager.RoleExistsAsync(SD.AdminEndUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.AdminEndUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.CustomerEndUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.CustomerEndUser));
                    }

                    if (User.IsInRole(SD.AdminEndUser))
                    {
                        if (Input.IsAdmin)
                        {
                            await _userManager.AddToRoleAsync(user, SD.AdminEndUser);

                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(user, SD.CustomerEndUser);
                        }
                        return RedirectToPage("/Users/Index");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.CustomerEndUser);
                    }


                    _logger.LogInformation("Пользователь создал новый аккаунт с паролем.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Подтвердите свой адрес электронной почты",
                       $"Пожалуйста, подтвердите свою учетную запись, <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'> нажав сюда</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("Регистрация завершена!", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
