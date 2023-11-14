using FluentValidation;
using XtradesApi.Dtos;

namespace XtradesApi.FluentValidators
{
    public class CreateCustomerValidator : AbstractValidator<UpsertCustomer>
    {
        public CreateCustomerValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.Phone).NotEmpty();
        }
    }
}
