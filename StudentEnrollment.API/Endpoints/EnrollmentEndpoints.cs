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

		//>GET-ALL
		group.MapGet("/", GetAllEnrollments)
			.AddEndpointFilter<LoggingFilter>()
			.WithName("GetAllEnrollments")
			.Produces<List<EnrollmentDTO>>(StatusCodes.Status200OK);
		//>GET-ID
		group.MapGet("/{id}", GetEnrollmentById)
			.AddEndpointFilter<LoggingFilter>()
			.WithName("GetEnrollmentById")
			.Produces<Enrollment>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound);
		//>POST
		group.MapPost("/", CreateEnrollment)
			.AddEndpointFilter<ValidationFilter<CreateEnrollmentDTO>>()
			.AddEndpointFilter<LoggingFilter>()
			.WithName("CreateEnrollment")
			.Produces(StatusCodes.Status400BadRequest)
			.Produces<Enrollment>(StatusCodes.Status201Created);
		//>PUT
		group.MapPut("/{id}", UpdateEnrollmentById)
			.AddEndpointFilter<ValidationFilter<EnrollmentDTO>>()
			.AddEndpointFilter<LoggingFilter>()
			.WithName("UpdateEnrollment")
			.Produces(StatusCodes.Status404NotFound)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status204NoContent);
		//>DELETE
		group.MapDelete("/{id}", DeleteEnrollmentById)
			.AddEndpointFilter<LoggingFilter>()
			.WithName("DeleteEnrollment")
			.Produces<Enrollment>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status404NotFound);
	}
	//>>GET-ALL
	[Authorize(Roles = "Administrator")]
	private static async Task<IResult> GetAllEnrollments(IEnrollmentRepository repo, IMapper mapper)
	{
		List<Enrollment> models = await repo.GetAllAsync();
		return Results.Ok(mapper.Map<List<EnrollmentDTO>>(models));
	}
	//>>GET-ID
	[Authorize(Roles = "Administrator")]
	private static async Task<IResult> GetEnrollmentById(IEnrollmentRepository repo, IMapper mapper, int id)
	{

		Enrollment record = await repo.GetByIdAsync(id);

		return record is Enrollment
				? Results.Ok(mapper.Map<EnrollmentDTO>(record))
				: Results.NotFound();
	}
	//>>POST
	[Authorize(Roles = "Administrator")]
	private static async Task<IResult> CreateEnrollment(CreateEnrollmentDTO createEnrollmentDto, IEnrollmentRepository repo, IMapper mapper)
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
	//>>PUT
	[Authorize(Roles = "Administrator")]
	private static async Task<IResult> UpdateEnrollmentById(EnrollmentDTO enrollmentDto, IEnrollmentRepository repo, IMapper mapper)
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
	//>>DELETE
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
