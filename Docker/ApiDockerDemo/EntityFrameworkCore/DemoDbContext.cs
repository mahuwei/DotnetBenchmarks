using ApiDockerDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDockerDemo.EntityFrameworkCore;

public class DemoDbContext : DbContext {
    public DemoDbContext(DbContextOptions<DemoDbContext> options)
        : base(options) {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<GoodsKind> GoodsKinds => Set<GoodsKind>();
    public DbSet<Goods> Goods => Set<Goods>();
}