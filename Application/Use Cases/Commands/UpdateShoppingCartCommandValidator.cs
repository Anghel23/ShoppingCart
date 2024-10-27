using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Cases.Commands
{
    public class UpdateShoppingCartCommandValidator : AbstractValidator<UpdateShoppingCartCommand>
    {
        public UpdateShoppingCartCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Shopping cart Id must not be empty.");

            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId must not be empty.");

            RuleFor(x => x.TotalPrice)
                .GreaterThanOrEqualTo(0).WithMessage("TotalPrice must be greater than or equal to zero.");

            RuleFor(x => x.IsEmpty).NotNull().WithMessage("IsEmpty must not be null.");
        }
    }
}
