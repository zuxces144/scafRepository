using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace migrationsKursovaiya.Models;

public partial class City
{
    public int Id { get; set; }

    [MinLength(1, ErrorMessage = "Минимальная длина - 2 символов")]
    [MaxLength(12, ErrorMessage = "Максимальная длина - 20 символов")]
    public string Name { get; set; } = null!;

    public int IdRegion { get; set; }

    public virtual Region IdRegionNavigation { get; set; } = null!;

    public virtual ICollection<PostOffice> PostOffices { get; set; } = new List<PostOffice>();
}
