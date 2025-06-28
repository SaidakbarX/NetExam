using FluentValidation;
using NetExam.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Validators;

public class CodeQuestionDtoValidator : AbstractValidator<CodeQuestionDto>
{
    public CodeQuestionDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.FunctionName).NotEmpty();
    }
}