using NetExam.Application.Dtos;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public class ExamResultService : IExamResultService
{
    private readonly IExamResultRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IExamRepository _examRepository;

    public ExamResultService(
        IExamResultRepository repository,
        IUserRepository userRepository,
        IExamRepository examRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _examRepository = examRepository;
    }

    public async Task<IEnumerable<ExamResultDto>> GetByUserIdAsync(long userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        var results = await _repository.GetByUserIdAsync(userId);

        return results.Select(r => new ExamResultDto
        {
            Id = r.Id,
            Score = r.Score,
            CompletedAt = r.CompletedAt,
            ExamTitle = r.Exam?.Title ?? "",
            UserFullName = user?.FullName ?? ""
        });
    }

    public async Task<IEnumerable<ExamResultDto>> GetByExamIdAsync(long examId)
    {
        var exam = await _examRepository.GetByIdAsync(examId);
        var results = await _repository.GetByExamIdAsync(examId);

        return results.Select(r => new ExamResultDto
        {
            Id = r.Id,
            Score = r.Score,
            CompletedAt = r.CompletedAt,
            ExamTitle = exam?.Title ?? "",
            UserFullName = r.User?.FullName ?? ""
        });
    }

    public async Task<IEnumerable<ExamResultDto>> GetByExamTitleAsync(string examTitle)
    {
        var results = await _repository.GetByExamTitleAsync(examTitle);
        return results.Select(r => new ExamResultDto
        {
            Id = r.Id,
            Score = r.Score,
            CompletedAt = r.CompletedAt,
            ExamTitle = r.Exam?.Title ?? "",
            UserFullName = r.User?.FullName ?? ""
        });
    }

    public async Task<double> GetAverageScoreAsync(long examId)
    {
        return await _repository.GetAverageScoreAsync(examId);
    }

    public async Task AddAsync(ExamResultDto dto)
    {
        var entity = new ExamResult
        {
            ExamId = dto.ExamId,
            UserId = dto.UserId,
            Score = dto.Score,
            CompletedAt = dto.CompletedAt
        };

        await _repository.AddAsync(entity);
    }
}
