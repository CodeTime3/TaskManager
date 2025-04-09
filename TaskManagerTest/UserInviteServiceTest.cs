using Microsoft.EntityFrameworkCore;
using TaskManagerDataAccess.DataAccess;
using TaskManagerDataAccess.DataAccess.Services;
using TaskManagerDataAccess.Models;

namespace TaskManagerTest;

public class UserInviteServiceTest
{   
    [Fact]
    public async Task AddUserInvite_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        DateTime dateTime = new DateTime();
        UserInvite userInvite = new UserInvite(6, 3, dateTime.Date, "hh", "hh", "hh");
        UserInviteService userInviteService = new UserInviteService(context);
        await userInviteService.AddUserInvite(userInvite);

        var userInvieteInDb = context.UserInvites.ToArray();

        Assert.Equal(6, userInvieteInDb[2].UserId);
        Assert.Equal(3, userInvieteInDb[2].ProjectId);     
        Assert.Equal(dateTime.Date, userInvieteInDb[2].ExpirationDate); 
        Assert.Equal("hh", userInvieteInDb[2].InviteMailObject);
        Assert.Equal("hh", userInvieteInDb[2].InviteMailText);     
        Assert.Equal("hh", userInvieteInDb[2].InviteLink);
    }

    [Fact]
    public async Task DeleteUserInviteById()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        UserInvite userInvite = new UserInvite();
        UserInviteService userInviteService = new UserInviteService(context);
        await userInviteService.DeleteUserInviteById(2);

        var userInvieteInDb = context.UserInvites.ToArray();

        Assert.Equal(2, userInvieteInDb.Length);
    }

    [Fact]
    public async Task DeleteUserInviteByUserId()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        UserInvite userInvite = new UserInvite();
        UserInviteService userInviteService = new UserInviteService(context);
        await userInviteService.DeleteUserInviteByUserId(6);

        var userInvieteInDb = context.UserInvites.ToArray();

        Assert.Equal(1, userInvieteInDb.Length);
    }

    [Fact]
    public async Task DeleteUserInviteByProjectId()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        UserInvite userInvite = new UserInvite();
        UserInviteService userInviteService = new UserInviteService(context);
        await userInviteService.DeleteUserInviteByProjectId(3);

        var userInvieteInDb = context.UserInvites.ToArray();

        Assert.Equal(0, userInvieteInDb.Length);
    }
}