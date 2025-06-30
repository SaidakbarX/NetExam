using FluentValidation;
using NetExam.Application.Dtos;
using NetExam.Application.Helpers;
using NetExam.Application.Interfaces;
using NetExam.Application.Services;
using NetExam.Application.Validators;
using NetExam.Infrastructure.Persistence.Repositories;

namespace NetExam.Api.Configurations;

public static class DependecyInjectionsConfiguration
{
    public static void ConfigureDependencies(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<ICodeQuestionService, CodeQuestionService>();
        services.AddScoped<ICodeSubmissionService, CodeSubmissionService>();
        services.AddScoped<IExamService, ExamService>();
        services.AddScoped<IExamQuestionService, ExamQuestionService>();
        services.AddScoped<IExamResultService, ExamResultService>();
        services.AddScoped<IProgrammingLanguageService, ProgrammingLanguageService>();
        services.AddScoped<IStarterTemplateService, StarterTemplateService>();
        services.AddScoped<ITestCaseService, TestCaseService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ICodeQuestionRepository, CodeQuestionRepository>();
        services.AddScoped<ICodeSubmissionRepository, CodeSubmissionRepository>();
        services.AddScoped<IExamRepository, ExamRepository>();
        services.AddScoped<IExamQuestionRepository, ExamQuestionRepository>();
        services.AddScoped<IExamResultRepository, ExamResultRepository>();
        services.AddScoped<IProgrammingLanguageRepository, ProgrammingLanguageRepository>();
        services.AddScoped<IStarterTemplateRepository, StarterTemplateRepository>();
        services.AddScoped<ITestCaseRepository, TestCaseRepository>();

        services.AddScoped<IValidator<CodeQuestionDto>, CodeQuestionDtoValidator>();
        services.AddScoped<IValidator<CodeSubmissionDto>, CodeSubmissionDtoValidator>();
        services.AddScoped<IValidator<ExamCreateDto>, ExamCreateDtoValidator>();
        services.AddScoped<IValidator<ExamQuestionDto>, ExamQuestionDtoValidator>();
        services.AddScoped<IValidator<ProgrammingLanguageDto>, ProgrammingLanguageDtoValidator>();
        services.AddScoped<IValidator<RefreshTokenDto>, RefreshTokenDtoValidator>();
        services.AddScoped<IValidator<StarterTemplateDto>, StarterTemplateDtoValidator>();
        services.AddScoped<IValidator<TestCaseDto>, TestCaseDtoValidator>();
        services.AddScoped<IValidator<UserCreateDto>, UserCreateDtoValidator>();
        services.AddScoped<IValidator<UserUpdateDto>, UserUpdateDtoValidator>();
    }
}
