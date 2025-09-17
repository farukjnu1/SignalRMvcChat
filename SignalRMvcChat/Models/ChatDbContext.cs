namespace SignalRMvcChat.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ChatDbContext : DbContext
    {
        public ChatDbContext()
            : base("name=ChatDbContext")
        {
        }

        public virtual DbSet<ChatGroup> ChatGroups { get; set; }
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }
        public virtual DbSet<ChatUser> ChatUsers { get; set; }
        public virtual DbSet<ChatUserGroup> ChatUserGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatGroup>()
                .Property(e => e.GroupName)
                .IsUnicode(false);

            modelBuilder.Entity<ChatMessage>()
                .Property(e => e.Sender)
                .IsUnicode(false);

            modelBuilder.Entity<ChatMessage>()
                .Property(e => e.Receiver)
                .IsUnicode(false);

            modelBuilder.Entity<ChatMessage>()
                .Property(e => e.GroupName)
                .IsUnicode(false);

            modelBuilder.Entity<ChatMessage>()
                .Property(e => e.FilePath)
                .IsUnicode(false);

            modelBuilder.Entity<ChatUser>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<ChatUserGroup>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<ChatUserGroup>()
                .Property(e => e.GroupName)
                .IsUnicode(false);
        }
    }
}
