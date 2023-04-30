using HajurKoCarRental_backend.DTOs;
using HajurKoCarRental_backend.Model;

namespace HajurKoCarRental_backend.Extensions
{
    public class Extensions
    {
        //converting DTO as model
        public static UserModel ToUser(LoginDto model)
        {
            return new UserModel
            {
                email_address = model.email_address,
                password = model.password
            };
        }
    }
}
