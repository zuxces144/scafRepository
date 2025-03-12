using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace migrationsKursovaiya.Models;

public partial class Category
{
    public int Id { get; set; }

    [MinLength(1, ErrorMessage = "Минимальная длина - 2 символов")]
    [MaxLength(12, ErrorMessage = "Максимальная длина - 40 символов")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
