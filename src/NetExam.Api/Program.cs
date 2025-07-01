
using NetExam.Api.Configurations;
using NetExam.Api.Entpoints;
using NetExam.Api.Middlewears;

namespace NetExam.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.ConfigureDataBase();
            builder.ConfigureSerilog();
            builder.ConfigureJwtSettings();
            builder.ConfigureDependencies();
            // Program.cs da
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

          
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.MapExamEndpoints();
            app.MapCodeQuestionEndpoints();
            app.MapCodeSubmissionEndpoints();
            app.MapExamResultEndpoints();
            app.MapStarterTemplateEndpoints();
            app.MapProgrammingLanguageEndpoints();
            app.MapTestCaseEndpoints();
            app.MapAuthEndpoints();
            app.MapUserEndpoints();
            app.UseCors("AllowReactApp");






            app.MapControllers();

            app.Run();
        }
    }
}
