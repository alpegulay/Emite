using App.Exam.Emite.Api.Core.Interfaces.Models;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Exam.Emite.Api.Core.Models
{
    public class TicketModel : ValidatableModel, IConvertable<Ticket>
    {
        public int Id { get; set; }

        [Required]
        public int? CustomerId { get; set; }

        [Required]
        public int? AgentId { get; set; }

        [Required]
        public TicketStatus Status { get; set; }

        [Required]
        public TicketPriority Priority { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Resolution { get; set; }

        public TicketModel(Ticket entity)
        {
            ToModel(entity);
        }

        public TicketModel()
        {

        }
        public Ticket ToEntity(Ticket entity)
        {
            entity.Id = Id;
            entity.CustomerId = CustomerId;
            entity.AgentId = AgentId;
            entity.Status = Status;
            entity.Priority = Priority;
            entity.Description = Description;
            entity.Resolution = Resolution;

            return entity;
        }

        public void ToModel(Ticket entity)
        {
            Id = entity.Id;
            CustomerId = entity.CustomerId;
            AgentId = entity.AgentId;
            Status = entity.Status;
            Priority = entity.Priority;
            Description = entity.Description;
            Resolution = entity.Resolution;
        }
    }
}
