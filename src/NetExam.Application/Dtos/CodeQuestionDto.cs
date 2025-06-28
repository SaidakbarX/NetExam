using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Dtos;

public class CodeQuestionDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string FunctionName { get; set; }
    public DateTime CreatedAt { get; set; }
}
