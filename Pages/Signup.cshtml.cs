using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyFirstAzureWebApp.Pages
{
    public class SignupModel : PageModel
    {
        [BindProperty]
        public SignupInputModel Input { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Save the user's information to the database

            // TODO: Send email confirmation to the user's email address

            return RedirectToPage("/Confirmation");
        }
    }

    public class SignupInputModel
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