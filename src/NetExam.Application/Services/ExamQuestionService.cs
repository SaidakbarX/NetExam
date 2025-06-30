using NetExam.Application.Dtos;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public class ExamQuestionService : IExamQuestionService
{
    private readonly IExamQuestionRepository _repository;

    public ExamQuestionService(IExamQuestionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ExamQuestionDto>> GetByExamIdAsync(long examId)
    {
        var entities = await _repository.GetByExamIdAsync(examId);
        return entities.Select(e => new ExamQuestionDto
        {
            ExamId = e.ExamId,
            CodeQuestionId = e.CodeQuestionId,
            Order = e.Order
        });
    }

    public async Task AddAsync(ExamQuestionDto dto)
    {
        var entity = new ExamQuestion
        {
            ExamId = dto.ExamId,
            CodeQuestionId = dto.CodeQuestionId,
            Order = dto.Order
        };

        await _repository.AddAsync(entity);
    }

    public async Task RemoveAsync(long examId, long questionId)
    {
        await _repository.RemoveAsync(examId, questionId);
    }

    public async Task UpdateAsync(ExamQuestionDto dto)
    {
        var entity = new ExamQuestion
        {
            ExamId = dto.ExamId,
            CodeQuestionId = dto.CodeQuestionId,
            Order = dto.Order
        };

        await _repository.UpdateAsync(entity);
    }
}
