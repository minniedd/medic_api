namespace medic_api.Endpoints.Users.GetDetailsById
{
    public class GetDetailsByIdResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public int orders { get; set; }
        public string lastLoginDate { get; set; }
        public string imageUrl { get; set; }
        public string status { get; set; }
        public string birthDate { get; set; }
    }
}
