using Microsoft.EntityFrameworkCore;
using TaskManagerDataAccess.Models;

namespace TaskManagerDataAccess.DataAccess.Services;

public class UserService
{
    private readonly TaskManagerDbContext _dbContext;

    public UserService (TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> AddUser (User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        _dbContext.Add(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User[]> GetAllUsers ()
    {
        var users = await _dbContext.Users
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(users);
        
        return users.ToArray();
    }

    public async Task<User> GetUsers (int id)
    {
        var user = await _dbContext.Users.FindAsync(id);

        ArgumentNullException.ThrowIfNull(user);

        return user;
    }

    public async Task<User> GetUserByUsername (string username)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.UserName.Equals(username));

        ArgumentNullException.ThrowIfNull(user);

        return user;
    } 

    public async Task<User> UpdateUser (User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task DeleteUser (int id)
    {
        var user = await _dbContext.Users.FindAsync(id);

        ArgumentNullException.ThrowIfNull(user);

        _dbContext.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}