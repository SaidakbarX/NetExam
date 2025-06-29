namespace NetExam.Domain.Entity;




public class User
{
    public long Id { get; set; }

    public string FullName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public long RoleId { get; set; }
    public string Salt { get; set; }
    public virtual UserRole Role { get; set; }

    public DateTime RegisteredAt { get; set; }

    public virtual ICollection<CodeSubmission> Submissions { get; set; }
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
}




