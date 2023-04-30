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
        public string model_name { get; set; }
        public string color { get; set; }
        public int registration_number { get; set; }
        public string brand_name { get; set; }
        public double rental_cost { get; set; }
        public AvailabilityStatus availability_status { get; set; }
        public string? photo { get; set; }


    }
}
