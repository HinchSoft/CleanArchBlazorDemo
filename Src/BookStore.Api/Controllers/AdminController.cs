using BookStore.Api.Utilities;
using BookStore.Application.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly BookStoreContext _context;

    public AdminController(
        ILogger<AdminController> logger,
        BookStoreContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("initialise")]
    public async Task<IActionResult> Initialise()
    {
        await _context.Database.EnsureCreatedAsync();
        var changes = await _context.SeedDb();

        return Ok(changes);
    }
}
