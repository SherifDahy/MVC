﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public string CommentText { get; set; }

    [ForeignKey("Review")]
    public int ReviewId { get; set; }

    public Review Review { get; set; }
}