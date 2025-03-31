using OneOf;
using OneOf.Types;
using School.Application.DTOs.Assignment;
using School.Application.ErrorHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Interfaces.IServices
{
    public interface IAssignmentService
    {
        Task<OneOf<AssignmentResponse,Errors>> GetByIdAsync(int id,CancellationToken cancellationToken=default);
        Task<IEnumerable<AssignmentResponse>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<OneOf<AssignmentResponse, Errors>> CreateAsync(AssignmentRequest request, CancellationToken cancellationToken = default);
        Task<OneOf<AssignmentResponse, Errors>> UpdateAsync(int id, AssignmentRequest request, CancellationToken cancellationToken = default);
        Task<OneOf<Errors?>> DeleteAsync(int id, CancellationToken cancellationToken = default);

	}
}
