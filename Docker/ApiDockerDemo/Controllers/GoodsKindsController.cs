using ApiDockerDemo.EntityFrameworkCore;
using ApiDockerDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDockerDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class GoodsKindsController : ControllerBase {
    private readonly IDbContextFactory<DemoDbContext> _dbContextFactory;
    private readonly ILogger<GoodsKindsController> _logger;

    public GoodsKindsController(IDbContextFactory<DemoDbContext> dbContextFactory,
        ILogger<GoodsKindsController> logger) {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    [HttpGet]
    public async Task<List<GoodsKind>> GetList() {
        _logger.LogInformation("开始查询...");
        await using var db = await _dbContextFactory.CreateDbContextAsync();
        //await CheckData(db);
        var goodsList = await db.GoodsKinds.AsSingleQuery()
            .AsNoTracking()
            .Include(d => d.Company)
            .Include(d => d.GoodsList)
            .ToListAsync();
        _logger.LogInformation("查询结束.");
        return goodsList;
    }

    [HttpGet]
    [Route("SplitQuery")]
    public async Task<List<GoodsKind>> GetSplitQueryList() {
        _logger.LogInformation("开始查询...");
        await using var db = await _dbContextFactory.CreateDbContextAsync();
        //await CheckData(db);
        var goodsList = await db.GoodsKinds
            .AsNoTracking()
            .Include(d => d.Company)
            .Include(d => d.GoodsList)
            .ToListAsync();
        _logger.LogInformation("查询结束.");
        return goodsList;
    }

    private async Task CheckData(DemoDbContext db) {
        var company = await db.Companies.FirstOrDefaultAsync();
        if(company == null){
            company = new Company { Id = Guid.NewGuid(), No = "001", Name = "第一季" };
            await db.Companies.AddAsync(company);
        }

        if(await db.GoodsKinds.AnyAsync() == false){
            var goodsKinds = new List<GoodsKind> {
                new GoodsKind {
                    Id = Guid.NewGuid(), CompanyId = company.Id, Name = "第一类"
                },
                new GoodsKind {
                    Id = Guid.NewGuid(), CompanyId = company.Id, Name = "第二类"
                }
            };
            foreach(var goodsKind in goodsKinds){
                goodsKind.GoodsList = new List<Goods>();
                for(var i = 0; i < 20; i++)
                    goodsKind.GoodsList.Add(new Goods {
                        Id = Guid.NewGuid(), GoodsKindId = goodsKind.Id,
                        Name = $"{goodsKind.Name}-{i}"
                    });
            }

            await db.GoodsKinds.AddRangeAsync(goodsKinds);
            await db.SaveChangesAsync();
        }
    }
}