using FluentValidation;
using ITEPortal.Domain.Dto;

namespace ITEPortal.Domain.Validators
{
    public class AuthCodeValidator : AbstractValidator<AuthCodeDto>
    {
        public AuthCodeValidator() 
        {
            RuleFor(x => x.CodeNumber)
                .NotEmpty().WithMessage("Code is required");
        }
    }
}
