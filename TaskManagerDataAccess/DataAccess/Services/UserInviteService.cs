using Microsoft.EntityFrameworkCore;
using TaskManagerDataAccess.Models;

namespace TaskManagerDataAccess.DataAccess.Services;

public class UserInviteService
{
    private readonly TaskManagerDbContext _dbContext;

    public UserInviteService (TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserInvite> AddUserInvite (UserInvite userInvite)
    {
        ArgumentNullException.ThrowIfNull(userInvite);

        _dbContext.Add(userInvite);
        await _dbContext.SaveChangesAsync();

        return userInvite;
    }

    public async Task DeleteUserInviteById (int id)
    {
        var userInvite = await _dbContext.UserInvites.FindAsync(id);

        ArgumentNullException.ThrowIfNull(userInvite);

        _dbContext.Remove(userInvite);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUserInviteByUserId (int id)
    {
        var userInvites = await _dbContext.UserInvites
            .Where(u => u.UserId == id)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(userInvites);

        _dbContext.RemoveRange(userInvites);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUserInviteByProjectId (int id)
    {
        var userInvites = await _dbContext.UserInvites
            .Where(u => u.ProjectId == id)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(userInvites);

        _dbContext.RemoveRange(userInvites);
        await _dbContext.SaveChangesAsync();
    }
}