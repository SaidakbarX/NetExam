using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Domain.Entity;

public class ExamResult
{
    public long Id { get; set; }
    public long ExamId { get; set; }
    public long UserId { get; set; }

    public double Score { get; set; }

    public DateTime CompletedAt { get; set; }

    public virtual Exam Exam { get; set; }
    public virtual User User { get; set; }
}
