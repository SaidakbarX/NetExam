using NetExam.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public interface ICodeSubmissionService
{
    Task<IEnumerable<CodeSubmissionDto>> GetAllAsync();
    Task<CodeSubmissionDto?> GetByIdAsync(long id);
    Task<IEnumerable<CodeSubmissionDto>> GetByUserIdAsync(long userId);
    Task<IEnumerable<CodeSubmissionDto>> GetByQuestionIdAsync(long questionId);
    Task<IEnumerable<CodeSubmissionDto>> GetSuccessfulSubmissionsAsync(long userId, long questionId);
    Task AddAsync(CodeSubmissionDto dto);
}