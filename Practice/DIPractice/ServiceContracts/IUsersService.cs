namespace ServiceContracts
{
    public interface IUsersService
    {
        int usersID { get; }
        string getMyName(string name);
        string getFriendName(string name);
    }
}
