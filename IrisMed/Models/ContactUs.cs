using IrisMed.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using IrisMed.Models;

namespace IrisMed.Models
{

    public class ContactUs
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = "";

        [EmailAddress]
        public string Email { get; set; } = "";

        [DataType(DataType.MultilineText)]
        public string Content { get; set; } = "";

        [DataType(DataType.MultilineText)]
        public string Response { get; set; }
    }
}
