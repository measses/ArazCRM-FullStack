using ArazCRM.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Offers -> Jobs ilişkisi: İş silindiğinde teklifler silinmez
            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Job)
                .WithMany(j => j.Offers)
                .HasForeignKey(o => o.JobId)
                .OnDelete(DeleteBehavior.Restrict);  // Cascade yerine Restrict kullanıldı

            // Offers -> Customers ilişkisi: Müşteri silindiğinde teklifler silinir
            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Offers)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);  // Müşteri silindiğinde teklifler de silinsin

            // Invoices -> Jobs ilişkisi: İş silindiğinde faturalar silinmez
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Job)
                .WithMany(j => j.Invoices)
                .HasForeignKey(i => i.JobId)
                .OnDelete(DeleteBehavior.Restrict);  // İş silindiğinde fatura silinmesin

            // Invoices -> Customers ilişkisi: Müşteri silindiğinde faturalar silinir
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);  // Müşteri silindiğinde faturalar da silinsin

            // Appointments -> Jobs ilişkisi: İş silindiğinde randevular silinmez
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Job)
                .WithMany(j => j.Appointments)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Restrict);  // İş silindiğinde randevu silinmesin

            // Appointments -> Customers ilişkisi: Müşteri silindiğinde randevular silinir
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);  // Müşteri silindiğinde randevular da silinsin

            // Expenses -> Jobs ilişkisi: İş silindiğinde masraflar silinir
            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Job)
                .WithMany(j => j.Expenses)
                .HasForeignKey(e => e.JobId)
                .OnDelete(DeleteBehavior.Cascade);  // İş silindiğinde masraflar da silinsin

            base.OnModelCreating(modelBuilder);
        }


    }
}
