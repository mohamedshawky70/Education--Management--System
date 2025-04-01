using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace School.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
		{
			//Add Mapster
			var mappingConfig = TypeAdapterConfig.GlobalSettings;
			mappingConfig.Scan(Assembly.GetExecutingAssembly());

			//Add Fluent Validation [FluentValidation.AspNetCore]
			services.AddFluentValidationAutoValidation()
				.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			return services;
		}

	}
}
