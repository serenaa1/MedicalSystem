using MedicalSystem.Migrations;
using MedicalSystem.Models.Domain;
using MedicalSystem.Models.Paciente;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Models
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Patient> Patients { get; set; }
    }

}
