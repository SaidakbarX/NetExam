using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Interfaces;

public interface IUserRepository
{
    Task<long> AddUserAsync(User user);

    Task<User?> GetUserByIdAsync(long id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByUserNameAsync(string userName);

    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<IEnumerable<User>> SearchUsersAsync(string keyword);

    Task UpdateUserAsync(User user);
    Task UpdateUserRoleAsync(long userId, string userRole);

    Task DeleteUserByIdAsync(long userId);

    Task<bool> CheckUserByIdAsync(long userId);
    Task<bool> CheckUsernameExistsAsync(string username);
    Task<long?> CheckEmailExistsAsync(string email);
    Task<bool> CheckPhoneNumberExistsAsync(string phoneNum);
}
