using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ReservationApp.Models;

namespace ReservationApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ReservationApp.Models.Reservation> Reservation { get; set; }
        public DbSet<ReservationApp.Models.TypeReservation> TypeReservation { get; set; }
        public DbSet<ReservationApp.Models.User> User { get; set; }
    }
}
