using PIXY.Models;
using Microsoft.EntityFrameworkCore;

namespace PIXY.Data
{
    public class PIXYContext : DbContext
    {
        public PIXYContext(DbContextOptions<PIXYContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PurchasedItem> PurchasedItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Image>().ToTable("Image");
            modelBuilder.Entity<Cart>().ToTable("Cart");
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
            modelBuilder.Entity<PurchasedItem>().ToTable("PurchasedItem");
        }

       // public DbSet<PIXY.Models.PurchasedItem> PurchasedItem { get; set; }
    }
}