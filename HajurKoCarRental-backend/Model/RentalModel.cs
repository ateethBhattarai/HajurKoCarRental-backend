using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HajurKoCarRental_backend.Model
{
    //To manage or declare the rental status of the car
    public enum RentalStatus { success, pending }

    [Table("RentalRequest")]
    public class RentalModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime rental_date { get; set; }
        public bool discount { get; set; }
        public RentalStatus rental_status { get; set; }
        public int rental_amount { get; set; }
        public UserModel user { get; set; }
        public CarsModel cars { get; set; }

    }
}
