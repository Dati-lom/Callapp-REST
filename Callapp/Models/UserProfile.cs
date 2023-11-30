using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Callapp.Models;

public class UserProfile
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserProfileId { get; set; }

    [StringLength(11, MinimumLength = 11)]
    public string PersonalNumber { get; set; }

    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    public int UserIdFk { get; set; }
    public User User { get; set; }
}