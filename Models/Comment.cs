using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project_Management_System.Models;

public partial class Comment
{
    [Key]
    public int CommentId { get; set; }

    [StringLength(100)]
    public string Text { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    public int TaskId { get; set; }

    public int CreatedBy { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("Comments")]
    public virtual User CreatedByNavigation { get; set; }

    [ForeignKey("TaskId")]
    [InverseProperty("Comments")]
    public virtual TaskModel Task { get; set; }
}
