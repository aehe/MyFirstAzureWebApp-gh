using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyFirstAzureWebApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public SignUpModel SignUp { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Send email confirmation
            var confirmationCode = GenerateConfirmationCode();
            await SendConfirmationEmail(SignUp.Email, confirmationCode);

            // Save user data to database or perform other actions

            return RedirectToPage("Confirmation", new { email = SignUp.Email });
        }

        private string GenerateConfirmationCode()
        {
            // Generate a unique confirmation code
            // You can use a library like Guid.NewGuid() or any other method
            return "ABC123";
        }

        private async Task SendConfirmationEmail(string email, string confirmationCode)
        {
            // Send an email with the confirmation code to the provided email address
            // You can use libraries like SendGrid or the built-in SmtpClient class
            var message = new MailMessage("noreply@example.com", email)
            {
                Subject = "Email Confirmation",
                Body = $"Your confirmation code is: {confirmationCode}"
            };

            using (var client = new SmtpClient())
            {
                await client.SendMailAsync(message);
            }
        }
    }

    public class SignUpModel
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