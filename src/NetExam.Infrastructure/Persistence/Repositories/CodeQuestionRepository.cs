using Microsoft.EntityFrameworkCore;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Infrastructure.Persistence.Repositories;

public class CodeQuestionRepository : ICodeQuestionRepository
{
    private readonly AppDbContext _context;

    public CodeQuestionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CodeQuestion?> GetByIdAsync(long id)
    {
        return await _context.CodeQuestions.FindAsync(id);
    }

    public async Task<CodeQuestion?> GetWithTestCasesAsync(long id)
    {
        return await _context.CodeQuestions
            .Include(q => q.TestCases)
            .Include(q => q.StarterTemplates)
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<CodeQuestion>> GetAllAsync()
    {
        return await _context.CodeQuestions.ToListAsync();
    }

    public async Task<IEnumerable<CodeQuestion>> SearchAsync(string keyword)
    {
        return await _context.CodeQuestions
            .Where(q => q.Title.Contains(keyword) || q.Description.Contains(keyword))
            .ToListAsync();
    }

    public async Task AddAsync(CodeQuestion question)
    {
        await _context.CodeQuestions.AddAsync(question);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CodeQuestion question)
    {
        _context.CodeQuestions.Update(question);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var question = await _context.CodeQuestions.FindAsync(id);
        if (question != null)
        {
            _context.CodeQuestions.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
}

