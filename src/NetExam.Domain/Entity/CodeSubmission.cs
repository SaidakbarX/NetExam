using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Domain.Entity;

public class CodeSubmission
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long CodeQuestionId { get; set; }

    public string Code { get; set; }

    public int ProgrammingLanguageId { get; set; }
    public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }

    public bool IsSuccess { get; set; }
    public int PassedTestCount { get; set; }
    public int TotalTestCount { get; set; }

    public string ResultLog { get; set; }
    public DateTime SubmittedAt { get; set; }

    public virtual User User { get; set; }
    public virtual CodeQuestion CodeQuestion { get; set; }
}
