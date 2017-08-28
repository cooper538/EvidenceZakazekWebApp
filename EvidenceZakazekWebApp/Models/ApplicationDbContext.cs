using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace EvidenceZakazekWebApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<PropertyDefinition> PropertyDefinitions { get; set; }
        public DbSet<PropertyValue> PropertyValues { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // PŘÍKLAD
            //modelBuilder.Entity<Attendance>()
            //    .HasRequired(a => a.Gig)
            //    .WithMany(g => g.Attendances)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(u => u.Followers)
            //    .WithRequired(f => f.Followee)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(u => u.Followees)
            //    .WithRequired(f => f.Follower)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<UserNotification>()
            //   .HasRequired(un => un.User)
            //   .WithMany(u => u.UserNotifications)
            //   .WillCascadeOnDelete(false);

            modelBuilder.Entity<PropertyValue>()
                .HasRequired(pv => pv.Product)
                .WithMany(p => p.PropertyValues)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}