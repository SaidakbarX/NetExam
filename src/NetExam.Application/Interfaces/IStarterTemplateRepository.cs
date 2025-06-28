using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Interfaces;

public interface IStarterTemplateRepository
{
    Task<StarterTemplate?> GetByIdAsync(long id);
    Task<IEnumerable<StarterTemplate>> GetByQuestionIdAsync(long questionId);

    Task AddAsync(StarterTemplate template);
    Task UpdateAsync(StarterTemplate template);
    Task DeleteAsync(long id);
}
