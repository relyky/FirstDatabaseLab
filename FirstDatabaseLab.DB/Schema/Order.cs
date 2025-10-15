using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FirstDatabaseLab.DB.Schema;

[Index("CustomerId", Name = "IX_Order_CustomerId")]
public partial class Order
{
    [Key]
    public int OrderNo { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal Amount { get; set; }

    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Order")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("OrderNoNavigation")]
    public virtual ICollection<OrderItem> OrderItem { get; set; } = new List<OrderItem>();
}
