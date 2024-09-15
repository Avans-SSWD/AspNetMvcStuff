
using Microsoft.AspNetCore.Mvc;
using MvcStuff.Controllers;
using MvcStuff.Core.Domain.Interfaces;
using MvcStuff.Core.Domain.Models;
using MvcStuff.Models;
using NSubstitute;

namespace MvcStuff.Test.Controllers;
public class ProductControllerTests
{
    [Fact]
    public void Can_Use_Repository()
    {
        // Arrange
        var mock = Substitute.For<IProductRepository>();
        mock.GetAll().Returns([new() { Name = "a" }, new() { Name = "b" }]);
        var sut = new ProductController(mock);

        // Act
        IEnumerable<Product>? result = (sut.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

        // Assert
        Product[] actualProducts = result?.ToArray() ?? Array.Empty<Product>();
        // assertions here -->

    }

    [Fact]
    public void Can_Paginate()
    {
        // Arrange
        var mock = Substitute.For<IProductRepository>();

        mock.GetAll().Returns((new Product[]
            {
                new () { Name = "a" },
                new () { Name = "b" },
                new () { Name = "c" },
                new () { Name = "d" },
                new () { Name = "e" },
            }).AsQueryable<Product>());
        var sut = new ProductController(mock);
        sut.PageSize = 3;

        // Act
        IEnumerable<ProductViewModel>? result = ((sut.Index(2) as ViewResult)?
            .Model as ProductListViewModel)?
            .Products;

        // Assert
        ProductViewModel[] actualResponses = result?.ToArray() ?? Array.Empty<ProductViewModel>();
        // assertions here -->
        Assert.True(actualResponses.Length == 2);
        Assert.Equal("d", actualResponses[0].Name);
        Assert.Equal("e", actualResponses[1].Name);
    }

}
