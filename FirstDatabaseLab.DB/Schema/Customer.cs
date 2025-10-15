using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FirstDatabaseLab.DB.Schema;

[Table("Customer")]
public partial class Customer
{
    /// <summary>
    /// 我是Id欄位說明
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 我是Name欄位說明
    /// </summary>
    [StringLength(20)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 我是Email欄位說明
    /// </summary>
    [StringLength(50)]
    public string Email { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
