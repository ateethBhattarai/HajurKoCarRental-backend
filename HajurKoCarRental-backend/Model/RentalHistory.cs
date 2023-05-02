using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HajurKoCarRental_backend.Model
{
    [Table("RentalHistory")]
    public class RentalHistory
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime requested_date { get; set; }
        public string authorized_by { get; set; }
        public int rental_duration { get; set; }
        public double rental_charge { get; set; }
        [ForeignKey("Users")]
        public int Users_id { get; set; }
        public virtual UserModel? Users { get; set; }

        [ForeignKey("Cars")]
        public int Cars_id { get; set; }

        public virtual CarsModel? Cars { get; set; }
    }
}
