using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerDataAccess.Models;

[Table("projects")]
public class Project
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProjectId { get; set; }
    [MaxLength(50)]
    [Required]
    public string ProjectName { get; set; }
    public Status ProjectStatus { get; set; } = Status.NotStarted;
}