using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace AuthwithIdentity.Models.DTOs.RequestesDto
{
    public class ForgotPasswordDto
    {
        [Required]
        public string Email { get; set; }
    }
}
