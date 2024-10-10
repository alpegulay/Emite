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
    public class Customer :  BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string? Name { get; set; }

        [Required]
        [Column("email")]
        public string? Email { get; set; }

        [Required]
        [Column("phoneNumber")]
        public string? PhoneNumber { get; set; }

        [Required]
        [Column("lastContactDate")]
        public DateTime? LastContactDate { get; set; }
    }
}
