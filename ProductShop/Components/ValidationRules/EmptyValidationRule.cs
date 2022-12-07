using System.Globalization;
using System.Windows.Controls;

namespace ProductShop.Components.ValidationRules
{
    public class EmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value as string;

            if (string.IsNullOrEmpty(text))
                return new ValidationResult(false, "Поле не может быть пустым");

            return new ValidationResult(true, null);
        }
    }
}
