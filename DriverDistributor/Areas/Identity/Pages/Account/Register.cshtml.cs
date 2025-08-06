using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DriverDistributor.Data;
using DriverDistributor.Entities;

namespace DriverDistributor.Areas.Identity.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly AppDbContext _dbContext;

    public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext dbContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }

    public class InputModel
    {
        [Required]
        [Display(Name = "کد پرسنلی")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "تنها وارد کردن عدد مجاز است")]
        [Length(5, 5, ErrorMessage = "کد پرسنلی 5 رقمی است")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "وارد کردن رمز عبور الزامی می باشد")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "تنها وارد کردن عدد مجاز است")]
        [MinLength(4, ErrorMessage = "رمز عبور باید حداقل 4 رقم باشد")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار رمز عبور یکسان نمی باشند")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "تنها وارد کردن عدد مجاز است")]
        public string ConfirmPassword { get; set; }
    }

    public void OnGet(string returnUrl = null)
    {
        ReturnUrl = returnUrl ?? Url.Content("~/");
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            var personnel = await _dbContext.Personnels.FirstOrDefaultAsync(p => p.PersonnelCode == Input.Username);
            var user = new ApplicationUser { UserName = Input.Username };

            if (personnel != null)
                user.PhoneNumber = personnel.PhoneNumber;

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect("/");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return Page();
    }
}
