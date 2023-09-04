using Microsoft.AspNetCore.Mvc;
using MvcStuff.Domain.Models;
using MvcStuff.Repositories;

namespace MvcStuff.Controllers;
public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public IActionResult Index()
    {
        ViewBag.Title = "Viewbag title vanuit ActionMethod";
        ViewBag.Blablabla = "bladadad";
        return View(_productRepository.GetAll());
    }

    public IActionResult Edit(int id)
    {
        var productToEdit = _productRepository.Get(id);
        if (productToEdit == null)
        {
            return RedirectToAction("Index");
        }

        return View(productToEdit);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        
        if (product.Id == 1)
        {
            ModelState.AddModelError(nameof(Product.Id), "Id cannot be 1. Good luck with that");
        }

        if (!ModelState.IsValid)
        {
            return View(product);
        }

        _productRepository.Update(product);
        
        return RedirectToAction("Index");
    }

}
