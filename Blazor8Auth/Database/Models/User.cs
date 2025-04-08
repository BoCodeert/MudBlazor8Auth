using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor8Auth.Database.Models
{
    public class User
    {
        [Key]   
        public int Id { get; set; }
        [Required, MaxLength(255), EmailAddress]
        public string Email { get; set; }
        [MaxLength]
        public string? PasswordHash { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
