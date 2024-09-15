using System.ComponentModel.DataAnnotations;

namespace MvcStuff.Models;
public class ProductViewModel
{
    public int Id { get; set; }
    [Required, MaxLength(10), MinLength(3)]
    public string Name { get; set; } = null!;
    [Required, DisplayFormat(DataFormatString = "{0:C}"/*, ApplyFormatInEditMode = true*/)]
    public decimal Price { get; set; }

    public string? Description { get; set; }
}
