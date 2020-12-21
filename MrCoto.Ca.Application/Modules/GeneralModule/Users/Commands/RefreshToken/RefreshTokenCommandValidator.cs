using FluentValidation;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}