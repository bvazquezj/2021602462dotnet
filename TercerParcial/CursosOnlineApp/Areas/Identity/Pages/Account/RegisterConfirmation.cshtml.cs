using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CursosOnlineApp.Areas.Identity.Pages.Account
{
    public class RegisterConfirmationModel : PageModel
    {
        public required string Email { get; set; }

        public IActionResult OnGet(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToPage("/Index");
            }

            Email = email;
            return Page();
        }
    }
}
