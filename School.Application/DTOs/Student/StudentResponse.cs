namespace School.Application.DTOs.Student;

public record StudentResponseV1
(
	int Id,
	string Name,
	string Phone,
	string Address
);
public record StudentResponseV2
(
	int Id,
	string Name,
	string Phone,
	string Address,
	int DepartmentId,
	string DepartmentName
);
