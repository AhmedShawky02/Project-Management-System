using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project_Management_System.Models;

[Index("Name", Name = "UQ__Projects__737584F671B63A72", IsUnique = true)]
public partial class Project
{
    [Key]
    public int ProjectId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(250)]
    public string Description { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EndDate { get; set; }

    public int Status { get; set; }

    public int CreatedBy { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("Projects")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("Status")]
    [InverseProperty("Projects")]
    public virtual Status? StatusNavigation { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
}
