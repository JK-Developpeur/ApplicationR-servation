using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationApp.Models
{
    public class ApplicationUsers : IdentityUser
    {
        public int counter { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }


    }
}
