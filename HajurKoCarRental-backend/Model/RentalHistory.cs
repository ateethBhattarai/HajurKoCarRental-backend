using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HajurKoCarRental_backend.Model
{
    [Table("RentalHistory")]
    public class RentalHistory
    {
        [Key]
        public int Id { get; set; }
        public UserModel user { get; set; }
        public CarsModel cars { get; set; }
        public DateTime requested_date { get; set; }
        public string authorized_by { get; set; }
        public int rental_duration { get; set; }
        public int rental_charge { get; set; }
    }
}
