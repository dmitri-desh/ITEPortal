using FluentValidation;
using ITEPortal.Domain.Dto;

namespace ITEPortal.Domain.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required")
                .EmailAddress().WithMessage("Your Email address is not valid"); ;
        }
    }
}
