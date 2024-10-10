using App.Exam.Emite.Api.Core.Interfaces.Models;
using App.Exam.Emite.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Exam.Emite.Data.Entities;

namespace App.Exam.Emite.Api.Core.Models
{
    public class CustomerModel : ValidatableModel, IConvertable<Customer>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public DateTime? LastContactDate { get; set; }

        public CustomerModel(Customer entity)
        {
            ToModel(entity);
        }

        public CustomerModel()
        {

        }
        public Customer ToEntity(Customer entity)
        {
            entity.Id = Id;
            entity.Name = Name;
            entity.Email = Email;
            entity.PhoneNumber = PhoneNumber;
            entity.LastContactDate = LastContactDate;

            return entity;
        }

        public void ToModel(Customer entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Email = entity.Email;
            PhoneNumber = entity.PhoneNumber;
            LastContactDate = entity.LastContactDate;
        }
    }
}
