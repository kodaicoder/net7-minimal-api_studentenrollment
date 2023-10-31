using AutoMapper;
using Carter;
using Microsoft.AspNetCore.Authorization;
using StudentEnrollment.API.DTOs.Course;
using StudentEnrollment.API.DTOs.Student;
using StudentEnrollment.API.Endpoints.Filters;
using StudentEnrollment.Data;
using StudentEnrollment.Data.Interfaces;

namespace StudentEnrollment.API.Endpoints;

public class CourseEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder routes)
	{
		//You can add .RequireAuthorization() to made this group of endpoints available only for authorized users
		//or you can use [Authorize] attribute to made each individual endpoint available only for authorized users
		//but now we set a Authorization Policy in Program.cs
		var group = routes.MapGroup("/api/Courses").WithTags(nameof(Course)).WithOpenApi();

		//!>>GET
		group.MapGet("/", GetAllCourses)
			.AddEndpointFilter<LoggingFilter>()
			.WithName("GetAllCourses")
			.WithSummary("Get all Courses")
			.WithDescription("")
			.Produces<List<CourseDTO>>(StatusCodes.Status200OK);

		//!>>GET-Id
		group.MapGet("/{id}", GetCourseById)
			.AddEndpointFilter<LoggingFilter>()
			.WithName("GetCourseById")
			.WithSummary("Get Course by Course Id")
			.WithDescription("{CourseId}")
			.Produces<CourseDTO>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound);

		//!>>POST
		group.MapPost("/", CreateCourse)
			.AddEndpointFilter<ValidationFilter<CreateCourseDTO>>()
			.AddEndpointFilter<LoggingFilter>()
			.WithName("CreatedCourse")
			.WithSummary("Created new Course")
			.WithDescription("")
			.Produces<Course>(StatusCodes.Status201Created)
			.Produces(StatusCodes.Status400BadRequest);

		//!>>PUT
		group.MapPut("/", UpdateCourseById).WithName("UpdateCourseById")
			.AddEndpointFilter<ValidationFilter<CourseDTO>>()
			.AddEndpointFilter<LoggingFilter>()
			.WithName("UpdateCourse")
			.WithSummary("Update Course by Course Id")
			.WithDescription("")
			.Produces(StatusCodes.Status204NoContent)
			.Produces(StatusCodes.Status404NotFound)
			.Produces(StatusCodes.Status400BadRequest);

		//!>>DELETE
		group.MapDelete("/{id}", DeleteCourseById).WithName("DeleteCourseById")
			.AddEndpointFilter<LoggingFilter>()
			.WithName("DeleteCourseById")
			.WithSummary("Delete Course by Course Id")
			.WithDescription("{Course_Id}")
			.Produces(StatusCodes.Status204NoContent)
			.Produces(StatusCodes.Status404NotFound)
			.Produces(StatusCodes.Status400BadRequest);

		//!Get Students of Course
		group.MapGet("/Course/GetStudents/{id}", GetStudentsByCourseId)
			.WithName("GetStudentsByCourseId")
			.WithSummary("Get Students by Course Id")
			.WithDescription("{Course_Id}")
			.Produces<List<StudentDTO>>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound);
	}
	//!>>GET
	private static async Task<IResult> GetAllCourses(IMapper mapper, ICourseRepository repo)
	{
		List<Course> courses = await repo.GetAllAsync();
		//Mapping from courses from LinQ back to CourseDTO for present it to requestor
		return Results.Ok(mapper.Map<List<CourseDTO>>(courses));
	}
	//!>>GET-Id
	private static async Task<IResult> GetCourseById(IMapper mapper, ICourseRepository repo, int id)
	{
		Course record = await repo.GetByIdAsync(id);
		return record is Course
			? Results.Ok(mapper.Map<CourseDTO>(record))
			: Results.NotFound();
	}
	//!>>POST
	[Authorize(Roles = "Administrator")]
	private static async Task<IResult> CreateCourse(IMapper mapper, ICourseRepository repo, CreateCourseDTO createCourseDto)
	{
		try
		{
			Course course = mapper.Map<Course>(createCourseDto);
			await repo.CreateAsync(course);
			return Results.Created($"/api/courses/{course.Id}", course);
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}
	//!>>PUT
	[Authorize(Roles = "Administrator")]
	private static async Task<IResult> UpdateCourseById(IMapper mapper, ICourseRepository repo, CourseDTO courseDto)
	{
		Course record = await repo.GetByIdAsync(courseDto.Id);
		if (record == null) return Results.NotFound();

		try
		{
			//update model properties
			mapper.Map(courseDto, record);
			// send updated model to repo
			await repo.UpdateAsync(record);
			return Results.NoContent();
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}
	//!>>DELETE
	[Authorize(Roles = "Administrator")]
	private static async Task<IResult> DeleteCourseById(ICourseRepository repo, int id)
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

	private static async Task<IResult> GetStudentsByCourseId(IMapper mapper, ICourseRepository repo, int id)
	{
		Course record = await repo.GetAllStudentsByCourseId(id);
		return
			record is Course
			? Results.Ok(mapper.Map<CourseDetailsDTO>(record))
			: Results.NotFound();
	}

}
