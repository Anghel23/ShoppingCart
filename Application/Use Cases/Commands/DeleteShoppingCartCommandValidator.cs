using FluentValidation;

namespace Application.Use_Cases.Commands
{
    public class DeleteShoppingCartCommandValidator : AbstractValidator<DeleteShoppingCartCommand>
    {
        public DeleteShoppingCartCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("ShoppingCart ID is required for deletion.");
        }
    }
}
