using FluentValidation;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.DisableUser
{
    public class DisableUserCommandValidator : AbstractValidator<DisableUserCommand>
    {
        public DisableUserCommandValidator()
        {
            RuleFor(x => x.DisablementTypeId)
                .GreaterThanOrEqualTo(1);
            
            RuleFor(x => x.Observation)
                .NotNull()
                .MaximumLength(250);
        }
    }
}