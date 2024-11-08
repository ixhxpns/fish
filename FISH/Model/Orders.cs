namespace FISH.Model;

using System;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents a daily order with various attributes including order details, delivery information, and status flags.
/// </summary>
public class Orders
{
    /// <summary>
    /// Gets or sets the unique identifier for the order.
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name associated with the order.
    /// </summary>
    [Required]
    [StringLength(10)]
    [Display(Name = "姓名")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the order was created.
    /// </summary>
    [Required]
    [Display(Name = "建單時間")]
    public DateTime OrderCreateDateTime { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the order is scheduled for pickup by the customer.
    /// </summary>
    [Required]
    [Display(Name = "自取時間")]
    public DateTime GetMySelfDateTime { get; set; }

    /// <summary>
    /// Gets or sets the type of the order.
    /// </summary>
    [Required]
    [StringLength(100)]
    [Display(Name = "訂單類型")]
    public string OrderType { get; set; }

    /// <summary>
    /// Gets or sets the detailed description of the order.
    /// </summary>
    [Required]
    [Display(Name = "訂單數量")]
    public int Count { get; set; }

    /// <summary>
    /// Gets or sets the detailed description of the order.
    /// </summary>
    [Required]
    [Display(Name = "訂單明細")]
    public string OrderDetail { get; set; }

    /// <summary>
    /// Gets or sets the total amount of the order.
    /// </summary>
    [Required]
    [Display(Name = "訂單總金額")]
    [DataType(DataType.Currency)]
    public decimal OrderAmount { get; set; }

    /// <summary>
    /// Gets or sets the pricing method used for the order.
    /// </summary>
    [Required]
    [Display(Name = "付款方式")]
    public string PricingMethod { get; set; } = "現金";

    /// <summary>
    /// Gets or sets the delivery method for the order.
    /// </summary>
    [Required]
    [Display(Name = "交貨方法")]
    public string DeliveryMethod { get; set; } = "定點取";

    /// <summary>
    /// Gets or sets the delivery time for the order.
    /// </summary>
    [Required]
    [Display(Name = "交貨時間")]
    public DateTime DeliveryTime { get; set; }

    /// <summary>
    /// Gets or sets the delivery address for the order.
    /// </summary>
    [Required]
    [Display(Name = "交貨地址")]
    public string DeliveryAddress { get; set; }

    /// <summary>
    /// Gets or sets the contact number associated with the order.
    /// </summary>
    [Required]
    [StringLength(10)]
    [Display(Name = "連絡電話")]
    [Phone]
    public string ContactNumber { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the order is deleted.
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether the order is selected.
    /// </summary>
    public bool IsSeleted { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether the order is printed.
    /// </summary>
    public bool IsPrinted { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether the order is delivered.
    /// </summary>
    public bool IsDelivered { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether the order is paid.
    /// </summary>
    public bool IsPaid { get; set; } = false;
}