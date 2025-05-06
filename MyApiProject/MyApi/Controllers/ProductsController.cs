using Microsoft.AspNetCore.Mvc;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(context.Products.ToList());
    }

    [HttpPost]
    public IActionResult Post([FromBody] Product product)
    {
        try
        {
            context.Products.Add(product);
            context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
           
    }
}