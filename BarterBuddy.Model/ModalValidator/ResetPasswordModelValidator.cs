using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarterBuddy.Resources.CommonResource;
using FluentValidation;

namespace BarterBuddy.Model.ModalValidator
{
    public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordUser>
    {
        public ResetPasswordModelValidator()
        {
            RuleFor(x => x.userName).NotNull().WithMessage("*Required")
                          .EmailAddress().WithMessage(CommonResource.InvalidEmailAddress);
        }
    }
}
