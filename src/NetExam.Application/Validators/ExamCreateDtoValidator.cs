using FluentValidation;
using NetExam.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Validators;

public class ExamCreateDtoValidator : AbstractValidator<ExamCreateDto>
{
    public ExamCreateDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Subject).NotEmpty().MaximumLength(100);
        RuleFor(x => x.CreatedByUserId).GreaterThan(0);
    }
}
