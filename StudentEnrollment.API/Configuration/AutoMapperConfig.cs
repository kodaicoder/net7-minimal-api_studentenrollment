using AutoMapper;
using StudentEnrollment.API.DTOs.Course;
using StudentEnrollment.API.DTOs.Enrollment;
using StudentEnrollment.API.DTOs.Student;
using StudentEnrollment.Data;

namespace StudentEnrollment.API.Configuration
{
	public class AutoMapperConfig : Profile
	{
		public AutoMapperConfig()
		{
			#region Mapper for Course
			CreateMap<Course, CourseDTO>().ReverseMap();
			CreateMap<Course, CreateCourseDTO>().ReverseMap();
			//? set up the mapping for the CourseDetailsDTO , while Students is a list of StudentDTOs that
			//? we will get from the Enrollments property of the Course entity.
			CreateMap<Course, CourseDetailsDTO>()
				.ForMember(courseDetails => courseDetails.Students,
				get => get.MapFrom(course => course.Enrollments.Select(Enrollment => Enrollment.Student)))
				.ReverseMap();
			#endregion

			#region Mapper for Student
			CreateMap<Student, StudentDTO>().ReverseMap();
			CreateMap<Student, CreateStudentDTO>().ReverseMap();
			//? set up the mapping for the StudentDetailsDTO , while Courses is a list of CourseDTOs that
			//? we will get from the Enrollments property of the Student entity.
			CreateMap<Student, StudentDetailsDTO>()
				.ForMember(studentDetails => studentDetails.Courses,
				get => get.MapFrom(student => student.Enrollments.Select(Enrollment => Enrollment.Course)))
				.ReverseMap();
			#endregion

			#region Mapper for Enrollment
			CreateMap<Enrollment, EnrollmentDTO>().ReverseMap();
			CreateMap<Enrollment, CreateEnrollmentDTO>().ReverseMap();
			#endregion
		}
	}
}
