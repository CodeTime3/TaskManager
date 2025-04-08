using Microsoft.EntityFrameworkCore;
using TaskManagerDataAccess.Models;

namespace TaskManagerDataAccess.DataAccess.Services;

public class ProjectTaskService
{
    private readonly TaskManagerDbContext _dbContext;

    public ProjectTaskService (TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProjectTask> AddTask (ProjectTask task)
    {
        ArgumentNullException.ThrowIfNull(task);

        _dbContext.Add(task);
        await _dbContext.SaveChangesAsync();

        return task;
    }

    public async Task<ProjectTask[]> GetTaskByProjectId (int id)
    {
        var tasks = await _dbContext.ProjectTasks
            .Where(t => t.ProjectId == id)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(tasks);

        return tasks.ToArray();
    }

    public async Task<ProjectTask> UpdateTask (ProjectTask task)
    {
        ArgumentNullException.ThrowIfNull(task);

        _dbContext.Update(task);
        await _dbContext.SaveChangesAsync();

        return task;
    }

    public async Task DeleteTaskByTaskId (int id)
    {
        var task = await _dbContext.ProjectTasks.FindAsync(id);

        ArgumentNullException.ThrowIfNull(task);

        _dbContext.Remove(task);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAllTasks (int id)
    {
        var tasks = await _dbContext.ProjectTasks
            .Where(t => t.ProjectId == id)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(tasks);

        _dbContext.RemoveRange(tasks);
        await _dbContext.SaveChangesAsync();
    }
}