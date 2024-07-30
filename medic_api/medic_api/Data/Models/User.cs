namespace medic_api.Data.Models
{
    public class User
    {
        public int id { get; set; }
        public string? username { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public string? name { get; set; }
        public int? orders { get; set; }
        public string? imageUrl { get; set; }
        public DateTime? birthDate { get; set; }
        public string refreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        public DateTime? lastLoginDate { get; set; }
        public bool isAdmin { get; set; }
        public bool isBlocked { get; set; }
        public string? status {  get; set; }
    }
}
