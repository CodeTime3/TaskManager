using Microsoft.EntityFrameworkCore;
using TaskManagerDataAccess.Models;

namespace TaskManagerDataAccess.DataAccess;

public class TaskManagerDbContext : DbContext
{
    public TaskManagerDbContext (DbContextOptions options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<UserProject> UserProjects { get; set; }
    public DbSet<UserInvite> UserInvites { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        builder.Entity<User>()
            .HasIndex(u => u.UserMail)
            .IsUnique();

        builder.Entity<UserProject>()
            .HasKey(u => new { u.UserId, u.ProjectId });
    }
}
