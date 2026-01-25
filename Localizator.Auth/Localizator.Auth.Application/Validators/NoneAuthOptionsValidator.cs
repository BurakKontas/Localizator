using FluentValidation;
using Localizator.Auth.Domain.Configuration;

namespace Localizator.Auth.Application.Validators;

public sealed class NoneAuthOptionsValidator : AbstractValidator<NoneAuthOptions>
{
    public NoneAuthOptionsValidator()
    {
        RuleFor(x => x.Mode)
            .NotEmpty();

    }
}

