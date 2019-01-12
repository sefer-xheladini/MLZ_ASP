using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Booking2.Models
{
    public class Rent
{
    public int RentId { get; set; }
    public int BuildingId { get; set; }
    public Building Building { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }
    public double PricePerDays { get; set; }
    public bool GuestWasHere { get; set; }

}
}
