using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class BackendContext: DbContext
    {
        // One to Many
        public DbSet<Cos_Cumparaturi> Cos { get; set; }
        public DbSet<Produse> Produs { get; set; }

        // One to One
        //public DbSet<Cos_Cumparaturi> Cos { get; set; }
        public DbSet<Discounts> Discount { get; set; }

        // Many to Many
        //public DbSet<Produse> Produs { get; set; }
        public DbSet<Stoc> Stocul { get; set; }
        public DbSet<StocProdus> StocProduse { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<RegisterModel> Register { get; set; }

        public DbSet<LoginModel> Login { get; set; }
        public BackendContext(DbContextOptions<BackendContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One to Many
            modelBuilder.Entity<Cos_Cumparaturi>()
                        .HasMany(m1 => m1.Produse_Alese)
                        .WithOne(m2 => m2.Cos);

            // One to One
            modelBuilder.Entity<Cos_Cumparaturi>()
                .HasOne(m5 => m5.Discount)
                .WithOne(m6 => m6.Cos_Redus)
                .HasForeignKey<Discounts>(m6 => m6.Cos_CumparaturiId);

            // Many to Many
            modelBuilder.Entity<StocProdus>().HasKey(mr => new { mr.ProdusId, mr.StocId });

            // One to many for m-m
            modelBuilder.Entity<StocProdus>()
                        .HasOne(mr => mr.Produs)
                        .WithMany(m3 => m3.StocProduse)
                        .HasForeignKey(mr => mr.ProdusId);

            modelBuilder.Entity<StocProdus>()
                        .HasOne(mr => mr.Stoc)
                        .WithMany(m4 => m4.StocProduse)
                        .HasForeignKey(mr => mr.StocId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
