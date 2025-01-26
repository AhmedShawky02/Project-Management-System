using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Models;

namespace Project_Management_System.Context;

public partial class ProjectManagementDbContext : DbContext
{

    public ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<TaskModel> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFCAA3B667BC");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Comments).HasConstraintName("FK__Comments__Create__5812160E");

            entity.HasOne(d => d.Task).WithMany(p => p.Comments).HasConstraintName("FK__Comments__TaskId__571DF1D5");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABEF0ABBD8042");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Projects).HasConstraintName("FK__Projects__Create__4E88ABD4");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Projects).HasConstraintName("FK__Projects__Status__4D94879B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A94698019");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__C8EE2063F0BECBB3");
        });

        modelBuilder.Entity<TaskModel>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949B1005B31BB");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.Tasks).HasConstraintName("FK__Tasks__AssignedT__5441852A");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks).HasConstraintName("FK__Tasks__ProjectId__534D60F1");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Tasks).HasConstraintName("FK__Tasks__Status__52593CB8");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CD8CDE2CE");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A354CFA1992");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles).HasConstraintName("FK__UserRoles__RoleI__3F466844");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles).HasConstraintName("FK__UserRoles__UserI__3E52440B");
        });

    }

}
