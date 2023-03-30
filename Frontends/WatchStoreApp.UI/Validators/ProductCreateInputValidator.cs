using FluentValidation;

namespace WatchStoreApp.UI.Validators
{
    public class ProductCreateInputValidator : AbstractValidator<ProductCreateInput>
    {
        public ProductCreateInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Məhsulun adı boş ola bilməz");
            RuleFor(x => x.Brend).NotEmpty().WithMessage("Məhsulun adı boş ola bilməz");
        }
    }
}
