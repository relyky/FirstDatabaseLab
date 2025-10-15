using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FirstDatabaseLab.DB.Schema;

[Index("OrderNo", Name = "IX_OrderItem_OrderNo")]
[Index("ProductNo", Name = "IX_OrderItem_ProductNo")]
public partial class OrderItem
{
    [Key]
    public int Ssn { get; set; }

    public int OrderNo { get; set; }

    public int ProductNo { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("OrderNo")]
    [InverseProperty("OrderItem")]
    public virtual Order OrderNoNavigation { get; set; } = null!;

    [ForeignKey("ProductNo")]
    [InverseProperty("OrderItem")]
    public virtual Product ProductNoNavigation { get; set; } = null!;
}
