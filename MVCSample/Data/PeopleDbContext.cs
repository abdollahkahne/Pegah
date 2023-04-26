using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MVCSample.Data
{
    public class PeopleDbContext : DbContext
    {
        public PeopleDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Person> People {get;set;}
        public virtual DbSet<Education> Educations {get;set;}
        public virtual DbSet<University> Universities {get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // define keys for entities
            modelBuilder.Entity<Person>().HasKey(p=>p.Id); // It can be changed to national Id
            modelBuilder.Entity<Education>().HasKey(e => e.Id);
            modelBuilder.Entity<University>().HasKey(u => u.Id);
            // Change Name of Persons Table to People
            modelBuilder.Entity<Person>().ToTable("People");
            // define relation between education and person and make it required
            modelBuilder.Entity<Education>().HasOne<Person>(e=>e.Person).WithMany(p =>p.Educations).HasForeignKey("PersonId").IsRequired();
            //  define relationship between education and university and it is optional 
            modelBuilder.Entity<Education>().HasOne<University>(e=>e.University).WithMany(u=>u.Educations).HasForeignKey("UniversityId");
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            foreach (var item in ChangeTracker.Entries<Person>())
            {
                if (item.State==EntityState.Added) {
                    item.CurrentValues["Created"]=DateTime.Now; // This can be done when creating entity too but overwrite here!
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}