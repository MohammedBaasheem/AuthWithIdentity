using System.ComponentModel.DataAnnotations;

namespace AuthwithIdentity.Models.DTOs.RequestesDto
{
    public class AddUserToRoleDto
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string roleName { get; set; }
    }
}
