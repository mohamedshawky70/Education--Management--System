namespace School.Application.DTOs.Student;

public record StudentRequest
(
	string Name,
	string Phone,
	string Address,
	int DepartmentId
);
