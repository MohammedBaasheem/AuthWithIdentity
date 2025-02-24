using System.ComponentModel.DataAnnotations;

namespace AuthwithIdentity.Models.DTOs.RequestesDto
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
