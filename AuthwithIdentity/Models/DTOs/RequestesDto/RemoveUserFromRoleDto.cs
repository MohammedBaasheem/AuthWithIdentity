using System.ComponentModel.DataAnnotations;

namespace AuthwithIdentity.Models.DTOs.RequestesDto
{
    public class RemoveUserFromRoleDto
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string roleName { get; set; }
    }
}
