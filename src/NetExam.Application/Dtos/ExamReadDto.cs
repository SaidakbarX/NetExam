using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Dtos;

public class ExamReadDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Subject { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatorFullName { get; set; }
}