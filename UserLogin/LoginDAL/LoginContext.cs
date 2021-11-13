using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LoginDAL
{
    public partial class LoginContext : DbContext
    {
        public LoginContext()
        {
        }

        public LoginContext(DbContextOptions<LoginContext> options)
            : base(options)
        {
        }

        public virtual DbSet<bankaccounts> bankaccounts { get; set; }
        public virtual DbSet<users> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\DB\\UserDb.mdf;Integrated Security=True;Connect Timeout=30;");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<bankaccounts>(entity =>
            {
                entity.Property(e => e.balance)
                    .HasColumnType("decimal(9, 2)")
                    .HasDefaultValueSql("((0.0))");

                entity.Property(e => e.timer)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.customer)
                    .WithMany(p => p.bankaccounts)
                    .HasForeignKey(d => d.customerid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__bankaccou__custo__267ABA7A");
            });

            modelBuilder.Entity<users>(entity =>
            {
                entity.Property(e => e.dob).HasColumnType("date");

                entity.Property(e => e.email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.pass)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.timer)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
