using Microsoft.EntityFrameworkCore;
using TaskManagerDataAccess;
using TaskManagerDataAccess.DataAccess;
using TaskManagerDataAccess.DataAccess.Services;
using TaskManagerDataAccess.Models;

namespace TaskManagerTest;

public class UserProjectServiceTest
{
    [Fact]
    public async Task AddUserProject_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        UserProject userProject = new UserProject(3, 6);
        UserProjectService userProjectService = new UserProjectService(context);
        await userProjectService.AddUserProject(userProject);

        var userProjectInDb = context.UserProjects.ToArray();

        Assert.Equal(3, userProjectInDb[1].ProjectId);
        Assert.Equal(6, userProjectInDb[1].UserId);     
        Assert.Equal(Role.Administrator, userProjectInDb[0].Role);   
    }

    [Fact]
    public async Task GetProjectsByUserId_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        UserProject userProject = new UserProject();
        UserProjectService userProjectService = new UserProjectService(context);
        await userProjectService.GetProjectsByUserId(1);

        var userProjectInDb = context.UserProjects.ToArray();

        Assert.Equal(1, userProjectInDb.Length);
    }

    [Fact]
    public async Task GetUsersByProjectId_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        UserProject userProject = new UserProject();
        UserProjectService userProjectService = new UserProjectService(context);
        await userProjectService.GetUsersByProjectId(3);

        var userProjectInDb = context.UserProjects.ToArray();

        Assert.Equal(1, userProjectInDb.Length);
    }

    [Fact]
    public async Task UpdateUserProject_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        UserProject userProject = new UserProject(3, 6, Role.Collaborator);
        UserProjectService userProjectService = new UserProjectService(context);
        userProject = await userProjectService.UpdateUserProject(userProject);

        Assert.Equal(Role.Collaborator, userProject.Role);
    }

    [Fact]
    public async Task DeleteUserFromProject_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        UserProject userProject = new UserProject(3, 6);
        UserProjectService userProjectService = new UserProjectService(context);
        await userProjectService.DeleteUserFromProject(userProject);

        var userProjectInDb = context.UserProjects.ToArray();

        Assert.Equal(1, userProjectInDb.Length);
    }

    [Fact]
    public async Task DeleteAllUsers_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        UserProject userProject = new UserProject();
        UserProjectService userProjectService = new UserProjectService(context);
        await userProjectService.DeleteAllUsers(3);

        var userProjectInDb = context.UserProjects.ToArray();

        Assert.Equal(0, userProjectInDb.Length);
    }
}