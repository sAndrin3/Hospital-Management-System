
using AppointmentManagement.Models;
using Microsoft.EntityFrameworkCore;


namespace AppointmentManagement.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<Appointment1> Appointment1s { get; set; }
    }
}

