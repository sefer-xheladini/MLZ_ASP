using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking2.Models
{
    public class Image
{
    public int ImageId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] Content { get; set; }
    public string ContentType { get; set; }

    public int BuildingId { get; set; }
    public Building Building { get; set; }

    }
}
