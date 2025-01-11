using CMS_CP6FINAL.Model;

public interface ILoginRepository
{
    Task<UserRegistration> ValidateUser(string username, string password);
    Task<bool> IsUsernameUnique(string username);
    Task<UserRegistration> CreateUser(int staffId, string username, string password);
    Task<bool> ResetPassword(int userId, string newUsername, string newPassword);
    Task<IEnumerable<Staff>> GetEligibleUsers();
    Task<bool> AssignCredentials(int staffId, string username, string password);
}
