using Microsoft.EntityFrameworkCore;
using TaskManagerDataAccess.Models;

namespace TaskManagerDataAccess.DataAccess.Services;

public class EmailConfirmService
{
    private readonly TaskManagerDbContext _dbContext;

    public EmailConfirmService (TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EmailConfirm> AddEmailConfirm (EmailConfirm emailConfirm)
    {
        ArgumentNullException.ThrowIfNull(emailConfirm);

        _dbContext.Add(emailConfirm);
        await _dbContext.SaveChangesAsync();

        return emailConfirm;
    }

    public async Task<EmailConfirm> GetEmailConfirmByToken (string token)
    {
        var emailConfirm = await _dbContext.EmailConfirms
            .FirstOrDefaultAsync(e => e.EmailConfirmToken.Equals(token));

        ArgumentNullException.ThrowIfNull(emailConfirm);

        return emailConfirm;
    }

    public async Task DeleteAllEmailConfirm (int id)
    {
        var emailConfirm = await _dbContext.EmailConfirms
            .Where(e => e.UserId == id)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(emailConfirm);

        _dbContext.RemoveRange(emailConfirm);
        await _dbContext.SaveChangesAsync();
    }
}