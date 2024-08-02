namespace medic_api.Endpoints.Users.GetDetailsById
{
    public class UpdateUserDetailsRequest
    {
        public string? name { get; set; }
        public string? username { get; set; }
        public int? orders { get; set; }
        public string? imageUrl { get; set; }
        public DateTime? birthDate { get; set; }
    }
}
