using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Domain.Entity;

public class ExamQuestion
{
    public long ExamId { get; set; }
    public virtual Exam Exam { get; set; }

    public long CodeQuestionId { get; set; }
    public virtual CodeQuestion CodeQuestion { get; set; }

    public int Order { get; set; }
}
