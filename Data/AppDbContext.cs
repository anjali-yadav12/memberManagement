using Microsoft.EntityFrameworkCore;
using member_managment.Models;
using member_managment.Data;


namespace member_managment.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Point>()
                .HasOne(p => p.Member)
                .WithMany(m => m.Points)
                .HasForeignKey(p => p.MemberID);

            modelBuilder.Entity<Coupon>()
                .HasOne(c => c.Member)
                .WithMany(m => m.Coupons)
                .HasForeignKey(c => c.MemberID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
