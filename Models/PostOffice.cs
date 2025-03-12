using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace migrationsKursovaiya.Models;

public partial class PostOffice
{
    public int Id { get; set; }

    [MinLength(1, ErrorMessage = "Минимальная длина - 2 символов")]
    [MaxLength(12, ErrorMessage = "Максимальная длина - 30 символов")]
    public string Name { get; set; } = null!;

    public int IdCity { get; set; }

    public virtual City IdCityNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
