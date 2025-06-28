using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Interfaces;

public interface ICodeSubmissionRepository
{
    Task<CodeSubmission?> GetByIdAsync(long id);
    Task<IEnumerable<CodeSubmission>> GetByUserIdAsync(long userId);
    Task<IEnumerable<CodeSubmission>> GetByQuestionIdAsync(long questionId);
    Task<IEnumerable<CodeSubmission>> GetSuccessfulSubmissionsAsync(long userId, long questionId);

    Task<IEnumerable<CodeSubmission>> GetAllAsync();
    Task AddAsync(CodeSubmission submission);
}
