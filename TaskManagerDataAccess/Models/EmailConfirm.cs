using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace TaskManagerDataAccess.Models;

[Table("email_confirms")]
public class EmailConfirm
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int EmailConfirmId { get; set; }
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User User { get; set; }
    public string EmailConfirmToken { get; set; }
    public DateTime EmailConfirmCreateAt { get; set; }

    public EmailConfirm (int userId, string emailConfirmToken, DateTime emailConfirmCreateAt) 
    {   
        UserId = userId;
        EmailConfirmToken = emailConfirmToken;
        EmailConfirmCreateAt = emailConfirmCreateAt;
    }

    public EmailConfirm () {}
}