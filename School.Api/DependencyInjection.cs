using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces.IUnitOfWork;
using School.Infrastructure.Implementation.UnitOfWork;
using School.Infrastructure.Data;
namespace School.Api;

public static class DependencyInjection
{
	public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
	{
		var ConnectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
		services.AddDbContext<ApplicationDbContext>(option =>
		option.UseSqlServer(ConnectionString)
		);

		services.AddScoped<IUnitOfWork,UnitOfWork>();
		return services;
	}
}
