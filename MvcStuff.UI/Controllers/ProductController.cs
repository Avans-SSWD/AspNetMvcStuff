using Microsoft.AspNetCore.Mvc;
using MvcStuff.Core.Domain.Interfaces;
using MvcStuff.Core.Domain.Models;
using MvcStuff.Models;

namespace MvcStuff.Controllers;
public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public int PageSize { get; set; } = 2;

    public IActionResult Index(int page = 1)
    {
        // ViewBag zo min mogelijk gebruiken! -->
        ViewBag.Title = "Viewbag title vanuit ActionMethod";
        ViewBag.Blablabla = "bladadad";
        
        var productsToReturn = _productRepository.GetAll()
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

        // Viewmodels zijn betere optie -->
        ProductListViewModel model = new()
        {
            Title = "Product overview",
            Products = productsToReturn
                .Select(p => // Ook de collectie van entiteiten omzetten naar viewmodels -->
                    new ProductViewModel 
                    { 
                        Id = p.Id,
                        Name = p.Name, 
                        Description = p.Description, 
                        Price = p.Price 
                    }),
            PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                // Materialized .Count() de collectie? Zo ja is dit een slecht idee -->
                TotalItems = _productRepository.GetAll().Count(),
            },
        };
        return View(model);
    }

    public IActionResult Detail(int id)
    {
        var productEntity = _productRepository.Get(id);

        if (productEntity == null)
        {
            //throw new Exception("Product not found");
            return new NotFoundResult();
        }
        var viewModel = new ProductViewModel
        {
            Id = productEntity.Id,
            Name = productEntity.Name,
            Price = productEntity.Price,
            Description = productEntity.Description
        };
        return View(viewModel);

    }

    public IActionResult Edit(int id)
    {
        var productToEdit = _productRepository.Get(id);
        if (productToEdit == null)
        {
            return RedirectToAction("Index");
        }
        // opgehaalde product entiteit omzetten naar viewmodel -->
        var viewModel = new ProductViewModel
        {
            Id = productToEdit.Id,
            Name = productToEdit.Name,
            Price = productToEdit.Price,
            Description = productToEdit.Description
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Edit(ProductViewModel productViewModel)
    {
        
        if (productViewModel.Id == 1)
        {
            ModelState.AddModelError(nameof(Product.Id), "Id cannot be 1. Good luck with that");
        }

        if (!ModelState.IsValid)
        {
            return View(productViewModel);
        }

        // viewmodel omzetten naar product entiteit -->
        var productToUpdate = new Product
        {
            Id = productViewModel.Id,
            Name = productViewModel.Name,
            Price = productViewModel.Price,
            Description = productViewModel.Description
        };

        // persisteren van de entiteit -->
        _productRepository.Update(productToUpdate);

        // Netter zou zijn om een redirect naar
        // de detailpagina van het product te doen
        // of een melding te geven dat bijwerken gelukt is -->
        return RedirectToAction("Detail", new { id = productToUpdate.Id });
        //return RedirectToAction("Index");
    }

}
