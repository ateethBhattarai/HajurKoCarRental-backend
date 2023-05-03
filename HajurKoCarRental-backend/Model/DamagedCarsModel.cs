using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HajurKoCarRental_backend.Model
{
    //To manage or declare the settlement status of damages parts
    public enum SettlementStatus {pending, success }

    [Table("DamagedCars")]
    public class DamagedCarsModel
    {
        [Key]
        public int Id { get; set; }
        public string damage_description { get; set; }
        public SettlementStatus settlement_status { get; set; }
        public int repair_cost { get; set; }

        [ForeignKey("Users")]
        public int Users_id { get; set; }
        public virtual UserModel? Users { get; set; }

        [ForeignKey("Cars")]
        public int Cars_id { get; set; }

        public virtual CarsModel? Cars { get; set; }

    }
}
