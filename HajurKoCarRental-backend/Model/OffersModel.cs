using System.ComponentModel.DataAnnotations.Schema;

namespace HajurKoCarRental_backend.Model
{
    //To name the table in database
    [Table("Offers")]
    public class OffersModel
    {
        public int Id { get; set; }
        public double offer_price { get; set; }

        [ForeignKey("Users")]
        public int Users_id { get; set; }
        public virtual UserModel? Users { get; set; }


        [ForeignKey("Cars")]
        public int Cars_id { get; set; }

        public virtual CarsModel? Cars { get; set; }
    }
}
