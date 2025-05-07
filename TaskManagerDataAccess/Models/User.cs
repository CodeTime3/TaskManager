using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerDataAccess.Models;

[Table("users")]
public class User
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int UserId { get; set; }
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

    public User(string userName, string userMail, string userHash)
    {   
        UserName = userName;
        UserMail = userMail;
        UserHash = userHash;
    }

    public User() {}
}