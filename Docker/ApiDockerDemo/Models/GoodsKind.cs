using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDockerDemo.Models;

public class GoodsKind {
    [Key]
    public Guid Id {get; set;}

    [Required]
    [MaxLength(30)]
    public string Name {get; set;} = null!;

    [ForeignKey(nameof(Goods.GoodsKindId))]
    public List<Goods>? GoodsList {get; set;}

    public Guid CompanyId {get; set;}

    [ForeignKey(nameof(CompanyId))]
    public Company? Company {get; set;}
}

public class Goods {
    [Key]
    public Guid Id {get; set;}

    [Required]
    [MaxLength(30)]
    public string Name {get; set;} = null!;

    public Guid GoodsKindId {get; set;}
}