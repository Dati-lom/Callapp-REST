using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Callapp.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [Required] public string Username { get; set; }

    [Required] public string Password { get; set; }

    [Required] public string Email { get; set; }

    public bool IsActive { get; set; } = true;


    public int UserProfIdFk { get; set; }
    public UserProfile UserProfile { get; set; }

    public override string ToString()
    {
        return "Username: " + Username + " | Email: " + Email + " | Password: nope :D";
    }
}