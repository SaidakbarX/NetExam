using FluentValidation;
using NetExam.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Validators;

public class CodeSubmissionDtoValidator : AbstractValidator<CodeSubmissionDto>
{
    public CodeSubmissionDtoValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.CodeQuestionId).GreaterThan(0);
        RuleFor(x => x.ProgrammingLanguageId).GreaterThan(0);
    }
}