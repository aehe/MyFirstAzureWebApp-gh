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

            // Save the user's email and password to your database or perform any other necessary actions

            // Send email confirmation
            await SendEmailConfirmation(Input.Email);

            return RedirectToPage("Confirmation");
        }

        private async Task SendEmailConfirmation(string email)
        {
            // Generate a unique token for email confirmation (you can use a library like Guid.NewGuid() for this)
            string token = Guid.NewGuid().ToString();

            // Save the token to your database along with the user's email

            // Compose the email message
            string confirmationLink = $"https://example.com/confirm?email={email}&token={token}";
            string subject = "Email Confirmation";
            string body = $"Please confirm your email address by clicking the link below:\n\n{confirmationLink}";

            // Send the email
            using (MailMessage message = new MailMessage("noreply@example.com", email, subject, body))
            {
                using (SmtpClient client = new SmtpClient("smtp.example.com"))
                {
                    // Configure the SMTP client with your email server settings
                    client.Send(message);
                }
            }
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