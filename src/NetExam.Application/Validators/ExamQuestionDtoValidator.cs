using FluentValidation;
using NetExam.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Validators;

public class ExamQuestionDtoValidator : AbstractValidator<ExamQuestionDto>
{
    public ExamQuestionDtoValidator()
    {
        RuleFor(x => x.ExamId).GreaterThan(0);
        RuleFor(x => x.CodeQuestionId).GreaterThan(0);
        RuleFor(x => x.Order).GreaterThanOrEqualTo(0);
    }
}