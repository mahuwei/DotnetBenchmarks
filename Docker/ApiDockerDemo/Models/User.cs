using System.ComponentModel.DataAnnotations;

namespace ApiDockerDemo.Models;

public class User {
    [Key]
    public Guid Id {get; set;}

    [Required]
    [MaxLength(30)]
    public string Name {get; set;} = null!;

    public int Age {get; set;}
}