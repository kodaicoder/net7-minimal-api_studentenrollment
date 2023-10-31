using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using StudentEnrollment.API.DTOs.Authenticate;
using StudentEnrollment.API.Endpoints.Filters;
using StudentEnrollment.API.Services.Interfaces;

namespace StudentEnrollment.API.Endpoints;

public class AuthenticationEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/auth").WithTags("Authentication").AllowAnonymous().WithOpenApi();

		group.MapPost("/login", LoginUser)
			.AddEndpointFilter<ValidationFilter<LoginDto>>()
			.AddEndpointFilter<LoggingFilter>()
			.WithName("Login")
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status401Unauthorized);

		group.MapPost("/register", RegisterUser)
			.AddEndpointFilter<ValidationFilter<RegisterDto>>()
			.AddEndpointFilter<LoggingFilter>()
			.WithName("Register")
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest);

	}
	private static async Task<IResult> LoginUser(LoginDto loginDto, IAuthenticationRepository authRepo)
	{
		AuthResponseDto res = await authRepo.Login(loginDto);
		if (res is null)
		{
			return Results.Unauthorized();
		}
		return Results.Ok(res);
	}
	private static async Task<IResult> RegisterUser(RegisterDto registerDto, IAuthenticationRepository authRepo)
	{
		IEnumerable<IdentityError> res = await authRepo.Register(registerDto);

		if (!res.Any())
		{
			return Results.Ok();
		}

		List<ErrorResponseDto> errors = new List<ErrorResponseDto>();
		foreach (var error in res)
		{
			errors.Add(new ErrorResponseDto
			{
				Code = error.Code,
				Description = error.Description
			});
		}

		return Results.BadRequest(errors);
	}
}


