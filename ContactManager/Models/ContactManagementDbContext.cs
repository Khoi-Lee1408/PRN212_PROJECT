using ContactManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ContactManagementDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<CallHistory> CallHistories { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=ContactDb;Trusted_Connection=True;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>().HasData(
                new Contact { Id = 1, FullName = "Nguyen Van An", PhoneNumber = "0912345678", Email = "nguyenvanan@example.com", Address = "Ha Noi", Category = "Ban be" },
                new Contact { Id = 2, FullName = "Tran Thi Binh", PhoneNumber = "0912345678", Email = "tranthibinh@example.com", Address = "Ho Chi Minh", Category = "Nguoi than" },
                new Contact { Id = 3, FullName = "Le Van Cuong", PhoneNumber = "0912345678", Email = "levancuong@example.com", Address = "Đa Nang", Category = "Shipper Shopee" },
                new Contact { Id = 4, FullName = "Pham Thi Dung", PhoneNumber = "0912345678", Email = "phamthidung@example.com", Address = "Hai Phong", Category = "Dong nghiep" },
                new Contact { Id = 5, FullName = "Hoang Van Đuc", PhoneNumber = "0912345678", Email = "hoangvanduc@example.com", Address = "Can Tho", Category = "Nguoi than" },
                new Contact { Id = 6, FullName = "Đo Thi Hanh", PhoneNumber = "0912345678", Email = "dothihanh@example.com", Address = "Hue" },
                new Contact { Id = 7, FullName = "Bui Van Giang", PhoneNumber = "0912345678", Email = "buivangiang@example.com", Address = "Nha Trang" }
            );
            modelBuilder.Entity<CallHistory>()
                .Property(c => c.CallTime)
                .HasColumnType("time(0)");
            modelBuilder.Entity<CallHistory>().HasData(
                new CallHistory { Id = 1, ContactId = 1, CallDate = new DateOnly(2026, 3, 25), CallTime = new TimeOnly(9, 30, 0), Duration = 120, CallType = "Incoming" },
                new CallHistory { Id = 2, ContactId = 2, CallDate = new DateOnly(2026, 3, 25), CallTime = new TimeOnly(10, 15, 0), Duration = 300, CallType = "Outgoing" },
                new CallHistory { Id = 3, ContactId = 1, CallDate = new DateOnly(2026, 3, 26), CallTime = new TimeOnly(14, 45, 0), Duration = 0, CallType = "Missed" },
                new CallHistory { Id = 4, ContactId = 3, CallDate = new DateOnly(2026, 3, 27), CallTime = new TimeOnly(8, 20, 0), Duration = 180, CallType = "Incoming" },
                new CallHistory { Id = 5, ContactId = 2, CallDate = new DateOnly(2026, 3, 27), CallTime = new TimeOnly(19, 5, 0), Duration = 240, CallType = "Outgoing" },
                new CallHistory { Id = 6, ContactId = 4, CallDate = new DateOnly(2026, 3, 28), CallTime = new TimeOnly(11, 0, 0), Duration = 60, CallType = "Incoming" },
                new CallHistory { Id = 7, ContactId = 1, CallDate = new DateOnly(2026, 3, 28), CallTime = new TimeOnly(21, 15, 0), Duration = 0, CallType = "Missed" }
            );
        }

    }
}
