using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OneOf;
using School.Application.DTOs.Student;
using School.Application.ErrorHandler;

namespace School.Application.Interfaces.IServices
{
    public interface IStudentService
    {
        Task<OneOf<StudentResponse, Errors>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<StudentResponse>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<OneOf<StudentResponse, Errors>> CreateAsync(StudentRequest request, CancellationToken cancellationToken = default);
		Task<OneOf<StudentResponse, Errors>> UpdateAsync(int id,StudentRequest request, CancellationToken cancellationToken = default);
		Task<OneOf<Errors?>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
