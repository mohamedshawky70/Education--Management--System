using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces.IServices;
using School.Application.Interfaces.IUnitOfWork;
using School.Infrastructure.Data;
using School.Infrastructure.Implementation.Services;
using School.Infrastructure.Implementation.UnitOfWork;
namespace School.Api;

public static class DependencyInjection
{
	public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
	{
		var ConnectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
		services.AddDbContext<ApplicationDbContext>(option =>
		option.UseSqlServer(ConnectionString)
		);

		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IStudentService, StudentService>();
		services.AddScoped<IAssignmentService, AssignmentService>();
		services.AddScoped<ISubjectService, SubjectService>();
		services.AddScoped<IEnrollmentService, EnrollmentService>();
		services.AddScoped<IDepartmentService, DepartmentService>();

		return services;
	}
}
