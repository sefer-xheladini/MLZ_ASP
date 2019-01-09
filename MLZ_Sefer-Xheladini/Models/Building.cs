using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLZ_Sefer_Xheladini.Models
{
    public class Building
{
    public int BuildingId { get; set; }
    public string Title { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public BuildingType BuildingType { get; set; }
    public int BedCount { get; set; }
    public string Condition { get; set; }
    public double PricePerDay { get; set; }
    public bool HasWlan { get; set; }
    public double WlanPrice { get; set; }
    public bool HasParking { get; set; }
    public double ParkingPrice { get; set; }
    public double Space { get; set; }
    public bool HasBalkony { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public bool IsActive { get; set; }
    public int ImageId { get; set; }
    public Image Image { get; set; }
    public ICollection<Image> ImageList { get; set; }


}
}
