using System.ComponentModel.DataAnnotations.Schema;

namespace HajurKoCarRental_backend.Model
{
    //To name the table in database
    [Table("Notification")]
    public class NotificationModel
    {
        public int Id { get; set; }
        public string notification_description { get; set; }

        [ForeignKey("Offers")]
        public int? Offer_id { get; set; }
        public virtual OffersModel? Offers { get; set; }

        [ForeignKey("Users")]
        public int Users_id { get; set; }
        public virtual UserModel? Users { get; set; }


        [ForeignKey("Cars")]
        public int Cars_id { get; set; }

        public virtual CarsModel? Cars { get; set; }
    }
}
