using App.Exam.Emite.Api.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Api.Core.Helpers
{
    public sealed class CustomValidator
    {
        public static bool IsModelValid<T>(T obj) where T : ValidatableModel
        {
            var context = new ValidationContext(obj, null, null);
            var result = new List<ValidationResult>();

            obj.ValidationResult = new List<ValidationError>();

            if (!Validator.TryValidateObject(
                obj,
                context,
                result,
                true))
            {
                foreach (var entry in result)
                {
                    var key = $"{entry.MemberNames.ElementAt(0)}";

                    obj.EnsureError(key, entry.ErrorMessage);
                }

                return false;
            }

            return true;
        }
    }
}
