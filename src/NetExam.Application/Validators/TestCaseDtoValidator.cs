using FluentValidation;
using NetExam.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Validators;

public class TestCaseDtoValidator : AbstractValidator<TestCaseDto>
{
    public TestCaseDtoValidator()
    {
        RuleFor(x => x.Input).NotEmpty();
        RuleFor(x => x.ExpectedOutput).NotEmpty();
        RuleFor(x => x.Order).GreaterThanOrEqualTo(0);
    }
}

