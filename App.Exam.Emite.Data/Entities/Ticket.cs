using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Exam.Emite.Data.Entities.Enums;
using App.Exam.Emite.Data.Entities.Base;

namespace App.Exam.Emite.Data.Entities
{
    public class Ticket :  BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("customerId")]
        public Customer? Customer{ get; set; }
        public int? CustomerId { get; set; }

        [Required]
        [Column("agentId ")]
        public int ?AgentId { get; set; }
        public Agent? Agent{ get; set; }

        [Required]
        [Column("status")]
        public TicketStatus Status { get; set; }

        [Required]
        [Column("priority")]
        public TicketPriority Priority { get; set; }

        [Required]
        [Column("description ")]
        public string? Description { get; set; }

        [Required]
        [Column("resolution ")]
        public string? Resolution { get; set; }
    }
}
