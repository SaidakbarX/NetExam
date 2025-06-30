using NetExam.Application.Dtos;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public class TestCaseService : ITestCaseService
{
    private readonly ITestCaseRepository _repository;

    public TestCaseService(ITestCaseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TestCaseDto>> GetByQuestionIdAsync(long questionId)
    {
        var list = await _repository.GetByQuestionIdAsync(questionId);
        return list.Select(x => new TestCaseDto
        {
            Id = x.Id,
            Input = x.Input,
            ExpectedOutput = x.ExpectedOutput,
            IsHidden = x.IsHidden,
            IsSample = x.IsSample,
            Order = x.Order
        });
    }

    public async Task<TestCaseDto?> GetByIdAsync(long id)
    {
        var x = await _repository.GetByIdAsync(id);
        return x == null ? null : new TestCaseDto
        {
            Id = x.Id,
            Input = x.Input,
            ExpectedOutput = x.ExpectedOutput,
            IsHidden = x.IsHidden,
            IsSample = x.IsSample,
            Order = x.Order
        };
    }

    public async Task AddAsync(TestCaseDto dto)
    {
        var entity = new TestCase
        {
            CodeQuestionId = 0, // Set this based on your use case
            Input = dto.Input,
            ExpectedOutput = dto.ExpectedOutput,
            IsHidden = dto.IsHidden,
            IsSample = dto.IsSample,
            Order = dto.Order
        };
        await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(TestCaseDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null) throw new Exception("Not found");
        existing.Input = dto.Input;
        existing.ExpectedOutput = dto.ExpectedOutput;
        existing.IsHidden = dto.IsHidden;
        existing.IsSample = dto.IsSample;
        existing.Order = dto.Order;
        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
}

