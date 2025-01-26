using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project_Management_System.Models;

[Table("Status")]
[Index("Name", Name = "UQ__Status__737584F6E532C705", IsUnique = true)]
public partial class Status
{
    [Key]
    public int StatusId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
}
