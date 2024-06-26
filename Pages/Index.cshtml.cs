using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyFirstAzureWebApp.Pages;

public class IndexModel : PageModel
{
    // Other properties and methods...

    public bool IsEmailConfirmed { get; set; } // Add this line

    // Other properties and methods...
}