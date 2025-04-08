using TaskManagerDataAccess.Models;

namespace TaskManagerDataAccess.DataAccess.Services
{
    public class ProjectService
    {
        private readonly TaskManagerDbContext _dbContext;

        public ProjectService (TaskManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Project> AddProject (Project project)
        {
            ArgumentNullException.ThrowIfNull(project);

            _dbContext.Add(project);
            await _dbContext.SaveChangesAsync();

            return project;
        }

        public async Task<Project> GetProject (int id)
        {
            var project = await _dbContext.Projects.FindAsync(id);

            ArgumentNullException.ThrowIfNull(project);

            return project;
        }

        public async Task<Project> UpdateProject (Project project)
        {
            ArgumentNullException.ThrowIfNull(project);

            _dbContext.Update(project);
            await _dbContext.SaveChangesAsync();

            return project;
        }

        public async Task DeleteProject (int id)
        {
            var project = await _dbContext.Projects.FindAsync(id);

            ArgumentNullException.ThrowIfNull(project);

            _dbContext.Remove(project);
            await _dbContext.SaveChangesAsync();
        }
    }
}