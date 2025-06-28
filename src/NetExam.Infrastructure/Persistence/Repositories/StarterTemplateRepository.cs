using Microsoft.EntityFrameworkCore;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Infrastructure.Persistence.Repositories;

public class StarterTemplateRepository : IStarterTemplateRepository
{
    private readonly AppDbContext _context;

    public StarterTemplateRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<StarterTemplate?> GetByIdAsync(long id)
    {
        return await _context.StarterTemplates
            .Include(st => st.CodeQuestion)
            .Include(st => st.ProgrammingLanguage)
            .FirstOrDefaultAsync(st => st.Id == id);
    }

    public async Task<IEnumerable<StarterTemplate>> GetByQuestionIdAsync(long questionId)
    {
        return await _context.StarterTemplates
            .Where(st => st.CodeQuestionId == questionId)
            .ToListAsync();
    }

    public async Task AddAsync(StarterTemplate template)
    {
        await _context.StarterTemplates.AddAsync(template);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(StarterTemplate template)
    {
        _context.StarterTemplates.Update(template);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await _context.StarterTemplates.FindAsync(id);
        if (entity != null)
        {
            _context.StarterTemplates.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
