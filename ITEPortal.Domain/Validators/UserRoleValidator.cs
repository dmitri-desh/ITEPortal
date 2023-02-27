using FluentValidation;
using ITEPortal.Domain.Dto;

namespace ITEPortal.Domain.Validators
{
    public class UserRoleValidator : AbstractValidator<UserRoleDto>
    {
        public UserRoleValidator() 
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Name is required");
        }
    }
}
