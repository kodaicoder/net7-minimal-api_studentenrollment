using AutoMapper;
using Carter;
using Microsoft.AspNetCore.Authorization;
using StudentEnrollment.API.DTOs.Student;
using StudentEnrollment.API.Endpoints.Filters;
using StudentEnrollment.API.Services.Interfaces;
using StudentEnrollment.Data;
using StudentEnrollment.Data.Interfaces;

namespace StudentEnrollment.API.Endpoints;

public class StudentEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/Student").WithTags(nameof(Student)).WithOpenApi();

		group.MapGet("/", GetAllStudents)
			.AddEndpointFilter<LoggingFilter>()
			.WithName("GetAllStudents")
			.Produces<List<StudentDTO>>(StatusCodes.Status200OK);

		group.MapGet("/{id}", GetStudentById)
			.AddEndpointFilter<LoggingFilter>()
			.WithName("GetStudentById")
			.Produces<StudentDTO>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound);

		group.MapPut("/{id}", UpdateStudentById)
			.AddEndpointFilter<ValidationFilter<StudentDTO>>()
			.AddEndpointFilter<LoggingFilter>()
			.WithName("UpdateStudent")
			.Produces(StatusCodes.Status404NotFound)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status204NoContent);

		group.MapPost("/", CreateStudent)
			.AddEndpointFilter<ValidationFilter<CreateStudentDTO>>()
			.AddEndpointFilter<LoggingFilter>()
			.WithName("CreateStudent")
			.Produces<Student>(StatusCodes.Status201Created)
			.Produces(StatusCodes.Status400BadRequest);

		group.MapDelete("/{id}", DeleteStudentById)
			.AddEndpointFilter<LoggingFilter>()
			.WithName("DeleteStudent")
			.Produces<Student>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status404NotFound);

		group.MapGet("/Student/GetCourses/{id}", GetCoursesByStudentId);

	}

	private static async Task<IResult> GetAllStudents(IMapper mapper, IStudentRepository repo)
	{
		List<Student> models = await repo.GetAllAsync();
		return Results.Ok(mapper.Map<List<StudentDTO>>(models));
	}

	private static async Task<IResult> GetStudentById(IMapper mapper, IStudentRepository repo, int id)
	{
		Student record = await repo.GetByIdAsync(id);
		return record is Student
				? Results.Ok(mapper.Map<StudentDTO>(record))
				: Results.NotFound();
	}
	[Authorize(Roles = "Administrator")]
	private static async Task<IResult> UpdateStudentById(IMapper mapper, IStudentRepository repo, StudentDTO studentDto, IFileUploadRepository fileUploadRepository)
	{
		Student record = await repo.GetByIdAsync(studentDto.Id);
		if (record == null) return Results.NotFound();

		try
		{
			//update model properties
			mapper.Map(studentDto, record);

			if (studentDto.ProfilePicture != null)
			{
				record.Picture = fileUploadRepository.UploadStudentFile(studentDto.ProfilePicture, studentDto.OriginalPictureName);
			}

			await repo.UpdateAsync(record);
			return Results.NoContent();
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}
	[Authorize(Roles = "Administrator")]
	private static async Task<IResult> CreateStudent(IMapper mapper, IStudentRepository repo, CreateStudentDTO createStudentDto, IFileUploadRepository fileUploadRepository)
	{
		try
		{
			Student student = mapper.Map<Student>(createStudentDto);
			student.Picture = fileUploadRepository.UploadStudentFile(createStudentDto.ProfilePicture, createStudentDto.OriginalPictureName);
			await repo.CreateAsync(student);
			return Results.Created($"/api/Student/{student.Id}", student);
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}
	[Authorize(Roles = "Administrator")]
	private static async Task<IResult> DeleteStudentById(IStudentRepository repo, int id)
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

	private static async Task<IResult> GetCoursesByStudentId(IMapper mapper, IStudentRepository repo, int id)
	{
		Student record = await repo.GetAllCoursesByStudentId(id);
		return
			record is Student
			? Results.Ok(mapper.Map<StudentDetailsDTO>(record))
			: Results.NotFound();
	}

}
