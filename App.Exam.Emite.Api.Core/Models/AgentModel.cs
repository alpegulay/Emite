using App.Exam.Emite.Api.Core.Interfaces.Models;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Exam.Emite.Api.Core.Models
{
    public class AgentModel : ValidatableModel, IConvertable<Agent>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? PhoneExtension { get; set; }

        [Required]
        public AgentStatus Status { get; set; }

        public AgentModel(Agent entity)
        {
            ToModel(entity);
        }

        public AgentModel()
        {

        }
        public Agent ToEntity(Agent entity)
        {
            entity.Id = Id;
            entity.Name = Name;
            entity.Email = Email;
            entity.PhoneExtension = PhoneExtension;
            entity.Status = Status;

            return entity;
        }

        public void ToModel(Agent entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Email = entity.Email;
            PhoneExtension = entity.PhoneExtension;
            Status = entity.Status;
        }
    }
}
