using ContactManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
                new Contact { Id = 6, FullName = "Đo Thi Hanh", PhoneNumber = "0912345678", Email = "dothihanh@example.com", Address = "Hue"},
                new Contact { Id = 7, FullName = "Bui Van Giang", PhoneNumber = "0912345678", Email = "buivangiang@example.com", Address = "Nha Trang"}
            );
        }
    }
}
