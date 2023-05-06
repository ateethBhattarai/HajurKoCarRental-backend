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
                password = model.password,
                Role = model.role
            };
        }

        public static RentalModel ToBookCars(this CarBookingDto model)
        {
            return new RentalModel
            {
                start_date = model.start_date,
                end_date = model.end_date,
                rental_status = model.rental_status,
                Users_id = model.Users_id,
                Cars_id = model.Cars_id
            };
        }

        public static CarsModel ToRegisterCars(this CarRegisterDto model)
        {
            return new CarsModel
            {
                year = model.year,
                model = model.model,
                brand = model.brand,
                registration_number = model.registration_number,
                mileage = model.mileage,
                description = model.description,
                availability_status = model.availability_status,
                rental_cost = model.rental_cost,
                color = model.color
            };
        }

        public static DamagedCarsModel ToRegisterDamagedCars(this DamagedCarRegistrationDto model)
        {
            return new DamagedCarsModel
            {
                damage_description = model.damage_description,
                settlement_status = model.settlement_status,
                repair_cost = model.repair_cost,
                Users_id = model.Users_id,
                Cars_id = model.Cars_id
            };
        }
    }
}
