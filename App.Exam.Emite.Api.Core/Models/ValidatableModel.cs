using App.Exam.Emite.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace App.Exam.Emite.Api.Core.Models
{
    public abstract class ValidatableModel
    {
        [NotMapped, XmlIgnore]
        public List<ValidationError> ValidationResult { get; set; } = new List<ValidationError>();

        public void EnsureError(string key, string message)
        {
            if (!string.IsNullOrEmpty(key))
                key = key.CamelCase();

            var validationError = ValidationResult.FirstOrDefault(x => x.Key == key);
            if (validationError == null)
            {
                validationError = new ValidationError
                {
                    Key = key,
                    Message = message
                };
                ValidationResult.Add(validationError);
            }
            else
            {
                validationError.Message = message;
            }
        }
    }
}
