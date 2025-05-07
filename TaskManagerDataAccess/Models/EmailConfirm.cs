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
    public Guid EmailConfirmToken { get; set; }
    public DateTime EmailConfirmCreateAt { get; set; }

    public EmailConfirm (int emailConfirmId, Guid emailConfirmToken, DateTime emailConfirmCreateAt) 
    {
        EmailConfirmId = emailConfirmId;
        EmailConfirmToken = emailConfirmToken;
        EmailConfirmCreateAt = emailConfirmCreateAt;
    }

    public EmailConfirm () {}
}