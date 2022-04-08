using System;
using System.Collections.Generic;
using System.Text;
using Example.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Example.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        //configuring the model
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Item>()
                .Property(e => e.ItemName)
                .HasColumnType("varchar(512)");
            builder
                .Entity<Item>()
                .Property(e => e.Borrower)
                .HasColumnType("varchar(512)");
            builder
                .Entity<Item>()
                .Property(e => e.Lender)
                .HasColumnType("varchar(512)");

            builder
                .Entity<Expense>()
                .Property(e => e.ExpenseName)
                .HasColumnType("varchar(512)");
            builder
                .Entity<Expense>()
                .Property(e => e.Amount)
                .HasColumnType("Integer");
        }

        //Configuration can be performed by overriding the OnConfiguring method,
        // or by passing options to the constructor
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder
        )
        {
            optionsBuilder.UseSqlite("Data Source=Sqlite.db");
        }
    }
}
