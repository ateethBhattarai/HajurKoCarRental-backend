using HajurKoCarRental_backend.Model;

namespace HajurKoCarRental_backend.DTOs
{
    public class CarRegisterDto
    {
        public string model { get; set; }
        public string color { get; set; }
        public int registration_number { get; set; }
        public string brand { get; set; }
        public double rental_cost { get; set; }
        public AvailabilityStatus availability_status { get; set; }
        public string description { get; set; }
        public double mileage { get; set; }
        public int year { get; set; }
        public IFormFile? photo { get; set; }

    }
}
