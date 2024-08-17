using System.ComponentModel.DataAnnotations;
namespace MassAssignmentAttacks.Models
{
    public class User
    {
        // [BindNever] use it if you are using RegisterSafe2 action method for form submission
        public int Id { get; set; } // Sensitive property (auto-incremented)

        [Required]
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
        // [BindNever] use it if you are using RegisterSafe2 action method for form submission
        public bool IsAdmin { get; set; } = false; // Sensitive property (not set by user)
    }
}