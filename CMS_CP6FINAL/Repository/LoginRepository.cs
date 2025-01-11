using CMS_CP6FINAL.Model;
using Microsoft.EntityFrameworkCore;

public class LoginRepository : ILoginRepository
{
    private readonly CmsCamp6finalContext _context;

    public LoginRepository(CmsCamp6finalContext context)
    {
        _context = context;
    }

    public async Task<UserRegistration> ValidateUser(string username, string password)
    {
        return await _context.UserRegistrations
            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
    }

    public async Task<bool> IsUsernameUnique(string username)
    {
        return await _context.UserRegistrations
            .AllAsync(u => u.Username != username);
    }

    public async Task<UserRegistration> CreateUser(int staffId, string username, string password)
    {
        var staff = await _context.Set<Staff>().FindAsync(staffId);
        if (staff == null) throw new Exception("Staff not found.");

        // Determine the RoleId based on the department
        int roleId = GetRoleIdForDepartment(staff.DepartmentId);

        var user = new UserRegistration
        {
            StaffId = staffId,
            RoleId = roleId,
            Username = username,
            Password = password
        };

        _context.UserRegistrations.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> ResetPassword(int userId, string newUsername, string newPassword)
    {
        var user = await _context.UserRegistrations.FindAsync(userId);
        if (user == null) return false;

        user.Username = newUsername;
        user.Password = newPassword;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Staff>> GetEligibleUsers()
    {
        return await _context.Set<Staff>()
            .Where(s => s.Department.DepartmentName != "Other" && !_context.UserRegistrations.Any(u => u.StaffId == s.StaffId))
            .ToListAsync();
    }

    public async Task<bool> AssignCredentials(int staffId, string username, string password)
    {
        if (!await IsUsernameUnique(username))
            throw new Exception("Username already exists.");

        var staff = await _context.Set<Staff>().FindAsync(staffId);
        if (staff == null) return false;

        int roleId = GetRoleIdForDepartment(staff.DepartmentId);

        var user = new UserRegistration
        {
            StaffId = staffId,
            RoleId = roleId,
            Username = username,
            Password = password
        };
        _context.UserRegistrations.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    private int GetRoleIdForDepartment(int departmentId)
    {
        return departmentId switch
        {
            1 => 1, // Administrator
            2=>3,//doctor
            5=>3,//doctor
            4 => 2, // Receptionist
            6 => 5, // Lab Technician
            7 => 4, // Pharmacist
            _ => throw new Exception("Invalid department.")
        };
    }
}
