using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerDataAccess.Models;

[Table("users")]
public class User
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int UserId { get; set; }
    [ForeignKey("EmailConfirmId")]
    public int EmailConfirmId { get; set; }
    public EmailConfirm EmailConfirm { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(5)]
    public string UserName { get; set; }
    [Required]
    [MinLength(5)]
    public string UserMail { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(8)]
    public string UserHash { get; set; }

    public User(int emailConfirmId, string userName, string userMail, string userHash)
    {   
        EmailConfirmId = emailConfirmId;
        UserName = userName;
        UserMail = userMail;
        UserHash = userHash;
    }

    public User() {}
}