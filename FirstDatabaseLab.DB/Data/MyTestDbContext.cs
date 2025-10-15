using System;
using System.Collections.Generic;
using FirstDatabaseLab.DB.Schema;
using Microsoft.EntityFrameworkCore;

namespace FirstDatabaseLab.DB.Data;

public partial class MyTestDbContext : DbContext
{
    public MyTestDbContext()
    {
    }

    public MyTestDbContext(DbContextOptions<MyTestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Id).HasComment("我是Id欄位說明");
            entity.Property(e => e.Email).HasComment("我是Email欄位說明");
            entity.Property(e => e.Name).HasComment("我是Name欄位說明");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
