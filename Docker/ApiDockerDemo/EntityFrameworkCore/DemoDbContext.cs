using ApiDockerDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDockerDemo.EntityFrameworkCore;

public class DemoDbContext : DbContext {
    public DemoDbContext(DbContextOptions<DemoDbContext> options)
        : base(options) {
    }

    public DbSet<User> Users => Set<User>();
}