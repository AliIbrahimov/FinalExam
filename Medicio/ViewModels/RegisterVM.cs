using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Medicio.ViewModels
{
    public class RegisterVM
    {
        public string Fullname { get; set; }
        [Required,NotNull]
        public string Username { get; set; }
        [EmailAddress,Required,NotNull]
        public string Email { get; set; }
        [DataType(DataType.Password),MinLength(4),NotNull]
        public string Password { get; set; }
        [DataType(DataType.Password), MinLength(4),Compare(nameof(Password)),NotNull]
        public string ConfirmPassword { get; set; }
    }
}
