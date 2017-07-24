using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarterBuddy.Resources.CommonResource;
using FluentValidation;

namespace BarterBuddy.Model.ModalValidator
{
    public class UserRegisterModelValidator : AbstractValidator<UserRegisterModel>
    {

        private const string required = "Required";
        private const bool isTrue = true;
        public UserRegisterModelValidator()
        {
            RuleFor(x => x.EmailId).NotNull().WithMessage("*Required")
              .EmailAddress().WithMessage(CommonResource.InvalidEmailAddress);


            RuleFor(x => x.Password)
              .NotNull()
              .WithMessage("*Required")
              .Length(6, 10);

            RuleFor(x => x.MobileNo)
                .Length(10)
                .WithMessage(CommonResource.InvalidMobileNo);

            RuleFor(x => x.isTerms)
              .Equal(true)
              .WithMessage(CommonResource.InvalidMobileNo);
        }
    }
}
