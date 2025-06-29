using NetExam.Application.Dtos;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public class CodeSubmissionService : ICodeSubmissionService
{
    private readonly ICodeSubmissionRepository _repository;

    public CodeSubmissionService(ICodeSubmissionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CodeSubmissionDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => new CodeSubmissionDto
        {
            Id = e.Id,
            UserId = e.UserId,
            CodeQuestionId = e.CodeQuestionId,
            ProgrammingLanguageId = e.ProgrammingLanguageId,
            Code = e.Code,
            IsSuccess = e.IsSuccess,
            PassedTestCount = e.PassedTestCount,
            TotalTestCount = e.TotalTestCount,
            ResultLog = e.ResultLog,
            SubmittedAt = e.SubmittedAt
        });
    }

    public async Task<CodeSubmissionDto?> GetByIdAsync(long id)
    {
        var e = await _repository.GetByIdAsync(id);
        if (e == null) return null;

        return new CodeSubmissionDto
        {
            Id = e.Id,
            UserId = e.UserId,
            CodeQuestionId = e.CodeQuestionId,
            ProgrammingLanguageId = e.ProgrammingLanguageId,
            Code = e.Code,
            IsSuccess = e.IsSuccess,
            PassedTestCount = e.PassedTestCount,
            TotalTestCount = e.TotalTestCount,
            ResultLog = e.ResultLog,
            SubmittedAt = e.SubmittedAt
        };
    }

    public async Task<IEnumerable<CodeSubmissionDto>> GetByUserIdAsync(long userId)
    {
        var list = await _repository.GetByUserIdAsync(userId);
        return list.Select(e => new CodeSubmissionDto
        {
            Id = e.Id,
            UserId = e.UserId,
            CodeQuestionId = e.CodeQuestionId,
            ProgrammingLanguageId = e.ProgrammingLanguageId,
            Code = e.Code,
            IsSuccess = e.IsSuccess,
            PassedTestCount = e.PassedTestCount,
            TotalTestCount = e.TotalTestCount,
            ResultLog = e.ResultLog,
            SubmittedAt = e.SubmittedAt
        });
    }

    public async Task<IEnumerable<CodeSubmissionDto>> GetByQuestionIdAsync(long questionId)
    {
        var list = await _repository.GetByQuestionIdAsync(questionId);
        return list.Select(e => new CodeSubmissionDto
        {
            Id = e.Id,
            UserId = e.UserId,
            CodeQuestionId = e.CodeQuestionId,
            ProgrammingLanguageId = e.ProgrammingLanguageId,
            Code = e.Code,
            IsSuccess = e.IsSuccess,
            PassedTestCount = e.PassedTestCount,
            TotalTestCount = e.TotalTestCount,
            ResultLog = e.ResultLog,
            SubmittedAt = e.SubmittedAt
        });
    }

    public async Task<IEnumerable<CodeSubmissionDto>> GetSuccessfulSubmissionsAsync(long userId, long questionId)
    {
        var list = await _repository.GetSuccessfulSubmissionsAsync(userId, questionId);
        return list.Select(e => new CodeSubmissionDto
        {
            Id = e.Id,
            UserId = e.UserId,
            CodeQuestionId = e.CodeQuestionId,
            ProgrammingLanguageId = e.ProgrammingLanguageId,
            Code = e.Code,
            IsSuccess = e.IsSuccess,
            PassedTestCount = e.PassedTestCount,
            TotalTestCount = e.TotalTestCount,
            ResultLog = e.ResultLog,
            SubmittedAt = e.SubmittedAt
        });
    }

    public async Task AddAsync(CodeSubmissionDto dto)
    {
        var entity = new CodeSubmission
        {
            UserId = dto.UserId,
            CodeQuestionId = dto.CodeQuestionId,
            ProgrammingLanguageId = dto.ProgrammingLanguageId,
            Code = dto.Code,
            IsSuccess = dto.IsSuccess,
            PassedTestCount = dto.PassedTestCount,
            TotalTestCount = dto.TotalTestCount,
            ResultLog = dto.ResultLog,
            SubmittedAt = dto.SubmittedAt
        };

        await _repository.AddAsync(entity);
    }
}

