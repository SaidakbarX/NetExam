using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Dtos;

public class ExamResultDto
{
    public long Id { get; set; }
    public string UserFullName { get; set; }
    public string ExamTitle { get; set; }
    public double Score { get; set; }
    public DateTime CompletedAt { get; set; }
}