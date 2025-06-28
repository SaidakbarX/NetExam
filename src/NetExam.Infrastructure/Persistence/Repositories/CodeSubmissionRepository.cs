using Microsoft.EntityFrameworkCore;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Infrastructure.Persistence.Repositories;

public class CodeSubmissionRepository : ICodeSubmissionRepository
{
    private readonly AppDbContext _context;

    public CodeSubmissionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CodeSubmission?> GetByIdAsync(long id)
    {
        return await _context.CodeSubmissions
            .Include(cs => cs.User)
            .Include(cs => cs.CodeQuestion)
            .Include(cs => cs.ProgrammingLanguage)
            .FirstOrDefaultAsync(cs => cs.Id == id);
    }

    public async Task<IEnumerable<CodeSubmission>> GetByUserIdAsync(long userId)
    {
        return await _context.CodeSubmissions
            .Where(cs => cs.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<CodeSubmission>> GetByQuestionIdAsync(long questionId)
    {
        return await _context.CodeSubmissions
            .Where(cs => cs.CodeQuestionId == questionId)
            .ToListAsync();
    }

    public async Task<IEnumerable<CodeSubmission>> GetSuccessfulSubmissionsAsync(long userId, long questionId)
    {
        return await _context.CodeSubmissions
            .Where(cs => cs.UserId == userId && cs.CodeQuestionId == questionId && cs.IsSuccess)
            .ToListAsync();
    }

    public async Task<IEnumerable<CodeSubmission>> GetAllAsync()
    {
        return await _context.CodeSubmissions.ToListAsync();
    }

    public async Task AddAsync(CodeSubmission submission)
    {
        await _context.CodeSubmissions.AddAsync(submission);
        await _context.SaveChangesAsync();
    }
}

