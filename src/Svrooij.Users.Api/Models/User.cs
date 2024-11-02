using System.ComponentModel.DataAnnotations;

namespace Svrooij.Users.Api.Models;

public class User
{
    public required string Id { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public required string Email { get; set; }

    [Length(2,15)]
    public string? FavoriteAnimal { get; set; }
}
