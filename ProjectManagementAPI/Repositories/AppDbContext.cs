using EFCoreAPI.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAPI.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        public DbSet<Person> Persons => Set<Person>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Profile> Profiles => Set<Profile>();
        public DbSet<UserProfileRelation> UsersProfilesRelations => Set<UserProfileRelation>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Attachment> Attachments => Set<Attachment>();
        public DbSet<ProjectAttachmentRelation> ProjectsAttachmentsRelations => Set<ProjectAttachmentRelation>();
        public DbSet<Status> Statuses => Set<Status>();
        public DbSet<TaskModel> Tasks => Set<TaskModel>();
        public DbSet<TaskUserRelation> TasksUsersRelations => Set<TaskUserRelation>();
        public DbSet<Comment> Comments => Set<Comment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --------------------
            // Persons
            // --------------------
            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Persons");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.LastName)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.IsActive)
                      .IsRequired();

                entity.HasOne(e => e.User)
                      .WithOne(u => u.Person)
                      .HasForeignKey<User>(u => u.PersonId);
            });

            // --------------------
            // Users
            // --------------------
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.DomainName)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e=>e.Salt)
                    .HasMaxLength(36)
                    .IsRequired();

                entity.Property(e => e.Password)
                      .HasMaxLength(256)
                      .IsRequired();

                entity.Property(e => e.Email)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.IsActive)
                      .IsRequired();

                entity.HasIndex(e => e.PersonId)
                      .IsUnique();
            });

            // --------------------
            // Profiles
            // --------------------
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profiles");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.IsActive)
                      .IsRequired();
            });

            // --------------------
            // UsersProfilesRelation
            // --------------------
            modelBuilder.Entity<UserProfileRelation>(entity =>
            {
                entity.ToTable("UsersProfilesRelation");
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => new { e.UserId, e.ProfileId })
                      .IsUnique();

                entity.HasIndex(e => e.ProfileId);

                entity.Property(e => e.IsActive)
                      .IsRequired();

                entity.HasOne(e => e.User)
                      .WithMany(u => u.UserProfiles)
                      .HasForeignKey(e => e.UserId);

                entity.HasOne(e => e.Profile)
                      .WithMany(p => p.UserProfiles)
                      .HasForeignKey(e => e.ProfileId);
            });

            // --------------------
            // Projects
            // --------------------
            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Projects");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(e => e.CreatedDate)
                      .IsRequired();

                entity.Property(e => e.IsActive)
                      .IsRequired();
            });

            // --------------------
            // Attachments
            // --------------------
            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("Attachments");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.DisplayName)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(e => e.Url)
                      .HasMaxLength(500)
                      .IsRequired();

                entity.Property(e => e.CreatedDate)
                      .IsRequired();

                entity.Property(e => e.IsActive)
                      .IsRequired();
            });

            // --------------------
            // ProjectsAttachmentsRelation
            // --------------------
            modelBuilder.Entity<ProjectAttachmentRelation>(entity =>
            {
                entity.ToTable("ProjectsAttachmentsRelation");
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => new { e.ProjectId, e.AttachmentId })
                      .IsUnique();

                entity.HasOne(e => e.Project)
                      .WithMany(p => p.ProjectAttachments)
                      .HasForeignKey(e => e.ProjectId);

                entity.HasOne(e => e.Attachment)
                      .WithMany(a => a.ProjectAttachments)
                      .HasForeignKey(e => e.AttachmentId);
            });

            // --------------------
            // Status
            // --------------------
            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Description)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.IsActive)
                      .IsRequired();
            });

            // --------------------
            // Tasks
            // --------------------
            modelBuilder.Entity<TaskModel>(entity =>
            {
                entity.ToTable("Tasks");
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.ProjectId);
                entity.HasIndex(e => e.StatusId);

                entity.Property(e => e.Title)
                      .HasMaxLength(500)
                      .IsRequired();

                entity.Property(e => e.Description)
                      .IsRequired();

                entity.Property(e => e.CreatedDate)
                      .IsRequired();

                entity.Property(e => e.IsActive)
                      .IsRequired();

                entity.HasOne(e => e.Project)
                      .WithMany(p => p.Tasks)
                      .HasForeignKey(e => e.ProjectId);

                entity.HasOne(e => e.Status)
                      .WithMany(s => s.Tasks)
                      .HasForeignKey(e => e.StatusId);
            });

            // --------------------
            // TasksUsersRelation
            // --------------------
            modelBuilder.Entity<TaskUserRelation>(entity =>
            {
                entity.ToTable("TasksUsersRelation");
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => new { e.TaskId, e.UserId })
                      .IsUnique();

                entity.HasIndex(e => e.UserId);

                entity.HasOne(e => e.Task)
                      .WithMany(t => t.TaskUsers)
                      .HasForeignKey(e => e.TaskId);

                entity.HasOne(e => e.User)
                      .WithMany(u => u.TaskUsers)
                      .HasForeignKey(e => e.UserId);
            });

            // --------------------
            // Comments
            // --------------------
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comments");
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.TaskId);
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Description)
                      .IsRequired();

                entity.Property(e => e.CreatedDate)
                      .IsRequired();

                entity.Property(e => e.IsActive)
                      .IsRequired();

                entity.HasOne(e => e.Task)
                      .WithMany(t => t.Comments)
                      .HasForeignKey(e => e.TaskId);

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Comments)
                      .HasForeignKey(e => e.UserId);
            });
        }
    }
}
