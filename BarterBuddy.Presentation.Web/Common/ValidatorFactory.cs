using System;
using System.Linq;
using BarterBuddy.Model;
using BarterBuddy.Model.ModalValidator;
using FluentValidation;
using Ninject;

namespace BarterBuddy.Presentation.Web.Common
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectValidatorFactory"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public ValidatorFactory(IKernel kernel)
        {
            this.kernel = kernel;
            AddBinding();
        }

        public void AddBinding()
        {
            kernel.Bind<IValidator<UserModel>>().To<UserModelValidator>();
        }

        /// <summary>
        /// Creates an instance of a validator with the given type using ninject.
        /// </summary>
        /// <param name="validatorType">Type of the validator.</param>
        /// <returns>The newly created validator</returns>
        public override IValidator CreateInstance(Type validatorType)
        {
            if (!kernel.GetBindings(validatorType).Any())
            {
                return null;
            }
            return kernel.Get(validatorType) as IValidator;
        }
    }
}
