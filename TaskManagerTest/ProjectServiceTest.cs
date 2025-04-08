using Microsoft.EntityFrameworkCore;
using TaskManagerDataAccess;
using TaskManagerDataAccess.DataAccess;
using TaskManagerDataAccess.DataAccess.Services;
using TaskManagerDataAccess.Models;

namespace TaskManagerTest;

public class ProjectServiceTest
{
    [Fact]
    public async Task AddProject_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        Project project = new Project("TaskManager");
        ProjectService projectService = new ProjectService(context);
        await projectService.AddProject(project);

        var projectsInDb = context.Projects.ToArray();

        Assert.Equal("TaskManager", projectsInDb[0].ProjectName);
    }

    [Fact]
    public async Task GetProject_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        Project project = new Project();
        ProjectService projectService = new ProjectService(context);
        project = await projectService.GetProject(1);
        var expected = project.ProjectStatus;

        Assert.Equal("TaskManager", project.ProjectName);
        Assert.Equal(expected, project.ProjectStatus);
    }

    [Fact]
    public async Task UpdateProject_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        Project project = new Project("TaskManager", Status.Completed);
        ProjectService projectService = new ProjectService(context);
        project.ProjectId = 1;
        project = await projectService.UpdateProject(project);

        Assert.Equal("TaskManager", project.ProjectName);
        Assert.Equal(Status.Completed, project.ProjectStatus);
    }

    [Fact]
    public async Task DeleteProject_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        Project project = new Project();
        ProjectService projectService = new ProjectService(context);
        await projectService.DeleteProject(2);

        var projectsInDb = context.Projects.ToArray();

        Assert.Equal(1, projectsInDb.Length);
    }
}