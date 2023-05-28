using Microsoft.EntityFrameworkCore;

namespace FamilyReportBook.Models;

public class ReportBookContext : DbContext
{
    public ReportBookContext(DbContextOptions<ReportBookContext> options) : base(options)
    { }

    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<Answer> Answers => Set<Answer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>()
            .HasMany(a => a.ReceivedAnswers)
            .WithOne(a => a.AnsweredToUser)
            .HasForeignKey(a => a.AnsweredToUserId);
        
        modelBuilder.Entity<AppUser>()
            .HasMany(a => a.GivenAnswers)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);
        
        base.OnModelCreating(modelBuilder);
    }
}
