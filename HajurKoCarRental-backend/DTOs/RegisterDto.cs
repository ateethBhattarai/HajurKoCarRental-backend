using HajurKoCarRental_backend.Model;
using System.ComponentModel.DataAnnotations;

namespace HajurKoCarRental_backend.DTOs
{
    public class RegisterDto
    {
        public string full_name { get; set; }
        public string password { get; set; }
        public DateTime date_of_birth { get; set; }
        public string phone_number { get; set; }
        public string email_address { get; set; }
        public string address { get; set; }
        public IFormFile? profile_picture { get; set; }
        public IFormFile? document { get; set; }
        public Role role { get; set; }
    }
}
