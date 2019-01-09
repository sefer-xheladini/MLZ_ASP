using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLZ_Sefer_Xheladini.Models
{
    public class Rent
{
    public int RentId { get; set; }
    public int BuildingTypeId { get; set; }
    public Building Building { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public double PricePerDaya { get; set; }
    public bool GuestWasHere { get; set; }
}
}
