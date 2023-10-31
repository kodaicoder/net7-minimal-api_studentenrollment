using Microsoft.AspNetCore.Identity;
using StudentEnrollment.API.DTOs.Authenticate;
using StudentEnrollment.API.Endpoints;

namespace StudentEnrollment.API.Services.Interfaces
{
	public interface IAuthenticationRepository
	{
		Task<AuthResponseDto> Login(LoginDto loginDto);
		Task<IEnumerable<IdentityError>> Register(RegisterDto registerDto);
	}
}
