namespace AuthwithIdentity.Models.DTOs.ResponsesDto
{
    public class AuthReponseDto
    {
        public string Message { get; set; }
        public bool IsAuthentcated { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }        
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirasOn { get; set; }
    }
}
