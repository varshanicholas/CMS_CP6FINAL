using CMS_CP6FINAL.Model;

public interface ILoginService
{
    Task<UserRegistration> RegisterUser(int staffId, string username, string password);
    Task<bool> ResetUserPassword(int userId, string newUsername, string newPassword);
    Task<UserRegistration> AuthenticateUser(string username, string password);
    Task<IEnumerable<Staff>> FetchEligibleUsers();
     Task<IEnumerable<UserRegistration>> GetAllUsers();
    // Task<bool> ResetUserPassword(int staffId, string newUsername, string newPassword);
    Task<bool> AssignUserCredentials(int staffId, string username, string password);
}
