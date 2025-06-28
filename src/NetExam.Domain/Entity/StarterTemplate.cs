using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Domain.Entity;

public class StarterTemplate
{
    public long Id { get; set; }
    public long CodeQuestionId { get; set; }
    public int ProgrammingLanguageId { get; set; }

    public string Code { get; set; }

    public virtual CodeQuestion CodeQuestion { get; set; }
    public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }
}
