using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace IrisMed.Models
{
    public class ContactUsModel: PageModel
    {
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [DataType(DataType.MultilineText)]
            [Required]
            public string Content { get; set; }
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
