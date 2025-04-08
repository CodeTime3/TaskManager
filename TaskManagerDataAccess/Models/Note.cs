using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerDataAccess.Models;

[Table("notes")]
public class Note
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NotedId { get; set; }
    [ForeignKey("ProjectTaskId")]
    public int ProjectTaskId { get; set; }
    public ProjectTask ProjectTask { get; set; }
    [MaxLength(50)]
    public string NoteTitle { get; set; }
    [MaxLength(200)]
    public string NoteText { get; set; }

    public Note (int projectTaskId, string noteTitle, string noteText)
    {
        ProjectTaskId = projectTaskId;
        NoteTitle = noteTitle;
        NoteText = noteText;
    }

    public Note () {}
}