namespace School.Application.DTOs.Subject
{
	public record SubjectResponse
	(
		int Id,
		string Name,
		DateTime Period,
		int TeacherId,
		string TeacherName
	);
}
