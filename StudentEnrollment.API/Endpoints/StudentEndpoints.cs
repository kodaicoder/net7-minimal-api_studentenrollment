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
		//>GET-ALL
		group.MapGet("/", GetAllStudents)
			.AddEndpointFilter<LoggingFilter>()
			.WithName("GetAllStudents")
			.Produces<List<StudentDTO>>(StatusCodes.Status200OK);
		//>GET-ID
		group.MapGet("/{id}", GetStudentById)
			.AddEndpointFilter<LoggingFilter>()
			.WithName("GetStudentById")
			.Produces<StudentDTO>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound);
		//>POST
		group.MapPost("/", CreateStudent)
			.AddEndpointFilter<ValidationFilter<CreateStudentDTO>>()
			.AddEndpointFilter<LoggingFilter>()
			.WithName("CreateStudent")
			.Produces<Student>(StatusCodes.Status201Created)
			.Produces(StatusCodes.Status400BadRequest);
		//>PUT
		group.MapPut("/{id}", UpdateStudentById)
			.AddEndpointFilter<ValidationFilter<StudentDTO>>()
			.AddEndpointFilter<LoggingFilter>()
			.WithName("UpdateStudent")
			.Produces(StatusCodes.Status404NotFound)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status204NoContent);
		//>DELETE
		group.MapDelete("/{id}", DeleteStudentById)
			.AddEndpointFilter<LoggingFilter>()
			.WithName("DeleteStudent")
			.Produces<Student>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status404NotFound);
		//>EXTRA
		group.MapGet("/Student/GetCourses/{id}", GetCoursesByStudentId);

	}
	//>>GET-ALL
	private static async Task<IResult> GetAllStudents(IStudentRepository repo, IMapper mapper)
	{
		List<Student> models = await repo.GetAllAsync();
		return Results.Ok(mapper.Map<List<StudentDTO>>(models));
	}
	//>>GET-ID
	private static async Task<IResult> GetStudentById(IStudentRepository repo, IMapper mapper, int id)
	{
		Student record = await repo.GetByIdAsync(id);
		return record is Student
				? Results.Ok(mapper.Map<StudentDTO>(record))
				: Results.NotFound();
	}
	//>>POST
	[Authorize(Roles = "Administrator")]
	private static async Task<IResult> CreateStudent(CreateStudentDTO createStudentDto, IStudentRepository repo, IFileUploadRepository fileUploadRepository, IMapper mapper)
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
	//>>PUT
	[Authorize(Roles = "Administrator")]
	private static async Task<IResult> UpdateStudentById(StudentDTO studentDto, IStudentRepository repo, IFileUploadRepository fileUploadRepository, IMapper mapper)
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
	//>>DELETE
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
	//>>EXTRA
	private static async Task<IResult> GetCoursesByStudentId(IStudentRepository repo, IMapper mapper, int id)
	{
		Student record = await repo.GetAllCoursesByStudentId(id);
		return
			record is Student
			? Results.Ok(mapper.Map<StudentDetailsDTO>(record))
			: Results.NotFound();
	}

}
