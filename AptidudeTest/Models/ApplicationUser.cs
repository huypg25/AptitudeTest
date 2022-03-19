using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AptidudeTest.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 255 chars")]
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public string Address { get; set; } = string.Empty;
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$",
        ErrorMessage = "Characters are not allowed.")]
        public string Phone { get; set; } = string.Empty;

        public int Status { get; set; } = -1;
        public string Education { get; set; } = string.Empty;
        public string WorkExperience { get; set; } = string.Empty;
    }
}
