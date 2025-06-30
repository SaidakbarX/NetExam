using NetExam.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public interface IExamResultService
{
    Task<IEnumerable<ExamResultDto>> GetByUserIdAsync(long userId);
    Task<IEnumerable<ExamResultDto>> GetByExamIdAsync(long examId);
    Task<IEnumerable<ExamResultDto>> GetByExamTitleAsync(string examTitle);
    Task<double> GetAverageScoreAsync(long examId);
    Task AddAsync(ExamResultDto dto);
}
