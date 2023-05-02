using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HajurKoCarRental_backend.Model
{
    //To manage or declare the role of the user
    public enum Role { Admin, Staff, Customer }

    //To name the table in database
    [Table("Users")]

    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string full_name { get; set; }
        public string password { get; set; }
        public DateTime date_of_birth { get; set; }
        public string phone_number { get; set; }
        public string email_address { get; set; }
        public string address { get; set; }
        public DateTime last_login { get; set; } = DateTime.UtcNow;
        public string? profile_picture { get; set; }
        public string? document { get; set; }

        public Role Role { get; set; }

        //creating place for JWT Access Token
        [NotMapped]
        public string JwtToken { get; set; }
    }
}
