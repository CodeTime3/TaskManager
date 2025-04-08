using Microsoft.EntityFrameworkCore;
using TaskManagerDataAccess;
using TaskManagerDataAccess.DataAccess;
using TaskManagerDataAccess.DataAccess.Services;
using TaskManagerDataAccess.Models;

namespace TaskManagerTest;

public class ProjectTaskServiceTest
{
    [Fact]
    public async Task AddTask_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        ProjectTask projectTask = new ProjectTask(3, "Fare crud user", "Completare i metodi per il crud dell'entity user");
        ProjectTaskService projectTaskService = new ProjectTaskService(context);
        await projectTaskService.AddTask(projectTask);

        var projectTasksInDb = context.ProjectTasks.ToArray();

        Assert.Equal("Fare crud user", projectTasksInDb[0].TaskTitle);
        Assert.Equal("Completare i metodi per il crud dell'entity user", projectTasksInDb[0].TaskText);
    }

    [Fact]
    public async Task GetTaskByProjectId_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        ProjectTask projectTask = new ProjectTask();
        ProjectTaskService projectTaskService = new ProjectTaskService(context);
        await projectTaskService.GetTaskByProjectId(3);

        var projectTasksInDb = context.ProjectTasks.ToArray();

        Assert.Equal(3, projectTasksInDb.Length);
    }

    [Fact]
    public async Task UpdateTask_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        ProjectTask projectTask = new ProjectTask(3, "Fare crud project", "Completare i metodi per il crud dell'entity project", Status.Completed);
        ProjectTaskService projectTaskService = new ProjectTaskService(context);
        projectTask.ProjectTaskId = 4;
        projectTask = await projectTaskService.UpdateTask(projectTask);

        Assert.Equal("Fare crud project", projectTask.TaskTitle);
        Assert.Equal("Completare i metodi per il crud dell'entity project", projectTask.TaskText);
        Assert.Equal(Status.Completed, projectTask.TaskStatus);
    }

    [Fact]
    public async Task DeleteTaskByTaskId_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        ProjectTask projectTask = new ProjectTask();
        ProjectTaskService projectTaskService = new ProjectTaskService(context);
        await projectTaskService.DeleteTaskByTaskId(6);

        var projectTasksInDb = context.ProjectTasks.ToArray();

        Assert.Equal(0, projectTasksInDb.Length);
    }

    [Fact]
    public async Task DeleteAllTasks_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        ProjectTask projectTask = new ProjectTask();
        ProjectTaskService projectTaskService = new ProjectTaskService(context);
        await projectTaskService.DeleteAllTasks(3);

        var projectTasksInDb = context.ProjectTasks.ToArray();

        Assert.Equal(0, projectTasksInDb.Length);
    }
}