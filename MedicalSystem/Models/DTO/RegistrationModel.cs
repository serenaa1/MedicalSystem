using System.ComponentModel.DataAnnotations;

namespace MedicalSystem.Models.DTO
{
    public class RegistrationModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [RegularExpression("")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string? Role { get; set; }
    }
}
