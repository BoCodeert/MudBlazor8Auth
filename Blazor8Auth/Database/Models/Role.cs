namespace Blazor8Auth.Database.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string UserRole { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
