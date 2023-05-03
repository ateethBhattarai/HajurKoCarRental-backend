using HajurKoCarRental_backend.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HajurKoCarRental_backend.DTOs
{
    public class CarBookingDto
    {
        public double discount { get; set; }
        public double rental_amount { get; set; }
        public DateTime start_date { get; set; } = DateTime.UtcNow;
        public DateTime end_date { get; set; }
        public RentalStatus rental_status { get; set; } = RentalStatus.pending;
        public int Users_id { get; set; }
        public int Cars_id { get; set; }
    }
}
