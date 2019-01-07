using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLZ_Sefer-Xheladini.Models
{
    public class Building
{
    public BuildingType buildingType { get; set; }
    public int Beds { get; set; }
    public string Condition { get; set; }
    public double Price { get; set; }
    public bool Wlan { get; set; }
    public bool WlanFree { get; set; }
    public double WlanPrice { get; set; }
    public bool Parking { get; set; }
    public bool ParkingFree { get; set; }
    public double ParkingPrice { get; set; }
    public double Space { get; set; }
    public bool Balkony { get; set; }
}
}
