using CMS_CP6FINAL.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        var eligibleDepartments = new List<string>
        {
            "Cardiology", "Oncology", "Neurology", "Pediatrics",
            "Orthopedics", "Gastroenterology", "Dermatology"
        };

        return await _context.Set<Staff>()
            .Where(s => eligibleDepartments.Contains(s.Department.DepartmentName) 
                        && !_context.UserRegistrations.Any(u => u.StaffId == s.StaffId))
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
            2 => 3, // Doctor
            4 => 2, // Receptionist
            5 => 3, // Another Doctor
            6 => 5, // Lab Technician
            7 => 4, // Pharmacist
            8 => 3, // Other Doctors
            9 => 3,
            10 => 3,
            11 => 3,
            12 => 3,
            _ => throw new Exception("Invalid department.")
        };
    }

    // Method to fetch Doctor details by StaffId
    public async Task<Doctor> GetDoctorByStaffId(int staffId)
    {
        return await _context.Doctors.FirstOrDefaultAsync(d => d.StaffId == staffId);
    }
}


//old without doctor

//using CMS_CP6FINAL.Model;
//using Microsoft.EntityFrameworkCore;

//public class LoginRepository : ILoginRepository
//{
//    private readonly CmsCamp6finalContext _context;

//    public LoginRepository(CmsCamp6finalContext context)
//    {
//        _context = context;
//    }

//    public async Task<UserRegistration> ValidateUser(string username, string password)
//    {
//        return await _context.UserRegistrations
//            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
//    }

//    public async Task<bool> IsUsernameUnique(string username)
//    {
//        return await _context.UserRegistrations
//            .AllAsync(u => u.Username != username);
//    }

//    public async Task<UserRegistration> CreateUser(int staffId, string username, string password)
//    {
//        var staff = await _context.Set<Staff>().FindAsync(staffId);
//        if (staff == null) throw new Exception("Staff not found.");

//        // Determine the RoleId based on the department
//        int roleId = GetRoleIdForDepartment(staff.DepartmentId);

//        var user = new UserRegistration
//        {
//            StaffId = staffId,
//            RoleId = roleId,
//            Username = username,
//            Password = password
//        };

//        _context.UserRegistrations.Add(user);
//        await _context.SaveChangesAsync();
//        return user;
//    }

//    public async Task<bool> ResetPassword(int userId, string newUsername, string newPassword)
//    {
//        var user = await _context.UserRegistrations.FindAsync(userId);
//        if (user == null) return false;

//        user.Username = newUsername;
//        user.Password = newPassword;
//        await _context.SaveChangesAsync();
//        return true;
//    }

//    public async Task<IEnumerable<Staff>> GetEligibleUsers()
//    {
//        return await _context.Set<Staff>()
//            .Where(s => s.Department.DepartmentName != "Other" && !_context.UserRegistrations.Any(u => u.StaffId == s.StaffId))
//            .ToListAsync();
//    }

//    public async Task<bool> AssignCredentials(int staffId, string username, string password)
//    {
//        if (!await IsUsernameUnique(username))
//            throw new Exception("Username already exists.");

//        var staff = await _context.Set<Staff>().FindAsync(staffId);
//        if (staff == null) return false;

//        int roleId = GetRoleIdForDepartment(staff.DepartmentId);

//        var user = new UserRegistration
//        {
//            StaffId = staffId,
//            RoleId = roleId,
//            Username = username,
//            Password = password
//        };
//        _context.UserRegistrations.Add(user);
//        await _context.SaveChangesAsync();
//        return true;
//    }

//    private int GetRoleIdForDepartment(int departmentId)
//    {
//        return departmentId switch
//        {
//            1 => 1, // Administrator
//            2=>3,//doctor
//            5=>3,//doctor
//            4 => 2, // Receptionist
//            6 => 5, // Lab Technician
//            7 => 4, // Pharmacist
//            8=>3,
//            9=>3,
//            10=>3,
//            11=>3,
//            12=>3,

//            _ => throw new Exception("Invalid department.")
//        };
//    }
//}
