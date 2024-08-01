namespace medic_api.Endpoints.Auth
{
    public class AddUserRequest
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? name { get; set; }
        public int? orders { get; set; }
        public string? imageUrl { get; set; }
        public DateTime? birthDate { get; set; }
    }
}
