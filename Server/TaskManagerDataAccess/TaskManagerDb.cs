namespace TaskManagerDataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TaskManagerDb : DbContext
    {
        public TaskManagerDb()
            : base("name=TaskManagerDbContext")
        {
        }

        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserProject> UserProject { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasMany(e => e.Role)
                .WithMany(e => e.Permission)
                .Map(m => m.ToTable("RolePermission").MapLeftKey("PermissionId").MapRightKey("RoleId"));

            modelBuilder.Entity<Task>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
