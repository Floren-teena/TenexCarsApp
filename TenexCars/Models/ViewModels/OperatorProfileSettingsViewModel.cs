using System.ComponentModel.DataAnnotations;
namespace TenexCars.Models.ViewModels
{
    public class OperatorProfileSettingsViewModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
