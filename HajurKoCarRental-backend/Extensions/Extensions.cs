using HajurKoCarRental_backend.DTOs;
using HajurKoCarRental_backend.Model;

namespace HajurKoCarRental_backend.Extensions
{
    public static class Extensions
    {
        //converting DTO as model
        public static UserModel ToUser(this LoginDto model)
        {
            return new UserModel
            {
                email_address = model.email_address,
                password = model.password
            };
        }

        public static UserModel ToRegister(this RegisterDto model)
        {
            return new UserModel
            {
                full_name = model.full_name,
                email_address = model.email_address,
                address = model.address,
                date_of_birth = model.date_of_birth,
                phone_number = model.phone_number,
                password = model.password
            };
        }
    }
}
