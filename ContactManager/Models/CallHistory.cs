using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ContactManager.Models
{
    public class CallHistory
    {
        public int Id { get; set; }

        [ForeignKey("Contact")]
        public int ContactId { get; set; }
        public DateOnly CallDate { get; set; }

        public TimeOnly CallTime { get; set; }
        public int Duration { get; set; }

        [Required, MaxLength(20)]
        public string CallType { get; set; }

    }
}
