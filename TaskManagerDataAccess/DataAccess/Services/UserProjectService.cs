using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TaskManagerDataAccess.Models;

namespace TaskManagerDataAccess.DataAccess.Services;

public class UserProjectService
{
    private readonly TaskManagerDbContext _dbContext;

    public UserProjectService (TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserProject> AddUserProject (UserProject userProject)
    {
        ArgumentNullException.ThrowIfNull(userProject);

        _dbContext.Add(userProject);
        await _dbContext.SaveChangesAsync();

        return userProject;
    }

    public async Task<UserProject[]> GetProjectsByUserId (int id)
    {
        var userProjects = await _dbContext.UserProjects
            .Where(u => u.UserId == id)
            .Include(u => u.Project)
            .Include(u => u.User)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(userProjects);

        return userProjects.ToArray();
    }

    public async Task<UserProject[]> GetUsersByProjectId (int id)
    {
        var userProjects = await _dbContext.UserProjects
            .Where(u => u.ProjectId == id)
            .Include(u => u.Project)
            .Include(u => u.User)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(userProjects);

        return userProjects.ToArray();
    }

    public async Task<UserProject> UpdateUserProject (UserProject userProject)
    {
        ArgumentNullException.ThrowIfNull(userProject);

        _dbContext.Update(userProject);
        await _dbContext.SaveChangesAsync();

        return userProject;
    }

    public async Task DeleteUserFromProject (UserProject userProject)
    {
        var user = await _dbContext.UserProjects
            .FirstOrDefaultAsync(u => u.UserId == userProject.UserId && u.ProjectId == userProject.ProjectId);

        ArgumentNullException.ThrowIfNull(user);

        _dbContext.Remove(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAllUsers (int id)
    {
        var users = await _dbContext.UserProjects
            .Where(u => u.ProjectId == id)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(users);

        _dbContext.RemoveRange(users);
        await _dbContext.SaveChangesAsync();
    }
}