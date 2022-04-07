using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Context
{
   public class MyContext : DbContext
   {
      public MyContext(DbContextOptions<MyContext> options) : base(options)
      {

      }

      public DbSet<Employee> Employees { get; set; }
      public DbSet<Account> Accounts { get; set; }
      public DbSet<Profiling> Profilings { get; set; }
      public DbSet<Education> Educations { get; set; }
      public DbSet<University> Universities { get; set; }
      public DbSet<AccountRole> AccountRoles { get; set; }
      public DbSet<Role> Roles { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         // Employe-Account (one to one)
         modelBuilder.Entity<Employee>()
            .HasOne(a => a.Account)
            .WithOne(b => b.Employee)
            .HasForeignKey<Account>(b => b.NIK)
            .OnDelete(DeleteBehavior.ClientCascade);

         // Account-Profiling (one to one)
         modelBuilder.Entity<Account>()
            .HasOne(a => a.Profiling)
            .WithOne(b => b.Account)
            .HasForeignKey<Profiling>(b => b.NIK)
            .OnDelete(DeleteBehavior.ClientCascade);


         // Education-profiling (one to many)
         modelBuilder.Entity<Education>()
            .HasMany(a => a.Profilings)
            .WithOne(e => e.Education)
            .HasForeignKey(v => v.EducationId)
            .OnDelete(DeleteBehavior.ClientCascade);


         // University-Education (one to many)
         modelBuilder.Entity<University>()
            .HasMany(c => c.Educations)
            .WithOne(e => e.University)
            .HasForeignKey(b => b.UniversityId)
            .OnDelete(DeleteBehavior.ClientCascade);


         modelBuilder.Entity<AccountRole>()
            .HasOne(ar => ar.Account)
            .WithMany(a => a.AccountRoles)
            .HasForeignKey(ar => ar.AccountId)
            .OnDelete(DeleteBehavior.ClientCascade);

         modelBuilder.Entity<AccountRole>()
            .HasOne(ar => ar.Role)
            .WithMany(r => r.AccountRoles)
            .HasForeignKey(ar => ar.RoleId)
            .OnDelete(DeleteBehavior.ClientCascade);

      }
   }
}
