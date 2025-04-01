namespace School.Application.DTOs.Subject
{
	public record SubjectRequest
	(
		string Name,
		DateTime Period,
		int TeacherId
	);
}
