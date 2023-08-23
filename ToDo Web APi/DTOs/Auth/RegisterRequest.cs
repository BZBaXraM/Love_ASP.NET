using System.ComponentModel.DataAnnotations;

namespace ToDo_Web_APi.DTOs.Auth;

public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }

    public string UserName { get; set; } = string.Empty;
}

// public class UserNameAttribute : ValidationAttribute
// {
//     protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
//     {
//         if (value is not string userName)
//         {
//             return new ValidationResult("UserName is not a string");
//         }
//
//         if (userName.Length < 2)
//         {
//             return new ValidationResult("UserName is too short");
//         }
//
//
//         return ValidationResult.Success;
//     }
// }