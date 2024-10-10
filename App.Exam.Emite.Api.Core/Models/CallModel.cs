using App.Exam.Emite.Api.Core.Interfaces.Models;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Exam.Emite.Api.Core.Models
{
    public class CallModel : ValidatableModel, IConvertable<Call>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int? CustomerId { get; set; }

        [Required]
        public int? AgentId { get; set; }

        [Required]
        public DateTime? StartTime { get; set; }

        [Required]
        public DateTime? EndTime { get; set; }

        [Required]
        public CallStatus Status { get; set; }

        [Required]
        public string? Notes { get; set; }

        public CallModel(Call entity)
        {
            ToModel(entity);
        }

        public CallModel()
        {

        }
        public Call ToEntity(Call entity)
        {
            entity.Id = Id;
            entity.CustomerId = CustomerId;
            entity.AgentId = AgentId;
            entity.StartTime = StartTime;
            entity.EndTime = EndTime;
            entity.Status = Status;
            entity.Notes = Notes;

            return entity;
        }

        public void ToModel(Call entity)
        {
            Id = entity.Id;
            CustomerId = entity.CustomerId;
            AgentId = entity.AgentId;
            StartTime = entity.StartTime;
            EndTime = entity.EndTime;
            Status = entity.Status;
            Notes = entity.Notes;
        }
    }
}
