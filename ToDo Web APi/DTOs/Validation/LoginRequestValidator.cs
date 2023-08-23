using FluentValidation;
using ToDo_Web_APi.DTOs.Auth;

namespace ToDo_Web_APi.DTOs.Validation;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        // RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.Password).Password().NotEmpty();
    }
}