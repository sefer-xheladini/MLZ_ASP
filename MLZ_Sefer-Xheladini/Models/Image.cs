using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLZ_Sefer_Xheladini.Models
{
    public class Image
{
    public int ImageId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] Content { get; set; }
    public string ContentType { get; set; }

}
}
