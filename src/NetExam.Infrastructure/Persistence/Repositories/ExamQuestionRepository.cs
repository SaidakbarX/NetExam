using Microsoft.EntityFrameworkCore;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Infrastructure.Persistence.Repositories;

public class ExamQuestionRepository : IExamQuestionRepository
{
    private readonly AppDbContext _context;

    public ExamQuestionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExamQuestion>> GetByExamIdAsync(long examId)
    {
        return await _context.ExamQuestions
            .Include(eq => eq.CodeQuestion)
            .Where(eq => eq.ExamId == examId)
            .OrderBy(eq => eq.Order)
            .ToListAsync();
    }

    public async Task AddAsync(ExamQuestion examQuestion)
    {
        await _context.ExamQuestions.AddAsync(examQuestion);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(long examId, long questionId)
    {
        var entity = await _context.ExamQuestions
            .FirstOrDefaultAsync(eq => eq.ExamId == examId && eq.CodeQuestionId == questionId);

        if (entity is not null)
        {
            _context.ExamQuestions.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateOrderAsync(long examId, long questionId, int newOrder)
    {
        var entity = await _context.ExamQuestions
            .FirstOrDefaultAsync(eq => eq.ExamId == examId && eq.CodeQuestionId == questionId);

        if (entity is not null)
        {
            entity.Order = newOrder;
            _context.ExamQuestions.Update(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task ReorderAsync(long examId, Dictionary<long, int> newOrders)
    {
        var questions = await _context.ExamQuestions
            .Where(eq => eq.ExamId == examId)
            .ToListAsync();

        foreach (var q in questions)
        {
            if (newOrders.TryGetValue(q.CodeQuestionId, out var newOrder))
            {
                q.Order = newOrder;
            }
        }

        _context.ExamQuestions.UpdateRange(questions);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ExamQuestion examQuestion)
    {
        var existing = await _context.ExamQuestions
            .FirstOrDefaultAsync(eq => eq.ExamId == examQuestion.ExamId && eq.CodeQuestionId == examQuestion.CodeQuestionId);

        if (existing is null)
            throw new InvalidOperationException("ExamQuestion not found.");

        existing.Order = examQuestion.Order;

        _context.ExamQuestions.Update(existing);
        await _context.SaveChangesAsync();
    }

}