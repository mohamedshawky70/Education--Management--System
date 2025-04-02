using OneOf;
using School.Application.DTOs.Teacher;
using School.Application.ErrorHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Interfaces.IServices
{
    public interface ITeacherService
    {
		Task<OneOf<TeacherResponse, Errors>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		Task<IEnumerable<TeacherResponse>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<OneOf<TeacherResponse, Errors>> CreateAsync(TeacherRequest request, CancellationToken cancellationToken = default);
		Task<OneOf<TeacherResponse, Errors>> UpdateAsync(int id, TeacherRequest request, CancellationToken cancellationToken = default);
		Task<OneOf<Errors?>> DeleteAsync(int id, CancellationToken cancellationToken = default);

	}
}
