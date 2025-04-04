using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using School.Application.Interfaces.IAuthentication;
using School.Application.Interfaces.IServices;
using School.Application.Interfaces.IUnitOfWork;
using School.Application.Interfaces.IUser;
using School.Application.Settings;
using School.Domain.Entities;
using School.Infrastructure.Data;
using School.Infrastructure.Implementation.Authentication;
using School.Infrastructure.Implementation.Services;
using School.Infrastructure.Implementation.UnitOfWork;
using SurveyBasket.API.Services;
using System.Text;
namespace School.Api;

public static class DependencyInjection
{
	public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
	{
		var ConnectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
		services.AddDbContext<ApplicationDbContext>(option =>
		option.UseSqlServer(ConnectionString)
		);
		//Add Hangfire
		services.AddHangfire(x => x.UseSqlServerStorage(ConnectionString));
		services.AddHangfireServer();

		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IStudentService, StudentService>();
		services.AddScoped<IAssignmentService, AssignmentService>();
		services.AddScoped<ISubjectService, SubjectService>();
		services.AddScoped<IEnrollmentService, EnrollmentService>();
		services.AddScoped<IDepartmentService, DepartmentService>();
		services.AddScoped<IDepartmentSubjectService, DepartmentSubjectService>();
		services.AddScoped<ITeacherService, TeacherService>();
		services.AddSingleton<IJwtProvider, JwtProvider>();
		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IAccountService, AccountService>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IRoleService, RoleService>();

		//Add IdentityRole
		services.AddIdentity<ApplicationUser, IdentityRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();

		//Add IOptions
		services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
		//علشان يشغل الفليديشن علي الكلاس علشان ممكن اغلط وادخل قيم غير منطقيه زي وقت انتهاء التوكن بالسالب او مدخلهوش اصلا
		services.AddOptions<JwtSettings>().BindConfiguration(nameof(JwtSettings)).ValidateDataAnnotations();
		//مش هعرف اعمل انجكت هنا علشان ستاتك كلاس فالحل ده علشان اعرف اجيب قيم الكلاس من السكشن علشان استخدمهم هنا
		var settings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

		services.AddOpenApi();

		services.AddAuthentication(option =>
		{
			option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}
		)
		.AddJwtBearer(o =>
		{
			o.SaveToken = true;
			o.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings!.Key)),
				ValidIssuer = settings.Issuer,
				ValidAudience = settings.Audience,
			};
		});

		//Configure Identity
		services.Configure<IdentityOptions>(options =>
		{
			//default is 6
			options.Password.RequiredLength = 8;
			//default false
			options.SignIn.RequireConfirmedEmail = true;
			options.User.RequireUniqueEmail = true;

		});

		//Add email sender
		services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
		services.AddTransient<IEmailSender, EmailSender>();
		services.AddHttpContextAccessor();

		


		return services;
	}
}
