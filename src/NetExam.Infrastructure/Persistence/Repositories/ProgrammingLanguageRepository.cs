using Microsoft.EntityFrameworkCore;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Infrastructure.Persistence.Repositories;

public class ProgrammingLanguageRepository : IProgrammingLanguageRepository
{
    private readonly AppDbContext _context;

    public ProgrammingLanguageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProgrammingLanguage?> GetByIdAsync(int id)
    {
        return await _context.ProgrammingLanguages.FindAsync(id);
    }

    public async Task<ProgrammingLanguage?> GetByNameAsync(string name)
    {
        return await _context.ProgrammingLanguages
            .FirstOrDefaultAsync(pl => pl.Name.ToLower() == name.ToLower());
    }

    public async Task<IEnumerable<ProgrammingLanguage>> GetAllAsync()
    {
        return await _context.ProgrammingLanguages.ToListAsync();
    }

    public async Task AddAsync(ProgrammingLanguage language)
    {
        await _context.ProgrammingLanguages.AddAsync(language);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProgrammingLanguage language)
    {
        _context.ProgrammingLanguages.Update(language);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var language = await _context.ProgrammingLanguages.FindAsync(id);
        if (language != null)
        {
            _context.ProgrammingLanguages.Remove(language);
            await _context.SaveChangesAsync();
        }
    }
}

