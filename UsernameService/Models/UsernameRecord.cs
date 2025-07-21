using System.ComponentModel.DataAnnotations;

namespace UsernameService.Models;

public class UsernameRecord
{
    [Key]
    public Guid AccountId { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 6)]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Username must be alphanumeric only.")]
    public string Username { get; set; } = string.Empty;
}
