using ServiceContracts;

namespace Services
{
    public class UsersService : IUsersService
    {
        private int usersId;

        public UsersService()
        {
            // simple constructor
            usersId = 1;
        }

        public int usersID { get {
                return usersId;
            } }

        public string getFriendName(string name)
        {
            return $"This is my friend: {name}. Nice to meet you";
        }

        public string getMyName(string name)
        {
            return $"This is my name: {name} with id: {usersID}";
        }

        public string getFriendAvatar(string name) { 
            return $"This is my name: {name} with id: {usersID}";

        }
    }
}
