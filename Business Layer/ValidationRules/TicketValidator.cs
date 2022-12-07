using Entity_Layer.Concrete;
using FluentValidation;

namespace Business_Layer.ValidationRules
{
    public class TicketValidator : AbstractValidator<Ticket>
    {
        public TicketValidator()
        {
            RuleFor(x => x.Priority).NotEmpty().WithMessage("Priority cannot be empty")
                .LessThanOrEqualTo(4).WithMessage("Priortiy's maximum value is 4")
                .GreaterThanOrEqualTo(1).WithMessage("Priority's minimum value is 1");
        }
    }
}
