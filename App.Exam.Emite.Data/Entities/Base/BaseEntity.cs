using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Exam.Emite.Data.Entities.Enums;
using App.Exam.Emite.Data.Interfaces.Entities;

namespace App.Exam.Emite.Data.Entities.Base
{
    public abstract class BaseEntity : IAuditable
    {

        [Column("created_date")]
        public DateTime? CreatedDate { get; set; }

        [MaxLength(36)]
        [Column("created_by_user_id")]
        public int CreatedByUserId { get; set; }

        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }

        [MaxLength(36)]
        [Column("updated_by_user_id")]
        public int UpdatedByUserId { get; set; }
        [Column("entity_status")]
        public int EntityStatus { get; set; } = (int)Enums.EntityStatus.Active;

    }
}
