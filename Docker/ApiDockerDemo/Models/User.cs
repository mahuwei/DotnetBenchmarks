﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDockerDemo.Models;

public class User {
    [Key]
    public Guid Id {get; set;}

    [Required]
    [MaxLength(30)]
    public string Name {get; set;} = null!;

    public int Age {get; set;}

    public Guid CompanyId {get; set;}

    [ForeignKey(nameof(CompanyId))]
    public Company? Company {get; set;}
}