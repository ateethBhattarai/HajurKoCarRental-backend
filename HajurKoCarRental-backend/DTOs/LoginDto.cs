using System.ComponentModel.DataAnnotations;

namespace HajurKoCarRental_backend.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Email field is required!!")]
        public string email_address { get; set; }

        [Required(ErrorMessage ="Password field is required!!")]
        public string password { get; set; }
    }
}
