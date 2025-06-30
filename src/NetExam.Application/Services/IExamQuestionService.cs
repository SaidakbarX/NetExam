using NetExam.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public interface IExamQuestionService
{
    Task<IEnumerable<ExamQuestionDto>> GetByExamIdAsync(long examId);
    Task AddAsync(ExamQuestionDto dto);
    Task RemoveAsync(long examId, long questionId);
    Task UpdateAsync(ExamQuestionDto dto);
}
