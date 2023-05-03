using HajurKoCarRental_backend.Model;

namespace HajurKoCarRental_backend.DTOs
{
    public class DamagedCarRegistrationDto
    {
        public string damage_description { get; set; }
        public SettlementStatus settlement_status { get; set; }
        public int repair_cost { get; set; }
        public int Users_id { get; set; }
        public int Cars_id { get; set; }
    }
}
