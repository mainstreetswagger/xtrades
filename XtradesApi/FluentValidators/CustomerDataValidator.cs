using FluentValidation;
using XtradesApi.Dtos;

namespace XtradesApi.FluentValidators
{
    public class CustomerDataValidator : AbstractValidator<CustomerData>
    {
        public CustomerDataValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.Phone).NotEmpty();
        }
    }
}
