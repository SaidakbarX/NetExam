using Microsoft.EntityFrameworkCore;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
   

    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
    public DbSet<CodeQuestion> CodeQuestions { get; set; }
    public DbSet<StarterTemplate> StarterTemplates { get; set; }
    public DbSet<TestCase> TestCases { get; set; }
    public DbSet<CodeSubmission> CodeSubmissions { get; set; }

    public DbSet<Exam> Exams { get; set; }
    public DbSet<ExamQuestion> ExamQuestions { get; set; }
    public DbSet<ExamResult> ExamResults { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}

