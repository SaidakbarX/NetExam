using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Domain.Entity;

public class UserRole
{
    public long Id { get; set; }                 
    public string Name { get; set; }             
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } 

    public virtual ICollection<User> Users { get; set; }
}
