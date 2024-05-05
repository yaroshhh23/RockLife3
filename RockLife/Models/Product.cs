using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RockLife.Models;

[Table("products")]
public partial class Product
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type")]
    public string? Type { get; set; }

    [Column("fabricator")]
    public string? Fabricator { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("color")]
    public string? Color { get; set; }

    [Column("price")]
    public double Price { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
