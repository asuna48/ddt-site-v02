using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace ddt_site_v02.Pages
{
    [Authorize]
    public class HomeModel : PageModel
    {
        public string? UserName { get; set; }
        public void OnGet()
        {
            UserName = User.Identity?.Name;
        }
    }
}