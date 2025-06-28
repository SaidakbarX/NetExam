using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Domain.Entity;

public class RefreshToken
{
    public long Id { get; set; }

    public string Token { get; set; }

    public DateTime CreatedAt { get; set; } 
    public DateTime Expires { get; set; }

    public bool IsRevoked { get; set; }

    public long UserId { get; set; }
    public virtual User User { get; set; }
}
