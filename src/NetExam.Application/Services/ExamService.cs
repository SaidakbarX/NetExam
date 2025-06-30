using NetExam.Application.Dtos;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public class ExamService : IExamService
{
    private readonly IExamRepository _repository;
    private readonly IUserRepository _userRepository;

    public ExamService(IExamRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<ExamReadDto>> GetAllAsync()
    {
        var exams = await _repository.GetAllAsync();
        var result = new List<ExamReadDto>();

        foreach (var exam in exams)
        {
            var user = await _userRepository.GetUserByIdAsync(exam.CreatedByUserId);
            result.Add(new ExamReadDto
            {
                Id = exam.Id,
                Title = exam.Title,
                Subject = exam.Subject,
                CreatedAt = exam.CreatedAt,
                CreatorFullName = user?.FullName ?? "Unknown"
            });
        }

        return result;
    }

    public async Task<ExamReadDto?> GetByIdAsync(long id)
    {
        var exam = await _repository.GetByIdAsync(id);
        if (exam == null) return null;

        var user = await _userRepository.GetUserByIdAsync(exam.CreatedByUserId);

        return new ExamReadDto
        {
            Id = exam.Id,
            Title = exam.Title,
            Subject = exam.Subject,
            CreatedAt = exam.CreatedAt,
            CreatorFullName = user?.FullName ?? "Unknown"
        };
    }

    public async Task<IEnumerable<ExamReadDto>> GetByCreatorIdAsync(long creatorId)
    {
        var exams = await _repository.GetByCreatorIdAsync(creatorId);
        var user = await _userRepository.GetUserByIdAsync(creatorId);

        return exams.Select(e => new ExamReadDto
        {
            Id = e.Id,
            Title = e.Title,
            Subject = e.Subject,
            CreatedAt = e.CreatedAt,
            CreatorFullName = user?.FullName ?? "Unknown"
        });
    }

    public async Task AddAsync(ExamCreateDto dto)
    {
        var entity = new Exam
        {
            Title = dto.Title,
            Subject = dto.Subject,
            CreatedByUserId = dto.CreatedByUserId,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(ExamCreateDto dto, long id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) throw new Exception("Exam not found");

        existing.Title = dto.Title;
        existing.Subject = dto.Subject;

        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
}