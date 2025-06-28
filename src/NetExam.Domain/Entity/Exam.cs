using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Domain.Entity;

public class Exam
{
    public long Id { get; set; }

    public string Title { get; set; }
    public string Subject { get; set; }

    public DateTime CreatedAt { get; set; }

    public long CreatedByUserId { get; set; }
    public virtual User CreatedBy { get; set; }

    public virtual ICollection<ExamQuestion> ExamQuestions { get; set; } 
    public virtual ICollection<ExamResult> Results { get; set; }
}
