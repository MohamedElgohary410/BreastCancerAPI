using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BreastCancerAPI.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ClientDetails> ClientDetails { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<PatientProfile> PatientProfiles { get; set; }
    }
}