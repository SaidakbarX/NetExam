using Microsoft.EntityFrameworkCore;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Infrastructure.Persistence.Repositories;

public class ExamResultRepository : IExamResultRepository
{
    private readonly AppDbContext _context;

    public ExamResultRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ExamResult?> GetByIdAsync(long id)
    {
        return await _context.ExamResults
            .Include(er => er.User)
            .Include(er => er.Exam)
            .FirstOrDefaultAsync(er => er.Id == id);
    }

    public async Task<IEnumerable<ExamResult>> GetByUserIdAsync(long userId)
    {
        return await _context.ExamResults
            .Where(er => er.UserId == userId)
            .Include(er => er.Exam)
            .ToListAsync();
    }

    public async Task<IEnumerable<ExamResult>> GetByExamIdAsync(long examId)
    {
        return await _context.ExamResults
            .Where(er => er.ExamId == examId)
            .Include(er => er.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<ExamResult>> GetByExamTitleAsync(string examTitle)
    {
        return await _context.ExamResults
            .Include(er => er.Exam)
            .Include(er => er.User)
            .Where(er => er.Exam.Title.ToLower().Contains(examTitle.ToLower()))
            .ToListAsync();
    }

    public async Task<ExamResult?> GetByUserAndExamAsync(long userId, long examId)
    {
        return await _context.ExamResults
            .FirstOrDefaultAsync(er => er.UserId == userId && er.ExamId == examId);
    }

    public async Task<double> GetAverageScoreAsync(long examId)
    {
        return await _context.ExamResults
            .Where(er => er.ExamId == examId)
            .AverageAsync(er => er.Score);
    }

    public async Task AddAsync(ExamResult result)
    {
        await _context.ExamResults.AddAsync(result);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ExamResult result)
    {
        _context.ExamResults.Update(result);
        await _context.SaveChangesAsync();
    }
}