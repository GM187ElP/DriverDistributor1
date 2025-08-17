using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DriverDistributor.Data;
using DriverDistributor.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        if (!ModelState.IsValid)
        {
            // Collect model errors to show as error alert
            ViewData["Errors"] = string.Join("<br/>", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
            return Page();
        }

        var personnel = await _dbContext.Personnels.FirstOrDefaultAsync(p => p.PersonnelCode == Input.Username);

        if (personnel == null)
        {
            ViewData["Errors"] = "شما اجازه ثبت نام در این وبسایت را ندارید!";
            return Page();
        }

        var existingUser = await _userManager.FindByNameAsync(Input.Username);
        if (existingUser != null)
        {
            ViewData["Warning"] = "شما قبلا ثبت نام کرده اید.";
            return Page();
        }

        var user = new ApplicationUser
        {
            UserName = Input.Username,
            PhoneNumber = personnel.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, Input.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            ViewData["Success"] = "ثبت نام با موفقیت انجام شد!";
            return Page();
        }

        // If creation failed
        ViewData["Errors"] = string.Join("<br/>", result.Errors.Select(e => e.Description));
        return Page();
    }

}