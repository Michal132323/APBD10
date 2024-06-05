using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using solution.DbContext;

namespace solution.Endpoints;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly MyDbContext _context;

    public AccountsController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet("{accountId:int}")]
    public async Task<IActionResult> GetAccount(int accountId)
    {
        var account = await _context.Accounts
            .Include(a => a.Role)
            .Include(a => a.ShoppingCarts)
            .ThenInclude(sc => sc.Product)
            .FirstOrDefaultAsync(a => a.AccountId == accountId);

        if (account == null)
        {
            return NotFound();
        }

        var result = new
        {
            firstName = account.FirstName,
            lastName = account.LastName,
            email = account.Email,
            phone = account.Phone,
            role = account.Role.Name,
            cart = account.ShoppingCarts.Select(sc => new
            {
                productId = sc.ProductId,
                productName = sc.Product.Name,
                amount = sc.Amount
            })
        };

        return Ok(result);
    }
}
