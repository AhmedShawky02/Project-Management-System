using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project_Management_System.Models;

public partial class UserRole
{
    [Key]
    public int UserRoleId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column("RoleID")]
    public int? RoleId { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("UserRoles")]
    public virtual Role? Role { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserRoles")]
    public virtual User? User { get; set; }
}
