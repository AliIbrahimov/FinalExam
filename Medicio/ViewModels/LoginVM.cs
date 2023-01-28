using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Medicio.ViewModels
{
    public class LoginVM
    {
        [Required,MinLength(4),NotNull]
        public string Username { get; set; }
        [DataType(DataType.Password),MinLength(4),NotNull]
        public string Password { get; set; }
    }
}
