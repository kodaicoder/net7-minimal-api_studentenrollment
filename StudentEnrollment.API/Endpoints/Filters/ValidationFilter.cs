using FluentValidation;
using FluentValidation.Results;

namespace StudentEnrollment.API.Endpoints.Filters
{
	public class ValidationFilter<TDto> : IEndpointFilter where TDto : class
	{
		private readonly IValidator<TDto> _validator;

		public ValidationFilter(IValidator<TDto> validator)
		{
			this._validator = validator;
		}

		public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
		{

			///>> RUN BEFORE ENDPOINT CODE CALL (LoginUser
			TDto dtoObject = context.GetArgument<TDto>(0); /*THIS SHOULD BE THE DTO OBJECT AND NEED TO BE A FIRST AGRUMENT*/
			ValidationResult? validationResult = await _validator.ValidateAsync(dtoObject);

			if (!validationResult.IsValid)
			{
				return Results.BadRequest(validationResult.ToDictionary());
			}

			return await next(context);

			//? or you can do this
			/// var result = await next(context);
			//////>> RUN AFTER ENDPOINT CODE CALL (LoginUser)
			/// return reseult
		}
	}
}
