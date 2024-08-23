using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TenexCars.Models.ViewModels
{
	public class SetNewPasswordViewModel
	{
		[Required]
		public string UserId { get; set; }

		[Required]
		public string Token { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "New Password")]
		[CustomPasswordValidation]
		public string NewPassword { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm New Password")]
		[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}

	public class CustomPasswordValidation : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var password = value as string;

			if (string.IsNullOrEmpty(password))
			{
				return new ValidationResult("Password is required.");
			}

			if (password.Length <= 6)
			{
				return new ValidationResult("Password must be longer than 6 characters.");
			}

			if (!Regex.IsMatch(password, @"[0-9]"))
			{
				return new ValidationResult("Password must contain at least one number.");
			}

			if (!Regex.IsMatch(password, @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$"))

			{
				return new ValidationResult("Password must contain at least one special character.");
			}

			return ValidationResult.Success;
		}
	}
}


/*using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TenexCars.Models.ViewModels
{
    public class SetNewPasswordViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessage = "The password must be at least 6 characters long, contain at least one number, and one special character.")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}*/
