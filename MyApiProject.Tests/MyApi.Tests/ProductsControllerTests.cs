using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Controllers;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Tests;

public class ProductsControllerTests
{
    private ApplicationDbContext GetInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public void Post_AddsProduct()
    {
        var db = GetInMemoryDb();
        var controller = new ProductsController(db);

        var product = new Product { Name = "Test Product", Price = 99.99M };
        var result = controller.Post(product) as CreatedAtActionResult;

        Assert.NotNull(result);
        Assert.Equal(1, db.Products.Count());
        Assert.Equal("Test Product", db.Products.First().Name);
    }

    [Fact]
    public void Get_ReturnsAllProducts()
    {
        var db = GetInMemoryDb();
        db.Products.Add(new Product { Name = "Item 1", Price = 10 });
        db.Products.Add(new Product { Name = "Item 2", Price = 20 });
        db.SaveChanges();

        var controller = new ProductsController(db);
        var result = controller.Get() as OkObjectResult;

        var products = result?.Value as IQueryable<Product>;
        Assert.NotNull(result);
        Assert.Equal(2, db.Products.Count());
    }
}