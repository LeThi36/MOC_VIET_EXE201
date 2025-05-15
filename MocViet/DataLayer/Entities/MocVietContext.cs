using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Entities;

public class MocVietContext : DbContext
{
    public MocVietContext(DbContextOptions<MocVietContext> options) : base(options) { }

    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Workshop> Workshops { get; set; }
    public DbSet<WorkshopRegistration> WorkshopRegistrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // CartItem
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Restrict); // Không cascade

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Product)
            .WithMany(p => p.CartItems)
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Restrict); // Không cascade

        // OrderDetail
        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId)
            .OnDelete(DeleteBehavior.Restrict); // KHÔNG dùng Cascade ở đây!

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Product)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(od => od.ProductId)
            .OnDelete(DeleteBehavior.Restrict); // Không cascade

        // ProductImage
        modelBuilder.Entity<ProductImage>()
            .HasOne(pi => pi.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Restrict); // Không cascade

        // Review
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Restrict); // Không cascade

        // WorkshopRegistration
        modelBuilder.Entity<WorkshopRegistration>()
        .HasOne(wr => wr.Workshop)
        .WithMany(w => w.WorkshopRegistrations)
        .HasForeignKey(wr => wr.WorkshopId)
        .OnDelete(DeleteBehavior.Restrict);  // Hoặc NoAction, không cascade delete
    }
}