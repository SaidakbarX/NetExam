using Microsoft.EntityFrameworkCore;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Infrastructure.Persistence.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly AppDbContext _context;

    public UserRoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserRole?> GetByIdAsync(long id)
    {
        return await _context.UserRoles.FindAsync(id);
    }

    public async Task<UserRole?> GetByNameAsync(string name)
    {
        return await _context.UserRoles.FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower());
    }

    public async Task<IEnumerable<UserRole>> GetAllAsync()
    {
        return await _context.UserRoles.ToListAsync();
    }

    public async Task AddAsync(UserRole role)
    {
        await _context.UserRoles.AddAsync(role);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserRole role)
    {
        _context.UserRoles.Update(role);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var role = await _context.UserRoles.FindAsync(id);
        if (role != null)
        {
            _context.UserRoles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }
}

