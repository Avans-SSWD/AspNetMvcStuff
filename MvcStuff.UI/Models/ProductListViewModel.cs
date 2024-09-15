using MvcStuff.Core.Domain.Models;

namespace MvcStuff.Models;
public class ProductListViewModel
{
    public required string Title { get; set; }
    public string? InfoNeededForView { get; set; }
    public required IEnumerable<ProductViewModel> Products { get; set; }
    public required PagingInfo PagingInfo { get; set; }
}
