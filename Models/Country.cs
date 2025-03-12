using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace migrationsKursovaiya.Models;

public partial class Country
{
    public int Id { get; set; }

    [MinLength(1, ErrorMessage = "Минимальная длина - 2 символов")]
    [MaxLength(12, ErrorMessage = "Максимальная длина - 30 символов")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();
}
