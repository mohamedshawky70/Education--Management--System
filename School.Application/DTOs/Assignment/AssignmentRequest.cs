namespace School.Application.DTOs.Assignment
{
	public record AssignmentRequest
	(
		 string Title,
		 DateTime DueDate,
		 byte MaxScore,
		 int SubjectId,
		 int StudentId
	);
}
