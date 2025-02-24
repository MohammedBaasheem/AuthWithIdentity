using System.ComponentModel.DataAnnotations;

namespace AuthwithIdentity.Models.DTOs.RequestesDto
{
    public class RegisterDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(100)]
        public string? Email { get; set; }
        [Required, StringLength(100)]
        public string? Password { get; set; }
    }
}
