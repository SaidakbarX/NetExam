using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Dtos;

public class StarterTemplateDto
{
    public long Id { get; set; }
    public long CodeQuestionId { get; set; }
    public int ProgrammingLanguageId { get; set; }
    public string Code { get; set; }
}
