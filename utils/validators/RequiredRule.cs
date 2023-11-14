using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JiraClone.utils.validators
{
	public class RequiredRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if(value == null || (string)value == "") return new ValidationResult(false, "Proszę podać wartość");
			return ValidationResult.ValidResult;
		}
	}
}
