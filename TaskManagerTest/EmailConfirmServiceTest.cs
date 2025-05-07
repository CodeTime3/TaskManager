using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManagerDataAccess.DataAccess;
using TaskManagerDataAccess.DataAccess.Services;
using TaskManagerDataAccess.Models;

namespace TaskManagerTest;

public class EmailConfirmServiceTest
{
    [Fact]
    public async Task AddEmailConfirm_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        DateTime dateTime = new DateTime();
        Guid guid = Guid.NewGuid();
        EmailConfirm emailConfirm = new EmailConfirm(1, guid.ToString(), dateTime.Date);
        EmailConfirmService emailConfirmService = new EmailConfirmService(context);
        await emailConfirmService.AddEmailConfirm(emailConfirm);

        var emailConfirmInDb = context.EmailConfirms.ToArray();

        Assert.Equal(guid.ToString(), emailConfirmInDb[1].EmailConfirmToken);
        Assert.Equal(dateTime.Date, emailConfirmInDb[1].EmailConfirmCreateAt);
    }

    [Fact]
    public async Task GetEmailConfirmByToken_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        EmailConfirm emailConfirm = new EmailConfirm();
        EmailConfirmService emailConfirmService = new EmailConfirmService(context);
        var token = "4b6eb6a5-6828-41f7-a780-fdac232aa7d2";
        emailConfirm = await emailConfirmService.GetEmailConfirmByToken(token);

        Assert.Equal(token, emailConfirm.EmailConfirmToken);
    }

    [Fact]
    public async Task DeleteAllEmailConfirm_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        EmailConfirm emailConfirm = new EmailConfirm();
        EmailConfirmService emailConfirmService = new EmailConfirmService(context);
        await emailConfirmService.DeleteAllEmailConfirm(1);

        var emailConfirmInDb = context.EmailConfirms.ToArray();

        Assert.Equal(0, emailConfirmInDb.Length);
    }
}