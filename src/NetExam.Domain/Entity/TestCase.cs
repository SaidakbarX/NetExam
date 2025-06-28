using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Domain.Entity;

public class TestCase
{
    public long Id { get; set; }
    public long CodeQuestionId { get; set; }

    public string Input { get; set; }
    public string ExpectedOutput { get; set; }

    public bool IsHidden { get; set; }
    public bool IsSample { get; set; } = false;
    public int Order { get; set; }

    public virtual CodeQuestion CodeQuestion { get; set; }
}
