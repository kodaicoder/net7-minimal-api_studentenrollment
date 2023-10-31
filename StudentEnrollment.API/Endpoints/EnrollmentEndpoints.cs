using AutoMapper;
using Carter;
using Microsoft.AspNetCore.Authorization;
using StudentEnrollment.API.DTOs.Enrollment;
using StudentEnrollment.API.Endpoints.Filters;
using StudentEnrollment.Data;
using StudentEnrollment.Data.Interfaces;

namespace StudentEnrollment.API.Endpoints;

public class EnrollmentEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/Enrollment").WithTags(nameof(Enrollment)).WithOpenApi();

		group.MapGet("/", GetAllEnrollments)
		.AddEndpointFilter<LoggingFilter>()
		.WithName("GetAllEnrollments")
		.Produces<List<EnrollmentDTO>>(StatusCodes.Status200OK);

		group.MapGet("/{id}", GetEnrollmentById)
		.AddEndpointFilter<LoggingFilter>()
		.WithName("GetEnrollmentById")
		.Produces<Enrollment>(StatusCodes.Status200OK)
		.Produces(StatusCodes.Status404NotFound);

		group.MapPut("/{id}", UpdateEnrollmentById)
		.AddEndpointFilter<ValidationFilter<EnrollmentDTO>>()
		.AddEndpointFilter<LoggingFilter>()
		.WithName("UpdateEnrollment")
		.Produces(StatusCodes.Status404NotFound)
		.Produces(StatusCodes.Status400BadRequest)
		.Produces(StatusCodes.Status204NoContent);

		group.MapPost("/", CreateEnrollment)
		.AddEndpointFilter<ValidationFilter<CreateEnrollmentDTO>>()
		.AddEndpointFilter<LoggingFilter>()
		.WithName("CreateEnrollment")
		.Produces(StatusCodes.Status400BadRequest)
		.Produces<Enrollment>(StatusCodes.Status201Created);

		group.MapDelete("/{id}", DeleteEnrollmentById)
		.AddEndpointFilter<LoggingFilter>()
		.WithName("DeleteEnrollment")
		.Produces<Enrollment>(StatusCodes.Status200OK)
		.Produces(StatusCodes.Status400BadRequest)
		.Produces(StatusCodes.Status404NotFound);
	}
	[Authorize(Roles = "Administrator")]
	private static async Task<IResult> GetAllEnrollments(IMapper mapper, IEnrollmentRepository repo)
	{
		List<Enrollment> models = await repo.GetAllAsync();
		return Results.Ok(mapper.Map<List<EnrollmentDTO>>(models));
	}
	[Authorize(Roles = "Administrator")]
	private static async Task<IResult> GetEnrollmentById(IMapper mapper, IEnrollmentRepository repo, int id)
	{

		Enrollment record = await repo.GetByIdAsync(id);

		return record is Enrollment
				? Results.Ok(mapper.Map<EnrollmentDTO>(record))
				: Results.NotFound();
	}
	[Authorize(Roles = "Administrator")]
	private static async Task<IResult> UpdateEnrollmentById(IMapper mapper, IEnrollmentRepository repo, EnrollmentDTO enrollmentDto)
	{
		Enrollment record = await repo.GetByIdAsync(enrollmentDto.Id);
		if (record == null) return Results.NotFound();

		try
		{
			//update model properties
			mapper.Map(enrollmentDto, record);
			await repo.UpdateAsync(record);
			return Results.NoContent();
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}

	private static async Task<IResult> CreateEnrollment(IMapper mapper, IEnrollmentRepository repo, CreateEnrollmentDTO createEnrollmentDto)
	{
		try
		{
			Enrollment enrollment = mapper.Map<Enrollment>(createEnrollmentDto);
			await repo.CreateAsync(enrollment);
			return Results.Created($"/api/Enrollment/{enrollment.Id}", enrollment);
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}

	private static async Task<IResult> DeleteEnrollmentById(IEnrollmentRepository repo, int id)
	{
		try
		{
			return await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}

}
