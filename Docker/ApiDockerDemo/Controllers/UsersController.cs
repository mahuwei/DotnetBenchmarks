using ApiDockerDemo.EntityFrameworkCore;
using ApiDockerDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDockerDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase {
    private readonly IDbContextFactory<DemoDbContext> _dbContextFactory;

    public UsersController(IDbContextFactory<DemoDbContext> dbContextFactory) {
        _dbContextFactory = dbContextFactory;
    }

    [HttpGet]
    public async Task<IEnumerable<User>> GetAsync() {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        var users = await dbContext.Users.ToListAsync();
        return users;
    }

    [HttpPost]
    public async Task<User> PostAsync(string name, int age) {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(d => d.Name == name);
        if(user != null) return user;
        user = new User { Id = Guid.NewGuid(), Name = name, Age = age };
        var entityEntry = await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        return entityEntry.Entity;
    }
}