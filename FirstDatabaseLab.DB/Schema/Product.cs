using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FirstDatabaseLab.DB.Schema;

public partial class Product
{
    [Key]
    public int ProductNo { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(18, 4)")]
    public decimal Price { get; set; }

    [InverseProperty("ProductNoNavigation")]
    public virtual ICollection<OrderItem> OrderItem { get; set; } = new List<OrderItem>();
}
