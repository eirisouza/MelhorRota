using FluentValidation;
using Routes.Api.Models;
using Routes.Domain.Constants;

namespace Routes.Api.Validators;

public sealed class BetterRouterRequestModelValidator : AbstractValidator<BetterRouterRequestModel>
{
    public BetterRouterRequestModelValidator()
    {
        RuleFor(c => c.Source)
            .NotEmpty()
            .Matches(RegexConstants.RouteValid)
            .WithMessage(RoutesValidatorMessages.SourceValueInvalid)
            .NotEqual(x => x.Target)
            .WithMessage(RoutesValidatorMessages.SourceValueInvalid)
            .Length(3);

        RuleFor(c => c.Target)
            .NotEmpty()
            .Matches(RegexConstants.RouteValid)
            .WithMessage(RoutesValidatorMessages.TargetValueInvalid)
            .Length(3);
    }
}
