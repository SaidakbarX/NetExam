using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Dtos;

public class ExamCreateDto
{
    public string Title { get; set; }
    public string Subject { get; set; }
    public long CreatedByUserId { get; set; }
}
