using FluentValidation;
using NetExam.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Validators;

public class ProgrammingLanguageDtoValidator : AbstractValidator<ProgrammingLanguageDto>
{
    public ProgrammingLanguageDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}
