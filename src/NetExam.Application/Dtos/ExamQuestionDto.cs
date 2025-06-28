using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Dtos;

public class ExamQuestionDto
{
    public long ExamId { get; set; }
    public long CodeQuestionId { get; set; }
    public int Order { get; set; }
}