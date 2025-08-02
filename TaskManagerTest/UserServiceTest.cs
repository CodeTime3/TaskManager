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
        User user = new User("Nicola", "Nicola@gmail.com", "nicola");
        UserService userService = new UserService(context);
        await userService.AddUser(user);

        var usersInDb = context.Users.ToArray();

        Assert.Equal("Nicola", usersInDb[1].UserName);
        Assert.Equal("Nicola@gmail.com", usersInDb[1].UserMail);
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
        user = await userService.GetUser(2);

        Assert.Equal("Nicola", user.UserName);
    }

    [Fact]
    public async Task GetUserByUserMail_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        User user = new User();
        UserService userService = new UserService(context);
        user = await userService.GetUserByUserMail("danikboosting@gmail.com");

        Assert.Equal("danikboosting@gmail.com", user.UserMail);
    }

    [Fact]
    public async Task UpdateUser_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        User user = new User("Nicola", "nicola@gmail.com", null);
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