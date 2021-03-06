namespace Api.Architecture.DomainLayer.ApiModels
{
    public class AuthenticationModel
    {
        public string Token_Type { get; set; }

        public string Access_Token { get; set; }

        public string Expires_In { get; set; }
    }
}
