using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JiraClone.utils.validators
{
    public class TicketTypeRule: ValidationRule
    {
        private RequiredRule requiredRule = new RequiredRule();
        private readonly string[] TYPES = { "FEATURE", "BUG" };

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var requiredResult = requiredRule.Validate(value, cultureInfo);
            if (requiredResult != ValidationResult.ValidResult) return requiredResult;

            if (!TYPES.Contains((string)value))
                return new ValidationResult(false, "Niepoprawny typ");

            return ValidationResult.ValidResult;
        }
    }
}
