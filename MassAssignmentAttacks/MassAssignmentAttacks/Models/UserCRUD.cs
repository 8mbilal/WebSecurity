namespace MassAssignmentAttacks.Models
{
    public class UserCRUD
    {
        public void Add(User user)
        {
            if (user.Email == "admin@example.com")
            {
                user.IsAdmin = true;
            }
            // Save user to database
        }
    }
}