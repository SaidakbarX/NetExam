using Microsoft.EntityFrameworkCore;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Infrastructure.Persistence.Repositories;

public class ExamRepository : IExamRepository
{
    private readonly AppDbContext _context;

    public ExamRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Exam?> GetByIdAsync(long id)
    {
        return await _context.Exams.FindAsync(id);
    }

    public async Task<IEnumerable<Exam>> GetAllAsync()
    {
        return await _context.Exams.ToListAsync();
    }

    public async Task<IEnumerable<Exam>> GetByCreatorIdAsync(long creatorId)
    {
        return await _context.Exams
            .Where(e => e.CreatedByUserId == creatorId)
            .ToListAsync();
    }

    public async Task<Exam?> GetWithQuestionsAsync(long examId)
    {
        return await _context.Exams
            .Include(e => e.ExamQuestions)
                .ThenInclude(eq => eq.CodeQuestion)
            .FirstOrDefaultAsync(e => e.Id == examId);
    }

    public async Task AddAsync(Exam exam)
    {
        await _context.Exams.AddAsync(exam);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Exam exam)
    {
        _context.Exams.Update(exam);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var exam = await _context.Exams.FindAsync(id);
        if (exam != null)
        {
            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
        }
    }
}