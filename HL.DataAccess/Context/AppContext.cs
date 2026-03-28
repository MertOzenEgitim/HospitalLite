using HL.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace HL.DataAccess.Context;

public class AppDbContext : DbContext
{
   public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
   {
    
   }
   public DbSet<AppUser> Users {get;set;}
   public DbSet<Doctor> Doctors {get;set;}
   public DbSet<Department> Departments {get;set;}
   public DbSet<Appointment> Appointments {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>()
        .HasOne(a=>a.Patient)
        .WithMany()
        .HasForeignKey(a=>a.PatientId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>()
        .HasOne(a=>a.Doctor)
        .WithMany()
        .HasForeignKey(a=>a.DoctorId)
        .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}