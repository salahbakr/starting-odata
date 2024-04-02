using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using OdataTest.Data;
using OdataTest.Data.Entities;
using OdataTest.Dtos;

namespace OdataTest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController(ApplicationDbContext dbContext) : ODataController
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    [EnableQuery]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult<IEnumerable<Customer>>> Get()
    {
        var customers = await _dbContext.Customers.AsNoTracking().Include(c => c.Orders).ToListAsync();
        return Ok(customers);
    }

    [EnableQuery]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult<Customer>> Get([FromRoute] int key)
    {
        var customer = await _dbContext.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == key);
        return Ok(customer);
    }

    [HttpPost]
    public async Task Create([FromBody] CustomerRequestDto customerDto)
    {
        var customer = new Customer
        {
            Name = customerDto.Name,
        };

        _dbContext.Customers.Add(customer);
        await _dbContext.SaveChangesAsync();
    }
}
