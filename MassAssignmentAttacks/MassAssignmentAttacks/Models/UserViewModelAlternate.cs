using Microsoft.AspNetCore.Mvc;

namespace MassAssignmentAttacks.Models
{
    // Safe model (only bind the properties that are needed by the user)
    // in this model, properties name should be same as the User model
    
    [ModelMetadataType(typeof(User))]
    public class UserViewModelAlternate 
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}