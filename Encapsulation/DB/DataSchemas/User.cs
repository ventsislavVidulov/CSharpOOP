namespace Encapsulation.DB.DataSchemas
{
    internal class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "user";

        public List<int> Reservations { get; set; } = new();

        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
            Reservations.Add(2);
        }
    }
}
