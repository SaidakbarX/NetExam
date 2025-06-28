using Microsoft.EntityFrameworkCore;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(long userId)
    {
        return await _context.RefreshTokens
            .Where(rt => rt.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAsync(RefreshToken token)
    {
        await _context.RefreshTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }

    public async Task RevokeAsync(string token)
    {
        var existing = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
        if (existing != null)
        {
            existing.IsRevoked = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteExpiredTokensAsync()
    {
        var expired = await _context.RefreshTokens
            .Where(rt => rt.Expires < DateTime.UtcNow || rt.IsRevoked)
            .ToListAsync();

        _context.RefreshTokens.RemoveRange(expired);
        await _context.SaveChangesAsync();
    }
}