namespace AuthwithIdentity.JwtOptions
{
    public class JWT
    {

        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audiense { get; set; }
        public int DurationInDays { get; set; }


    }
}
