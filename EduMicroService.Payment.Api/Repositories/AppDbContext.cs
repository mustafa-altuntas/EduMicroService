﻿using Microsoft.EntityFrameworkCore;

namespace EduMicroService.Payment.Api.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever(); // Use sequential GUIDs
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.OrderCode).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Created).IsRequired();
                entity.Property(e => e.Amount).IsRequired().HasPrecision(18, 2);
                entity.Property(e => e.Status).IsRequired();
            });
                
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
