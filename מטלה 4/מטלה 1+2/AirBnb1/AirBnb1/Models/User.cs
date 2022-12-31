namespace AirBnb1.Models
{
    public class User
    {
        public static List<User> UsersList=new List<User>();

        private string firstName;
        private string lastName;
        private string password;
        private string email;

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }

        public List<User> Read(string email, string password)
        {
            DBservices dbs=new DBservices();
            return dbs.ReadUser(email, password);
        }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertUser(this);
        }

        public int Update()
        {
            DBservices dbs = new DBservices();
            return dbs.UpdateUser(this);
        }

    }
}
