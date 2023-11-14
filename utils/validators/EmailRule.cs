using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JiraClone.utils.validators
{
	public class EmailRule : ValidationRule
	{
		private RequiredRule requiredRule = new RequiredRule();

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			var requiredResult = requiredRule.Validate(value, cultureInfo);
			if (requiredResult != ValidationResult.ValidResult) return requiredResult;

			string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
			Regex regex = new Regex(pattern);

			if (!regex.IsMatch((string)value)) return new ValidationResult(false, "Niepoprawny email");

			return ValidationResult.ValidResult;
		}
	}
}
