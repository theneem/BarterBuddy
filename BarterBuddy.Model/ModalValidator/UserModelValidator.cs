using BarterBuddy.Resources.CommonResource;
using FluentValidation;


namespace BarterBuddy.Model.ModalValidator
{
    public class UserModelValidator : AbstractValidator<UserModel>
    {
        private const string required = "Required";

        public UserModelValidator()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage("*Required")
               .EmailAddress().WithMessage(CommonResource.InvalidEmailAddress);


            RuleFor(x => x.Password)
              .NotNull()
              .WithMessage("*Required")
              .Length(6, 10);

            RuleFor(x=>x.MobileNo)
                .Length(10)
                .WithMessage(CommonResource.InvalidMobileNo);
        }
    }
}
