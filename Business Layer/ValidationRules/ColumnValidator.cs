using Entity_Layer.Concrete;
using FluentValidation;

namespace Business_Layer.ValidationRules
{
    public class ColumnValidator : AbstractValidator<Column>
    {
        public ColumnValidator() 
        {

            RuleFor(x => x.ColumnName).NotEmpty().WithMessage("Column name cannot be empty!");

        }
    }
}
