#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{

    public ApplicationDbContext() : base()
    {

    }
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDet> OrderDets { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.CategoryParentId, "IX_Categories_CategoryParentId");

            entity.Property(e => e.CategoryDesc).HasMaxLength(500);
            

        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.HasIndex(e => e.ReviewId, "IX_Comment_ReviewId");

            entity.Property(e => e.CommentText).HasMaxLength(50);

        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF3158FE53");

            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            
        });

        modelBuilder.Entity<OrderDet>(entity =>
        {
            entity.HasKey(e => e.RecId);

            entity.ToTable("OrderDet");

            entity.HasIndex(e => e.OrderId, "IX_OrderDet_OrderId");

            entity.HasIndex(e => e.ProductId, "IX_OrderDet_ProductId");

            


        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryId");

            

        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK_Review_1");

            entity.ToTable("Review");

            entity.HasIndex(e => e.ProductId, "IX_Review_ProductId");

            entity.HasIndex(e => e.UserId, "IX_Review_UserId");

            

        });

        

        base.OnModelCreating(modelBuilder);
    }


}