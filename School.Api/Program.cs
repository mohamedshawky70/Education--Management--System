using School.Api;
using School.Application;
using School.Application.SeedRoles;
using School.Infrastructure;
using Serilog;
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddApiDependencies(builder.Configuration);
builder.Services.AddApplicationDependencies();
builder.Services.AddInfrastructureDependencies();

//Add logging
builder.Host.UseSerilog((context, configuration) =>
	configuration.ReadFrom.Configuration(context.Configuration)
);


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
//Use logging
app.UseSerilogRequestLogging();

//Use CORS
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//Add ExceptionHandler
app.UseExceptionHandler();
//Add rete limiting
app.UseRateLimiter();
var scopFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using var scop = scopFactory.CreateScope();
var rolManager = scop.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var userManager = scop.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
await DefaultRoles.SeedRolesAsync(rolManager);
await DefaultUser.SeedAdminUserAsync(userManager);

app.Run();
