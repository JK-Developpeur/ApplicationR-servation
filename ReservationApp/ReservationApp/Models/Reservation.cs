using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationApp.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public Status Status { get; set; }
        public int? TypeReservationid { get; set; }
        public TypeReservation TypeReservation { get; set; }

        [ForeignKey("UserId")]
        public string  UserId { get; set; }
        public virtual ApplicationUsers User { get; set; }

    }
}
