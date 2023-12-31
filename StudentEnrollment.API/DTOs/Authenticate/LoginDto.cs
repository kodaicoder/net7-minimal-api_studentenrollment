﻿using FluentValidation;

namespace StudentEnrollment.API.Endpoints;
public class LoginDto
{
	public string Username { get; set; }
	public string Password { get; set; }
}

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
	public LoginDtoValidator()
	{
		RuleFor(x => x.Username)
			.NotEmpty();

		RuleFor(x => x.Password)
			.NotEmpty()
			.MinimumLength(6)
			.MaximumLength(20);
	}
}



