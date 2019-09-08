using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FoodService.FoodService.DataAccess.DAO.Database
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
        public virtual DbSet<OrderCustomer> OrderCustomer { get; set; }
        public virtual DbSet<OrderEmployee> OrderEmployee { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<TypeOfFood> TypeOfFood { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-D4KP4PD;Database=FoodService;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
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
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(4, 2)");

                entity.HasOne(d => d.TypeOfFoodIdRefNavigation)
                    .WithMany(p => p.FoodItem)
                    .HasForeignKey(d => d.TypeOfFoodIdRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FoodItem__TypeOf__2739D489");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.CustomerIdRefNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerIdRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__CustomerI__30C33EC3");

                entity.HasOne(d => d.EmployeeRefNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.EmployeeRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__EmployeeR__31B762FC");

                entity.HasOne(d => d.PaymentIdRefNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.PaymentIdRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__PaymentId__32AB8735");
            });

            modelBuilder.Entity<OrderCustomer>(entity =>
            {
                entity.HasKey(e => new { e.OrderIdRef, e.CustomerIdRef });

                entity.HasOne(d => d.CustomerIdRefNavigation)
                    .WithMany(p => p.OrderCustomer)
                    .HasForeignKey(d => d.CustomerIdRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderCust__Custo__3A4CA8FD");

                entity.HasOne(d => d.OrderIdRefNavigation)
                    .WithMany(p => p.OrderCustomer)
                    .HasForeignKey(d => d.OrderIdRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderCust__Order__395884C4");
            });

            modelBuilder.Entity<OrderEmployee>(entity =>
            {
                entity.HasKey(e => new { e.OrderIdRef, e.EmployeeIdRef });

                entity.HasOne(d => d.EmployeeIdRefNavigation)
                    .WithMany(p => p.OrderEmployee)
                    .HasForeignKey(d => d.EmployeeIdRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderEmpl__Emplo__3E1D39E1");

                entity.HasOne(d => d.OrderIdRefNavigation)
                    .WithMany(p => p.OrderEmployee)
                    .HasForeignKey(d => d.OrderIdRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderEmpl__Order__3D2915A8");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasOne(d => d.FoodItemIdRefNavigation)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.FoodItemIdRef)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__FoodI__3587F3E0");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__Order__367C1819");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasIndex(e => e.Code)
                    .HasName("UQ__Payment__A25C5AA79F5E0EF6")
                    .IsUnique();

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
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
