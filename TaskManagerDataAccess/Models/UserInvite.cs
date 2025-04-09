using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerDataAccess.Models;

[Table("user_invites")]
public class UserInvite 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InviteId { get; set; }
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User User { get; set; }
    [ForeignKey("ProjectId")]
    public int ProjectId { get; set; }
    public Project project { get; set; }
    public DateTime ExpirationDate { get; set; } 
    [Required]
    [MaxLength(50)]
    public string InviteMailObject { get; set; } 
    [Required]
    [MaxLength(200)]
    public string InviteMailText { get; set; }
    [Required]
    [MaxLength(500)]
    public string InviteLink { get; set; }

    public UserInvite (int userId, int projectId, DateTime expirationDate, string inviteMailObject, string inviteMailText, string inviteLink)
    {
        UserId = userId;
        ProjectId = projectId;
        ExpirationDate = expirationDate;
        InviteMailObject = inviteMailObject;
        InviteMailText = inviteMailText;
        InviteLink = inviteLink;
    }

    public UserInvite () {}
}