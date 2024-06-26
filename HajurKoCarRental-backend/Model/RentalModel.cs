﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HajurKoCarRental_backend.Model
{
    //To manage or declare the rental status of the car
    public enum RentalStatus { success, pending, cancelled }
    public enum Discount { available, unavailable}

    [Table("RentalRequest")]
    public class RentalModel
    {
        [Key]
        public int Id { get; set; }
        //public double discount { get; set; }
        public string accepted_by { get; set; } = "Not Accepted Yet";
        public Discount discount { get; set; } = Discount.unavailable;
        public double rental_amount { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public RentalStatus rental_status { get; set; } = RentalStatus.pending;

        [ForeignKey("Users")]
        public int Users_id { get; set; }
        public virtual UserModel? Users { get; set; }

        [ForeignKey("Cars")]
        public int Cars_id { get; set; }

        public virtual CarsModel? Cars { get; set; }

    }
}