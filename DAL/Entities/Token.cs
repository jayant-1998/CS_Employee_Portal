using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_Portal.DAL.Entities
{
    [Table("token", Schema = "dbo")]
    public class Token
    {
        [Key]
        public int Id { get; set; }
        public string EncryptionKey { get; set; }
        public string SecretKeyEncrypted { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
