﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public partial class Review
{
    public int ReviewId { get; set; }
    [ForeignKey("ApplicationUser")]

    public int UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }


    public int ProductId { get; set; }

    public decimal Rate { get; set; }

    public virtual Product Product { get; set; }

}