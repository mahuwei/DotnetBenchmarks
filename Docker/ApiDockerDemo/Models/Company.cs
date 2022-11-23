using System.ComponentModel.DataAnnotations;

namespace ApiDockerDemo.Models;

public class Company {
    [Key]
    public Guid Id {get; set;}

    [Required]
    [MaxLength(10)]
    public string No {get; set;} = null!;

    [Required]
    [MaxLength(30)]
    public string Name {get; set;} = null!;
}