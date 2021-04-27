using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationApp.Models
{
    public class TypeReservation
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Entrer Nom")]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Display(Name = "Nombre")]
        public int? Nombre { get; set; }

    }
}
