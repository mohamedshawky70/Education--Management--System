namespace School.Application.DTOs.Student;

public record StudentResponse
(
	int Id,
	string Name,
	string Phone,
	string Address,
	int DepartmentId,
	string DepartmentName
);
