using CMS_CP6FINAL.Model;

public class LoginService : ILoginService
{
    private readonly ILoginRepository _loginRepository;

    public LoginService(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    public async Task<UserRegistration> RegisterUser(int staffId, string username, string password)
    {
        if (!await _loginRepository.IsUsernameUnique(username))
            throw new Exception("Username already exists.");

        return await _loginRepository.CreateUser(staffId, username, password);
    }

    public async Task<bool> ResetUserPassword(int userId, string newUsername, string newPassword)
    {
        if (!await _loginRepository.IsUsernameUnique(newUsername))
            throw new Exception("Username already exists.");

        return await _loginRepository.ResetPassword(userId, newUsername, newPassword);
    }

    public async Task<UserRegistration> AuthenticateUser(string username, string password)
    {
        return await _loginRepository.ValidateUser(username, password);
    }

    public async Task<IEnumerable<Staff>> FetchEligibleUsers()
    {
        return await _loginRepository.GetEligibleUsers();
    }

    public async Task<bool> AssignUserCredentials(int staffId, string username, string password)
    {
        return await _loginRepository.AssignCredentials(staffId, username, password);
    }
}
