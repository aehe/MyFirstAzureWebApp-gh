using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyFirstAzureWebApp.Pages
{
    public class SignUpModel : PageModel
    {
        [BindProperty]
        public SignUpInputModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Save the user's information to the database

            // Send email confirmation
            string confirmationCode = Guid.NewGuid().ToString();
            string confirmationLink = $"{Request.Scheme}://{Request.Host}/SignUp/ConfirmEmail?code={confirmationCode}";

            // TODO: Send the confirmation email using an email service

            return RedirectToPage("/SignUp/ConfirmationSent");
        }
    }

    public class SignUpInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}