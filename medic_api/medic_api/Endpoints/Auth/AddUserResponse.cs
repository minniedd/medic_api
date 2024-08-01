namespace medic_api.Endpoints.Auth
{
    public class AddUserResponse
    {
        public string? message { get; set; }
        public string? token { get; set; }
        public string? refreshToken { get; set; }
    }
}
