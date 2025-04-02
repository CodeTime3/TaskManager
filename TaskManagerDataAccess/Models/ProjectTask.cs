using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerDataAccess.Models;

[Table("project_tasks")]
public class ProjectTask
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProjectTaskId { get; set; }
    [ForeignKey("ProjectId")]
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    [MaxLength(50)]
    public string TaskTitle { get; set; }
    [MaxLength(200)]
    public string TaskText { get; set; }
    public Status TaskStatus { get; set; } = Status.NotStarted;
}