using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace migrationsKursovaiya.Models;

public partial class Order
{
    public int Id { get; set; }

    public int IdProduct { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? Ordered { get; set; }

    [Range(1, 100000, ErrorMessage = "Значение должно быть от 1 до 100000")]
    public int? ToPay { get; set; }

    public int IdUserBuyer { get; set; }

    public int IdPostOffice { get; set; }

    public int IdUserSeller { get; set; }

    public string? AditionalInformation { get; set; }

    public virtual PostOffice IdPostOfficeNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual User IdUserBuyerNavigation { get; set; } = null!;

    public virtual User IdUserSellerNavigation { get; set; } = null!;
}
