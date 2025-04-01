namespace School.Application.DTOs.Assignment
{
	public record AssignmentResponse
	(
		 int Id,
		 string Title,
		 DateTime DueDate,
		 byte MaxScore,
		 int SubjectId,
		 int StudentId,
		 string StudentName,
		 string SubjectName
	);
}
