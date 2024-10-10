using App.Exam.Emite.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Exam.Emite.Api.Core.Interfaces.Models;
using App.Exam.Emite.Data.Entities;
using System.Xml.Linq;

namespace App.Exam.Emite.Api.Core.Models
{
    public class CallSearchModel: IConvertable<CallSearchEntity>
    {
        public CallStatus? Status { get; set; }

        public DateTime? StartTime { get; set; } = DateTime.UtcNow;

        public DateTime? EndTime { get; set; } = DateTime.UtcNow;

        public string? Email { get; set; } = string.Empty;

        public CallSearchEntity ToEntity(CallSearchEntity entity)
        {
            entity.Email = Email;
            entity.EndTime = EndTime;
            entity.StartTime = StartTime;
            entity.Status = Status;

            return entity;
        }

        public void ToModel(CallSearchEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
