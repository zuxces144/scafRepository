using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace migrationsKursovaiya.Models;

public partial class Product
{
    public int Id { get; set; }

    [MinLength(5, ErrorMessage = "Минимальная длина - 5 символов")]
    public string Name { get; set; } = null!;

    [MinLength(10, ErrorMessage = "Минимальная длина - 5 символов")]
    public string? Description { get; set; }

    public int IdCategory { get; set; }

    [Range(0.1, 10000000, ErrorMessage = "Цена должна быть от 0.1 до 10000000")]
    public int Price { get; set; }

    public virtual Category IdCategoryNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
