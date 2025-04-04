using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using School.Application.ExceptionHandler;
using System.Reflection;

namespace School.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
		{

			#pragma warning disable EXTEX0018
			services.AddHybridCache();
			#pragma warning restore EXTEX0018

			return services;
		}

	}
}
