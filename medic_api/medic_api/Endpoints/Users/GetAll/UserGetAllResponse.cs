namespace medic_api.Endpoints.Users.GetAll
{

    public class UserGetAllResponse
    {
        public List<UserGetAllResponseUser> Users { get; set; }
    }

    public class UserGetAllResponseUser
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string lastLoginDate { get; set; }
    }
}
