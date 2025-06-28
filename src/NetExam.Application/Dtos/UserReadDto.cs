using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Dtos;

public class UserReadDto
{
    public long Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string RoleName { get; set; }
    public DateTime RegisteredAt { get; set; }
}
