using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_Portal.DAL.Entities
{
    [Table("registered", Schema = "dbo")]
    public class Registration
    {
        [Key]
        public int UserId { get; set; }
        public string Role { get; set; } = "user";
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
