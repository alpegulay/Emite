using App.Exam.Emite.Data.Entities.Base;
using App.Exam.Emite.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Exam.Emite.Data.Entities
{
    public class Call :  BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("customerId ")]
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Required]
        [Column("agentId  ")]
        public int? AgentId { get; set; }
        public Agent? Agent { get; set; }

        [Required]
        [Column("startTime")]
        public DateTime? StartTime { get; set; }

        [Required]
        [Column("endTime")]
        public DateTime? EndTime { get; set; }

        [Required]
        [Column("callStatus")]
        public CallStatus Status { get; set; }

        [Required]
        [Column("notes ")]
        public string? Notes { get; set; }

    }
}
