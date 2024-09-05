using System.ComponentModel.DataAnnotations;

namespace MvcStuff.Core.Domain.Models;
public class Product
{
    public int Id { get; set; }
    [Required, MaxLength(10), MinLength(3)]
    public required string Name { get; set; }
    [Required, DisplayFormat(DataFormatString = "{0:C}"/*, ApplyFormatInEditMode = true*/)]
    public decimal Price { get; set; }

    public string? Description { get; set; }
}
