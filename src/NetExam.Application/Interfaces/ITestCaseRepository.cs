using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Interfaces;

public interface ITestCaseRepository
{
    Task<TestCase?> GetByIdAsync(long id);
    Task<IEnumerable<TestCase>> GetByQuestionIdAsync(long questionId);

    Task AddAsync(TestCase testCase);
    Task UpdateAsync(TestCase testCase);
    Task DeleteAsync(long id);
}

