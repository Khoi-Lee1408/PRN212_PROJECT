using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContactManager.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required, MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        [MaxLength(200)]
        public string? Category { get; set; }
    }
}
