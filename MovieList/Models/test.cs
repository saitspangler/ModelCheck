using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MovieList.Models
{        public class User
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public List<UserGroup> UserGroups { get; set; }
        }

        public class Group
        {
            public int GroupId { get; set; }
            public string GroupName { get; set; }
            public List<UserGroup> UserGroups { get; set; }
        }

        public class UserGroup
        {
            public int UserId { get; set; }
            public User User { get; set; }
            public int GroupId { get; set; }
            public Group Group { get; set; }
        }

        public class Task
        {
            public int TaskId { get; set; }
            public string Description { get; set; }
            public int Priority { get; set; }
            public byte[] Picture { get; set; }
            public int AssignedBy { get; set; }
            public User AssignedByUser { get; set; }
            public int AssignedTo { get; set; }
            public User AssignedToUser { get; set; }
        }

        public class MyDbContext : DbContext
        {
            public DbSet<User> Users { get; set; }
            public DbSet<Group> Groups { get; set; }
            public DbSet<UserGroup> UserGroups { get; set; }
            public DbSet<Task> Tasks { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source=mydatabase.db");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<UserGroup>()
                    .HasKey(ug => new { ug.UserId, ug.GroupId });
                modelBuilder.Entity<Task>()
                    .HasOne(t => t.AssignedByUser)
                    .WithMany()
                    .HasForeignKey(t => t.AssignedBy);
                modelBuilder.Entity<Task>()
                    .HasOne(t => t.AssignedToUser)
                    .WithMany()
                    .HasForeignKey(t => t.AssignedTo);
            }
        }
    }
}
}
