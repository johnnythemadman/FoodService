using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FoodService.DAO
{
    public partial class FoodServiceContext : DbContext
    {
        public FoodServiceContext()
        {
        }

        public FoodServiceContext(DbContextOptions<FoodServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<FoodItem> FoodItem { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<TypeOfFood> TypeOfFood { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-D4KP4PD;Initial Catalog=FoodService;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FoodItem>(entity =>
            {
                entity.Property(e => e.FoodItemId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.TypeOfFoodIdRefNavigation)
                    .WithMany(p => p.FoodItem)
                    .HasForeignKey(d => d.TypeOfFoodIdRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FoodItem__TypeOf__398D8EEE");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.CustomerIdRefNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerIdRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__CustomerI__45F365D3");

                entity.HasOne(d => d.EmployeeRefNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.EmployeeRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__EmployeeR__46E78A0C");

                entity.HasOne(d => d.PaymentIdRefNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.PaymentIdRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__PaymentId__47DBAE45");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.OrderItemId).ValueGeneratedNever();

                entity.HasOne(d => d.FoodItemIdRefNavigation)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.FoodItemIdRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__FoodI__3C69FB99");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasIndex(e => e.Code)
                    .HasName("UQ__Payment__A25C5AA7C5B28973")
                    .IsUnique();

                entity.Property(e => e.PaymentId).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TypeOfFood>(entity =>
            {
                entity.Property(e => e.TypeOfFoodId).ValueGeneratedNever();

                entity.Property(e => e.Image).HasMaxLength(1);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
