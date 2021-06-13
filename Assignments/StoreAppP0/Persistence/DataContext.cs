using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Persistence
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationProductInventoryJunction> LocationProductInventoryJunctions { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProductInventoryJunction> OrderProductInventoryJunctions { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=P0DB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccountCreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AddressCity)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.AddressState)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.AddressStreet)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AddressCity)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.AddressState)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.AddressStreet)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LocationProductInventoryJunction>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LocationProductInventoryJunction");

                entity.Property(e => e.ItemsPerOrder).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Location)
                    .WithMany()
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__LocationP__Locat__44FF419A");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__LocationP__Produ__45F365D3");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.LastOrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderCreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Orders__Customer__4222D4EF");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__Orders__Location__412EB0B6");
            });

            modelBuilder.Entity<OrderProductInventoryJunction>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("OrderProductInventoryJunction");

                entity.HasOne(d => d.Order)
                    .WithMany()
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderProd__Order__48CFD27E");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__OrderProd__Produ__49C3F6B7");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
