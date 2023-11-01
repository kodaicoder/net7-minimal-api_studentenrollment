using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentEnrollment.API.Configuration;
using StudentEnrollment.API.Services.Interfaces;
using StudentEnrollment.API.Services.Repositories;
using StudentEnrollment.Data;
using StudentEnrollment.Data.Interfaces;
using StudentEnrollment.Data.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add connection string to builder db context
string conn = builder.Configuration.GetConnectionString("StudentEnrollmentDbConnection")!;

builder.Services.AddDbContext<StudentEnrollmentDbContext>(options =>
{
	options.UseSqlServer(conn);
});
builder.Services.AddHttpContextAccessor();
//Add Scheme of Authentication
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	//Config JWT Bearer that was a scheme of authentication
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidateAudience = true,
			ValidAudience = builder.Configuration["Jwt:Audience"],
			ValidateLifetime = true,
			ClockSkew = TimeSpan.Zero,
			//JWT key is in safe storage (User Secrets)
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};
	});

builder.Services.AddAuthorization(options =>
{
	// This will made all endpoints of API app to be authenticated inclueded login and register too
	options.FallbackPolicy = new AuthorizationPolicyBuilder()
	.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
	.RequireAuthenticatedUser()
	.Build();
});

// Add Identity to builder services
builder.Services.AddIdentityCore<SchoolUser>()
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<StudentEnrollmentDbContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "bearer"
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{}
		}
	});

});

//builder.Services.AddScoped<IValidator<CreateEnrollmentDTO>, CreateEnrollmentDTOValidator>();

// add Scope for Interfaces and Repositories to services of API app
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IFileUploadRepository, FileUploadRepository>();


builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

// build CORS policy
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

//builder.Services.AddValidatorsFromAssemblyContaining<Program>();
///? can using this way to adding validators from other project app assembly
///? but for now we using current project app as a assembly
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// always to use UseAuthentication before UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
// using CORS policy
app.UseCors("AllowAll");

//using Carter as a helper to import all endpoints to app
app.MapCarter();

app.Run();

