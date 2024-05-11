using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Shop.Models
{
    public partial class ShopsContext : DbContext
    {
        public ShopsContext()
        {
        }

        public ShopsContext(DbContextOptions<ShopsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Category_Name");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("Product_id");

                entity.Property(e => e.PcategoryId).HasColumnName("Pcategory_Id");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Product_Name");

                entity.Property(e => e.ProductQuantity).HasColumnName("Product_Quantity");

                entity.HasOne(d => d.Pcategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PcategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Pcatego__25869641");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_Email");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_Name");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_Password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
