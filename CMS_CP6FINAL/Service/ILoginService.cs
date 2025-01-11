using CMS_CP6FINAL.Model;

public interface ILoginService
{
    Task<UserRegistration> RegisterUser(int staffId, string username, string password);
    Task<bool> ResetUserPassword(int userId, string newUsername, string newPassword);
    Task<UserRegistration> AuthenticateUser(string username, string password);
    Task<IEnumerable<Staff>> FetchEligibleUsers();
    Task<bool> AssignUserCredentials(int staffId, string username, string password);
}
