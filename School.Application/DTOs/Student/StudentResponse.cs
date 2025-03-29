namespace School.Application.DTOs.Student;

public record StudentResponse
(
	string Name,
	string Phone,
	string Address,
	int DepartmentId,
	string DepartmentName
);
