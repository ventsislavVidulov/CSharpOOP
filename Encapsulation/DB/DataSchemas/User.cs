namespace Encapsulation.DB.DataSchemas
{
    internal class User
    {
        //TODO : Add security for user name and password
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "user";

        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
