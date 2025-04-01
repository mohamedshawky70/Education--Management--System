using Mapster;
using School.Application.DTOs.Assignment;
using School.Application.DTOs.Student;
using School.Application.DTOs.StudentSubject;
using School.Application.DTOs.Subject;


namespace School.Application.Mapping
{
	public class MappingConfig : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<Student, StudentResponse>()
				.Map(des => des.DepartmentName, src => src.Department.Name);
			config.NewConfig<Assignment, AssignmentResponse>()
				.Map(des => des.StudentName, src => src.Student!.Name)
				.Map(des => des.SubjectName, src => src.Subject!.Name);

			config.NewConfig<Subject, SubjectResponse>()
				.Map(des => des.TeacherName, src => src.Teacher!.Name);

			config.NewConfig<StudentSubject, StudentSubjectResponse>()
				.Map(des => des.studentName, src => src.Student!.Name)
				.Map(des => des.subjectName, src => src.Subject!.Name);
		}
	}
}
