using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MyFirstAzureWebApp.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SignUpModel> _logger;

        public SignUpModel(IConfiguration configuration, ILogger<SignUpModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

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

            // Generate a confirmation code
            string confirmationCode = Guid.NewGuid().ToString();

            // Save the confirmation code and user details in your database

            // Send confirmation email
            await SendConfirmationEmail(Input.Email, confirmationCode);

            // Redirect to a page indicating that a confirmation email has been sent
            return RedirectToPage("ConfirmationSent");
        }

        private async Task SendConfirmationEmail(string email, string confirmationCode)
        {
            string emailFrom = _configuration["EmailSettings:From"];
            string emailSubject = "Confirm your email";
            string emailBody = $"Please confirm your email by clicking the following link: {confirmationCode}";

            using (MailMessage mailMessage = new MailMessage(emailFrom, email, emailSubject, emailBody))
            {
                using (SmtpClient smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"]))
                {
                    smtpClient.Port = Convert.ToInt32(_configuration["EmailSettings:SmtpPort"]);
                    smtpClient.Credentials = new System.Net.NetworkCredential(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
                    smtpClient.EnableSsl = true;

                    await smtpClient.SendMailAsync(mailMessage);
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