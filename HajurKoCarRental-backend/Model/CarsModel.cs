using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HajurKoCarRental_backend.Model
{
    //To manage the status of the availability of car for rental
    public enum AvailabilityStatus { unavailable, available }

    [Table("Cars")]
    public class CarsModel
    {
        [Key]
        public int Id { get; set; }
        public string model { get; set; }
        public string color { get; set; }
        public int registration_number { get; set; }
        public string brand { get; set; }
        public double rental_cost { get; set; }
        public AvailabilityStatus availability_status { get; set; }
        public string description { get; set; }
        public double mileage { get; set; }
        public int year { get; set; }
        public string? photo { get; set; }
    }
}
