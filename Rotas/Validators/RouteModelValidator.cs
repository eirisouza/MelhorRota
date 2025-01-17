using FluentValidation;
using Routes.Api.Models;
using Routes.Domain.Constants;

namespace Routes.Api.Validators;

public sealed class RouteModelValidator : AbstractValidator<RouteModel>
{
    public RouteModelValidator()
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

        RuleFor(c => c.Value)
            .NotEmpty()
            .NotEqual(0);
    }
}
