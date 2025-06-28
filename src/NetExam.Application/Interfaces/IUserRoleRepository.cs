using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Interfaces;

public interface IUserRoleRepository
{
    Task<UserRole> GetByIdAsync(long id);
    Task<UserRole> GetByNameAsync(string name);
    Task<IEnumerable<UserRole>> GetAllAsync();

    Task AddAsync(UserRole role);
    Task UpdateAsync(UserRole role);
    Task DeleteAsync(long id);
}
