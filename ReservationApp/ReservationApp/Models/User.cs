using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        [Display(Name = "FirstNam")]
        public string FirstName  { get; set; }

        [Required(ErrorMessage = "Entrer Prenom de LastName")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
