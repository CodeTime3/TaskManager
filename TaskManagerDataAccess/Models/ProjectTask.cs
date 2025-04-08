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

    public ProjectTask (int projectId, string taskTitle, string taskText)
    {   
        ProjectId = projectId;
        TaskTitle = taskTitle;
        TaskText = taskText;
    }

    public ProjectTask (int projectId, string taskTitle, string taskText, Status taskStatus)
    {   
        ProjectId = projectId;
        TaskTitle = taskTitle;
        TaskText = taskText;
        TaskStatus = taskStatus;
    }

    public ProjectTask () {}
}