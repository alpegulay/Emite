using App.Exam.Emite.Api.Core.Helpers;
using App.Exam.Emite.Api.Core.Interfaces.Models;
using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Data;
using App.Exam.Emite.Data.Constants;
using App.Exam.Emite.Data.Entities.Enums;
using App.Exam.Emite.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace App.Exam.Emite.Api.Core.Validators
{
    public class CallModelValidator : IValidator<CallModel>
    {
        private readonly DataContext _context;

        public CallModelValidator(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> IsValidAsync(CallModel model)
        {
            var isValid = CustomValidator.IsModelValid(model);

            return isValid;
        }
    }
}
