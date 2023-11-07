﻿using Microsoft.EntityFrameworkCore;
using TPI_P3.Data.Entities;

namespace TPI_P3.Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<OrderLine> ProductLines { get; set; }


        public TPIContext(DbContextOptions<TPIContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasDiscriminator(u => u.UserType);


            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Name = "Seba",
                    Password = "123456",
                    Status = true,
                    UserName = "SebaR",
                    UserType = "Admin",
                }
                );

            modelBuilder.Entity<Product>().HasData(
                    new Product
                    {
                        ProductId = 1,
                        Description = "Zapatilla Nike",
                        Price = 1700,
                        Status = true,



                    },
                    new Product
                    {
                        ProductId = 2,
                        Description = "Zapatilla Adidas",
                        Price = 1600,
                        Status = true,
                        


                    });



            modelBuilder.Entity<Colour>().HasData(
                new Colour
                {
                    ColourId = 1,
                    ColourName = "Azul"
                },
                new Colour
                {
                    ColourId = 2,
                    ColourName = "Rojo"
                });

            modelBuilder.Entity<Size>().HasData(
                new Size
                {
                    SizeId = 4,
                    SizeName = "L",
                },
                new Size
                {
                    SizeId = 6,
                    SizeName = "XXL",
                },
                new Size
                {
                    SizeId = 7,
                    SizeName = "L"
                }
                );



            // TABLA ENTRE PRODUCT Y SIZE
            modelBuilder.Entity<Product>()
             .HasMany(p => p.Sizes)
             .WithMany()
             .UsingEntity(j => j
                .ToTable("SizesProducts")
                );

            // TABLA ENTRE PRODUCT Y COLOUR
            modelBuilder.Entity<Product>()
                .HasMany(c => c.Colours)
                .WithMany()
                .UsingEntity(j => j
                    .ToTable("ColoursProducts")
                    );

            modelBuilder.Entity<OrderLine>()
                .HasOne(ol => ol.product)
                .WithMany()
                .HasForeignKey(ol => ol.ProcuctId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderLines)
                .WithOne()
                .HasForeignKey(ol => ol.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

               
        }
    }
}

