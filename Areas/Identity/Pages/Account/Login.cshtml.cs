using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ddt_site_v02.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

    public string? ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
            [Display(Name = "Kullanıcı Adı")]
            public string? UserName { get; set; }

            [Required(ErrorMessage = "Şifre zorunludur.")]
            [DataType(DataType.Password)]
            [Display(Name = "Şifre")]
            public string? Password { get; set; }

            [Display(Name = "Beni Hatırla")]
            public bool RememberMe { get; set; }
        }

        public void OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        // Default redirect to /Home when no returnUrl provided
        returnUrl = returnUrl ?? Url.Content("/Home");

        // Giriş başarılıysa yönlendir (öncelik: güvenli returnUrl)
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(Input.UserName ?? string.Empty, Input.Password ?? string.Empty, Input.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("Kullanıcı giriş yaptı.");
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }
                return Redirect("/Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi.");
                return Page();
            }
        }
        // Hatalıysa formu tekrar göster
        return Page();
    }
    }
}
