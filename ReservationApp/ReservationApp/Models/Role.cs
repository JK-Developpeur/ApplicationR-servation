using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationApp.Models
{
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual List<string> Users { get; set; }

    }
}
