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
        public bool available_discount { get; set; } = false;
        public double rental_amount { get; set; }
        public RentalStatus rental_status { get; set; } = RentalStatus.pending;

        [ForeignKey("Users")]
        public int Users_id { get; set; }
        public virtual UserModel? Users { get; set; }


        [ForeignKey("Cars")]
        public int Cars_id { get; set; }

        public virtual CarsModel? Cars { get; set; }

    }
}
