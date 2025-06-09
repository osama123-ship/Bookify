using Bookify.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Data
{
    public class Context : IdentityDbContext<ApplicationUser,IdentityRole,string>
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Event> Events { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingDetails> BookingDetails { get; set; }
        public DbSet<TemporaryBooking> Temporaries { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public Context(DbContextOptions options) : base(options)
        {
        }
        public Context()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tickets)
            .HasForeignKey(t => t.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Company>()
           .HasOne(c => c.User)
           .WithOne(u => u.Company)
           .HasForeignKey<Company>(c => c.UserId);

            modelBuilder.Entity<Event>()
           .HasOne(e => e.Company).WithMany(c => c.Events)
           .HasForeignKey(e => e.CompanyId)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ticket>()
           .HasOne(t => t.TicketType)
           .WithMany()
           .HasForeignKey(t => t.TicketTypeId)
           .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
