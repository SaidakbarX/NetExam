using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Dtos;

public class TestCaseDto
{
    public long Id { get; set; }
    public string Input { get; set; }
    public string ExpectedOutput { get; set; }
    public bool IsHidden { get; set; }
    public bool IsSample { get; set; }
    public int Order { get; set; }
}