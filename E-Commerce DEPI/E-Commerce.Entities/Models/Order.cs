﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public partial class Order
{
    public int OrderId { get; set; }

    [ForeignKey("ApplicationUser")]
    public int UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    public DateTime? OrderDate { get; set; }

    public string OrderShippingAddress { get; set; }

    public decimal OrderShippingCost { get; set; }

    public string OrderStatus { get; set; }

    public DateTime? OrderEstimatedDeliveryDate { get; set; }

    public bool? OrderIsShipped { get; set; }


}