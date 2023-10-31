using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudentEnrollment.API.DTOs.Authenticate;
using StudentEnrollment.API.Endpoints;
using StudentEnrollment.API.Services.Interfaces;
using StudentEnrollment.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentEnrollment.API.Services.Repositories
{
	public class AuthenticationRepository : IAuthenticationRepository
	{
		private readonly UserManager<SchoolUser> _userManager;
		private readonly IConfiguration _configuration;
		private SchoolUser _user;

		public AuthenticationRepository(UserManager<SchoolUser> userManager, IConfiguration configuration)
		{
			this._userManager = userManager;
			this._configuration = configuration;
		}

		public async Task<AuthResponseDto> Login(LoginDto loginDto)
		{
			_user = await _userManager.FindByNameAsync(loginDto.Username);

			if (_user is null)
			{
				return default;
			}

			bool isPasswordValid = await _userManager.CheckPasswordAsync(_user, loginDto.Password);

			if (!isPasswordValid)
			{
				return default;
			}

			var token = await GenerateJwtToken();

			return new AuthResponseDto
			{
				UserID = _user.Id,
				Token = token
			};
		}

		public async Task<IEnumerable<IdentityError>> Register(RegisterDto registerDto)
		{
			_user = new SchoolUser
			{
				UserName = registerDto.Username,
				Email = registerDto.Email,
				FirstName = registerDto.FirstName,
				LastName = registerDto.LastName,
				DateOfBirth = registerDto.DateOfBirth
			};


			IdentityResult res = await _userManager.CreateAsync(_user, registerDto.Password);
			if (res.Succeeded)
			{
				await _userManager.AddToRoleAsync(_user, "User");
			}

			//This will be empty list of errors if processs is successful
			return res.Errors;
		}

		private async Task<string> GenerateJwtToken()
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


			var roles = await _userManager.GetRolesAsync(_user);
			var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
			var userClaims = await _userManager.GetClaimsAsync(_user);

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub,_user.Id),
				new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email,_user.Email),
				new Claim("userId",_user.Id),
			}.Union(userClaims).Union(roleClaims);

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddHours(Convert.ToInt32(_configuration["Jwt:DurationInHours"])),
				signingCredentials: credentials
			);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
