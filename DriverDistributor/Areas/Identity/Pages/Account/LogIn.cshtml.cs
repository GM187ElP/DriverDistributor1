using System.ComponentModel.DataAnnotations;
using DriverDistributor.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DriverDistributor.Areas.Identity.Pages.Account
{
    public class LogInModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogInModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
            [Display(Name = "کد پرسنلی")]
            public string Email { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "رمز عبور")]
            [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
            public string Password { get; set; }

            [Display(Name = "مرا به یاد داشته باش!")]
            public bool RememberMe { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
            {
                // Model validation errors
                ViewData["Errors"] = "لطفا فرم را به درستی پر کنید!";
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(
                Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Optional: show success modal before redirect
                ViewData["Success"] = "ورود موفقیت‌آمیز بود!";
                // Return Page and let front-end JS handle redirect after 3s
                return Page();
            }
            else if (result.IsLockedOut)
            {
                ViewData["Errors"] = "حساب شما قفل شده است.";
                return Page();
            }
            else
            {
                ViewData["Errors"] = "نام کاربری یا رمز عبور اشتباه است!";
                return Page();
            }
        }
    }
}
