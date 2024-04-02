using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using OdataTest.Data;
using OdataTest.Data.Entities;
using OdataTest.Dtos;

namespace OdataTest.Controllers;

public class OrdersController(ApplicationDbContext dbContext) : ODataController
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    [EnableQuery]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult<IEnumerable<Order>>> Get()
    {
        var customers = await _dbContext.Orders.AsNoTracking().ToListAsync();
        return Ok(customers);
    }

    [EnableQuery]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult<Order>> Get([FromRoute] int key)
    {
        var customer = await _dbContext.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == key);
        return Ok(customer);
    }
}
