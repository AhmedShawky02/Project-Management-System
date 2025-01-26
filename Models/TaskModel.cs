using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project_Management_System.Models;

[Index("Name", Name = "UQ__Tasks__737584F6727FFE80", IsUnique = true)]
public partial class TaskModel
{
    [Key]
    public int TaskId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(250)]
    public string Description { get; set; } = null!;

    public int Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DueDate { get; set; }

    public int ProjectId { get; set; }

    public int AssignedTo { get; set; }

    [ForeignKey("AssignedTo")]
    [InverseProperty("Tasks")]
    public virtual User? AssignedToNavigation { get; set; }

    [InverseProperty("Task")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [ForeignKey("ProjectId")]
    [InverseProperty("Tasks")]
    public virtual Project? Project { get; set; }

    [ForeignKey("Status")]
    [InverseProperty("Tasks")]
    public virtual Status? StatusNavigation { get; set; }
}
