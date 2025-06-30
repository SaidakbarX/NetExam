using NetExam.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public interface IUserRoleService
{
    Task<IEnumerable<UserRoleDto>> GetAllAsync();
    Task<UserRoleDto?> GetByIdAsync(long id);
    Task<UserRoleDto?> GetByNameAsync(string name);
    Task AddAsync(UserRoleDto dto);
    Task UpdateAsync(UserRoleDto dto);
    Task DeleteAsync(long id);
}
