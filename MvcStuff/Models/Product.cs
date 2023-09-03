using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcStuff.Models;
public class Product
{
    public int Id { get; set; }
    [Required, MaxLength(10), MinLength(3)]
    public string Name { get; set; } = null!;
    [Required, DisplayFormat(DataFormatString = "{0:C}"/*, ApplyFormatInEditMode = true*/)]
    public decimal Price { get; set; }
    
    public string? Description { get; set; }

}
