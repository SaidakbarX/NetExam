using Microsoft.EntityFrameworkCore;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{

    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<long> AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user.Id;
    }
    public async Task<User?> GetUserByIdAsync(long id)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }

    public async Task<User?> GetUserByUserNameAsync(string userName)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.FullName.ToLower() == userName.ToLower());
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.Include(u => u.Role).ToListAsync();
    }

    public async Task<IEnumerable<User>> SearchUsersAsync(string keyword)
    {
        return await _context.Users
            .Where(u => u.FullName.Contains(keyword) || u.Email.Contains(keyword))
            .Include(u => u.Role)
            .ToListAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserRoleAsync(long userId, string userRole)
    {
        var user = await _context.Users.FindAsync(userId);
        var role = await _context.UserRoles.FirstOrDefaultAsync(r => r.Name == userRole);

        if (user != null && role != null)
        {
            user.RoleId = role.Id;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteUserByIdAsync(long userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> CheckUserByIdAsync(long userId)
    {
        return await _context.Users.AnyAsync(u => u.Id == userId);
    }

    public async Task<bool> CheckUsernameExistsAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.FullName.ToLower() == username.ToLower());
    }

    public async Task<long?> CheckEmailExistsAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        return user?.Id;
    }

    public async Task<bool> CheckPhoneNumberExistsAsync(string phoneNum)
    {
        return await _context.Users.AnyAsync(u => u.Email == phoneNum); 
    }
}

