using System.ComponentModel.DataAnnotations;

namespace MassAssignmentAttacks.Models
{
    // Safe model (only bind the properties that are needed by the user)
    // in this model, properties name can be different from the User model
    public class UserViewModel 
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        public string UserPassword { get; set; }
    }
}