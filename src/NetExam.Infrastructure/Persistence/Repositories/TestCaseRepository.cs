using Microsoft.EntityFrameworkCore;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;

namespace NetExam.Infrastructure.Persistence.Repositories;

public class TestCaseRepository : ITestCaseRepository
{
    private readonly AppDbContext _context;

    public TestCaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TestCase?> GetByIdAsync(long id)
    {
        return await _context.TestCases.FindAsync(id);
    }

    public async Task<IEnumerable<TestCase>> GetByQuestionIdAsync(long questionId)
    {
        return await _context.TestCases
            .Where(tc => tc.CodeQuestionId == questionId)
            .OrderBy(tc => tc.Order)
            .ToListAsync();
    }

    public async Task AddAsync(TestCase testCase)
    {
        await _context.TestCases.AddAsync(testCase);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TestCase testCase)
    {
        _context.TestCases.Update(testCase);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var tc = await _context.TestCases.FindAsync(id);
        if (tc != null)
        {
            _context.TestCases.Remove(tc);
            await _context.SaveChangesAsync();
        }
    }
}

