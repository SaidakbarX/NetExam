using NetExam.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public interface ITestCaseService
{
    Task<IEnumerable<TestCaseDto>> GetByQuestionIdAsync(long questionId);
    Task<TestCaseDto?> GetByIdAsync(long id);
    Task AddAsync(TestCaseDto dto);
    Task UpdateAsync(TestCaseDto dto);
    Task DeleteAsync(long id);
}
