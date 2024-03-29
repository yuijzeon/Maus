using Maus.Infrastructure.Payment;
using Maus.Infrastructure.Payment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Maus.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly PaymentContext _context;

    public TestController(PaymentContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    [HttpGet("provider")]
    public async IAsyncEnumerable<ProviderEntity> GetProvider([FromQuery(Name = "id")] List<int> ids)
    {
        var fetchAll = ids.Count == 0;

        var asyncEntities = _context.Providers
            .Include(x => x.ProviderMethods)
            .ThenInclude(x => x.ProviderMethodBanks)
            .Where(x => fetchAll || ids.Contains(x.Id))
            .AsAsyncEnumerable();

        await foreach (var entity in asyncEntities)
        {
            yield return entity;
        }
    }

    [HttpPost("provider")]
    public async Task<IActionResult> PostProvider([FromBody] ProviderEntity entity)
    {
        _context.Providers.Add(entity);
        await _context.SaveChangesAsync();

        return Ok();
    }
}