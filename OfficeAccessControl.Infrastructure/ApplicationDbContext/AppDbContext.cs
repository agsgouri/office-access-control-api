using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OfficeAccessControl.Core.Models;
using OfficeAccessControl.Infrastructure.Identity;

namespace OfficeAccessControl.Infrastructure.ApplicationDbContext
{
    public class AppDbContext
        : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }


        public DbSet<OfficeLocation> OfficeLocations { get; set; }
        public DbSet<AccessLog> AccessLogs { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<AttendanceSession> AttendanceSessions { get; set; }
        public DbSet<OfficeLocationAllowedRole> OfficeLocationAllowedRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttendanceSession>()
                .HasOne(s => s.Attendance)
                .WithMany(a => a.Sessions)
                .HasForeignKey(s => s.AttendanceId);
            modelBuilder.Entity<OfficeLocationAllowedRole>()
                .HasOne(r => r.OfficeLocation)
                .WithMany(l => l.AllowedRoles)
                .HasForeignKey(r => r.LocationId)
                .HasPrincipalKey(l => l.Id);

            base.OnModelCreating(modelBuilder);
        }

    }
}
