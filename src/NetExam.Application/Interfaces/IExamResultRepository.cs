using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Interfaces;

public interface IExamResultRepository
{
    Task<ExamResult?> GetByIdAsync(long id);
    Task<IEnumerable<ExamResult>> GetByUserIdAsync(long userId);
    Task<IEnumerable<ExamResult>> GetByExamIdAsync(long examId);
    Task<IEnumerable<ExamResult>> GetByExamTitleAsync(string examTitle);

    Task<double> GetAverageScoreAsync(long examId);

    Task AddAsync(ExamResult result);
}
