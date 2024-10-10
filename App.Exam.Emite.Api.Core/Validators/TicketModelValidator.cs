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
    public class TicketModelValidator : IValidator<TicketModel>
    {
        private readonly DataContext _context;

        public TicketModelValidator(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> IsValidAsync(TicketModel model)
        {
            var isValid = CustomValidator.IsModelValid(model);

            ////Check duplicate name
            //var any = await _context.Tickets.AnyAsync(x => x.Id == model.Id &&
            //                                             model.CustomerId == x.CustomerId &&
            //                                             x.EntityStatus == (int)EntityStatus.Active);

            //if (any)
            //{
            //    model.EnsureError("CustomerId", string.Format(ErrorMessage.AlreadyExists, "CustomerId"));
            //    isValid = false;
            //}


            return isValid;
        }
    }
}
