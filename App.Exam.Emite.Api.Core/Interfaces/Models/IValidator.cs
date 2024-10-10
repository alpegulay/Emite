using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Api.Core.Interfaces.Models
{
    public interface IValidator<in TModel>
    {
        Task<bool> IsValidAsync(TModel model);
    }
}
