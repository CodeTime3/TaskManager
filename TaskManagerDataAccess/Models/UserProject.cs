using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerDataAccess.Models;

[Table("user_projects")]
public class UserProject
{   
    [ForeignKey("ProjectId")]
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User User { get; set; }
    public Role Role { get; set; } 
}