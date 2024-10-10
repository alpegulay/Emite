using App.Exam.Emite.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Data.Entities
{
    public class CallSearchEntity
    {
        public CallStatus? Status { get; set; }

        public DateTime? StartTime { get; set; } = DateTime.UtcNow;

        public DateTime? EndTime { get; set; } = DateTime.UtcNow;

        public string? Email { get; set; } = string.Empty;
    }
}
