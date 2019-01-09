using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLZ_Sefer_Xheladini.Models
{
    public class User
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Password { get; set; }
        public UserType UserType { get; set; }
}
}
