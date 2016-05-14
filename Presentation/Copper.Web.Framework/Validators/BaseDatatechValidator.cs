using FluentValidation;

namespace Copper.Web.Framework.Validators
{
    public partial class BaseCopperValidator<T> : AbstractValidator<T> where T : class
    {
        protected decimal CopperMaxNumber
        {
            get { return 999999.99M; }
        }

        protected int CopperMaxInt
        {
            get { return 999999; }
        }

    }
}
