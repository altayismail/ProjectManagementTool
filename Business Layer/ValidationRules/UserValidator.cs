using Entity_Layer.Concrete;
using FluentValidation;

namespace Business_Layer.ValidationRules
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Firstname).NotEmpty().WithMessage("Firstname cannot be empty.").
                MinimumLength(2).WithMessage("Firstname should has at least 2 char.").
                MaximumLength(30).WithMessage("Firstname should has maximum 30 char.");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Lastname cannot be empty.").
                MinimumLength(2).WithMessage("Lastname should has at least 2 char.").
                MaximumLength(30).WithMessage("Lastname should has maximum 30 char.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("User email cannot be empty.").
                Matches("@").WithMessage("Email should include '@' symbol.").
                MaximumLength(50).WithMessage("Email should has maximum 50 char.").
                MinimumLength(10).WithMessage("Email should has minimum 10 char.");

            RuleFor(x => x.Title).NotEmpty().WithMessage("User title cannot be empty.");
        }
    }
}
