using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project_Management_System.Models;

[Index("Email", Name = "UQ__Users__A9D10534C723F84E", IsUnique = true)]
[Index("UserName", Name = "UQ__Users__C9F28456EA84848F", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("AssignedToNavigation")]
    public virtual ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();

    [InverseProperty("User")]
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
