using Microsoft.EntityFrameworkCore;
using TaskManagerDataAccess.DataAccess;
using TaskManagerDataAccess.DataAccess.Services;
using TaskManagerDataAccess.Models;

namespace TaskManagerTest;

public class UserServiceTest
{   
    [Fact]
    public async Task AddUser_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        User user = new User("Nicola", "nicola@gmail.com", "nicola");
        UserService userService = new UserService(context);
        await userService.AddUser(user);

        var usersInDb = context.Users.ToArray();

        Assert.Equal("Nicola", usersInDb[1].UserName);
        Assert.Equal("nicola@gmail.com", usersInDb[1].UserMail);
        Assert.Equal("nicola", usersInDb[1].UserHash);
    }

    [Fact]
    public async Task GetAllUsers_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        User user = new User();
        UserService userService = new UserService(context);
        await userService.GetAllUsers();

        var usersInDb = context.Users.ToArray();

        Assert.Equal(2, usersInDb.Length);
    }

    [Fact]
    public async Task GetUsers_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        User user = new User();
        UserService userService = new UserService(context);
        user = await userService.GetUsers(2);

        Assert.Equal("Nicola", user.UserName);
    }

    [Fact]
    public async Task GetUserByUsername_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        User user = new User();
        UserService userService = new UserService(context);
        user = await userService.GetUserByUsername("Daniele");

        Assert.Equal("Daniele", user.UserName);
    }

    [Fact]
    public async Task UpdateUser_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        User user = new User("Nicola", "nicola@gmail.com", "nicola");
        UserService userService = new UserService(context);
        user.UserId = 2;
        user = await userService.UpdateUser(user);

        Assert.Equal("Nicola", user.UserName);
    }

    [Fact]
    public async Task DeleteUser_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        User user = new User();
        UserService userService = new UserService(context);
        await userService.DeleteUser(5);

        var usersInDb = context.Users.ToArray();

        Assert.Equal(1, usersInDb.Length);
    }
}