using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Interfaces;

public interface IExamRepository
{
    Task<Exam?> GetByIdAsync(long id);
    Task<IEnumerable<Exam>> GetAllAsync();
    Task<IEnumerable<Exam>> GetByCreatorIdAsync(long creatorId);
    Task<Exam?> GetWithQuestionsAsync(long examId);

    Task AddAsync(Exam exam);
    Task UpdateAsync(Exam exam);
    Task DeleteAsync(long id);
}
