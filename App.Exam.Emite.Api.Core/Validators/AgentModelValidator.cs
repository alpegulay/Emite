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
    public class AgentModelValidator : IValidator<AgentModel>
    {
        private readonly DataContext _context;

        public AgentModelValidator(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> IsValidAsync(AgentModel model)
        {
            var isValid = CustomValidator.IsModelValid(model);

            if (string.IsNullOrEmpty(model.Name))
            {
                model.EnsureError(nameof(model.Name), ErrorMessage.IsRequired);
                isValid = false;
            }

            //Check duplicate name
            var any = await _context.Agents.AnyAsync(x => x.Id == model.Id &&
                                                         model.Name.IgnoreCaseEquals(x.Name)&&
                                                         x.EntityStatus == (int)EntityStatus.Active);

            if (any)
            {
                model.EnsureError("Name", string.Format(ErrorMessage.AlreadyExists, "Agent name"));
                isValid = false;
            }


            return isValid;
        }
    }
}
