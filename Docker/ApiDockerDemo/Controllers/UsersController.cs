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
        var users = await dbContext.Users.Include(d => d.Company).AsNoTracking().ToListAsync();
        return users;
    }

    [HttpPost]
    public async Task<User> PostAsync(string name, int age) {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(d => d.Name == name);
        if(user != null) return user;
        var company = await GetCompanyAsync(dbContext);
        user = new User { Id = Guid.NewGuid(), Name = name, Age = age, CompanyId = company.Id };
        var entityEntry = await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        return entityEntry.Entity;
    }

    private async Task<Company> GetCompanyAsync(DemoDbContext dbContext) {
        var company = await dbContext.Companies.FirstOrDefaultAsync();
        if(company != null) return company;
        company = new Company { Id = Guid.NewGuid(), No = "C001", Name = "µÚÒ»¼Ò" };
        var entityEntry = await dbContext.Companies.AddAsync(company);
        await dbContext.SaveChangesAsync();
        return entityEntry.Entity;
    }
}