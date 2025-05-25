using Microsoft.EntityFrameworkCore;
using Przychodnia.Models;

namespace Przychodnia.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors   { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }
        
    }
}