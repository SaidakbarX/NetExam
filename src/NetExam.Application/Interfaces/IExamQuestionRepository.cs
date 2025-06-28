using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Interfaces;

public interface IExamQuestionRepository
{
    Task<IEnumerable<ExamQuestion>> GetByExamIdAsync(long examId);
    Task AddAsync(ExamQuestion examQuestion);
    Task RemoveAsync(long examId, long questionId);
    Task UpdateAsync(ExamQuestion examQuestion);

}

