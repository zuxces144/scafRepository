using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace migrationsKursovaiya.Models;

public partial class User
{
    public int Id { get; set; }

    [MinLength(1, ErrorMessage = "Минимальная длина - 5 символов")]
    [MaxLength(12, ErrorMessage = "Максимальная длина - 50 символов")]
    public string Nickname { get; set; } = null!;

    [Phone(ErrorMessage = "Введите корректный номер телефона")]
    public string Phone { get; set; } = null!;

    [EmailAddress(ErrorMessage = "Введите корректный email")]
    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    [MinLength(1, ErrorMessage = "Минимальная длина - 2 символов")]
    [MaxLength(12, ErrorMessage = "Максимальная длина - 10 символов")]
    public string? Name { get; set; }

    [MinLength(1, ErrorMessage = "Минимальная длина - 2 символов")]
    [MaxLength(12, ErrorMessage = "Максимальная длина - 20 символов")]
    public string? Surname { get; set; }

    public int? IdCity { get; set; }

    public virtual ICollection<Order> OrderIdUserBuyerNavigations { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderIdUserSellerNavigations { get; set; } = new List<Order>();
}
