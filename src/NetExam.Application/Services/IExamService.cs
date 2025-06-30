using NetExam.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public interface IExamService
{
    Task<IEnumerable<ExamReadDto>> GetAllAsync();
    Task<ExamReadDto?> GetByIdAsync(long id);
    Task<IEnumerable<ExamReadDto>> GetByCreatorIdAsync(long creatorId);
    Task AddAsync(ExamCreateDto dto);
    Task UpdateAsync(ExamCreateDto dto, long id);
    Task DeleteAsync(long id);
}
