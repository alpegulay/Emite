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
    public class CustomerModelValidator : IValidator<CustomerModel>
    {
        private readonly DataContext _context;

        public CustomerModelValidator(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> IsValidAsync(CustomerModel model)
        {
            var isValid = CustomValidator.IsModelValid(model);

            if (string.IsNullOrEmpty(model.Name))
            {
                model.EnsureError(nameof(model.Name), ErrorMessage.IsRequired);
                isValid = false;
            }

            //Check duplicate name
            var any = await _context.Customers.AnyAsync(x => x.Id == model.Id &&
                                                         model.Name.IgnoreCaseEquals(x.Name)&&
                                                         x.EntityStatus == (int)EntityStatus.Active);

            if (any)
            {
                model.EnsureError("Name", string.Format(ErrorMessage.AlreadyExists, "Customer name"));
                isValid = false;
            }


            return isValid;
        }
    }
}
