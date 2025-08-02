using TaskManagerDataAccess;

namespace TaskManagerWeb.Models;

public class UpdateUserProjectModel
{
    public int ProjectId { get; set; }
    public Role Role { get; set; }
}