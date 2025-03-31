using OneOf.Types;
using School.Application.DTOs.Assignment;
using School.Application.ErrorHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Infrastructure.Implementation.Services
{
	public class AssignmentService(IUnitOfWork unitOfWork) : IAssignmentService
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		public async Task<OneOf<AssignmentResponse, Errors>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			var assignment = await _unitOfWork.Assignment.FindByInclude(x => x.Id == id, cancellationToken, ["Student", "Subject"]);
			if (assignment is null)
				return AssignmentErrors.NotFound;

			var response = assignment.Adapt<AssignmentResponse>();
			return response;
		}
		public async Task<IEnumerable<AssignmentResponse>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var assignment = await _unitOfWork.Assignment.FindAllInclude(cancellationToken, ["Student", "Subject"]);

			var response = assignment.Adapt<IEnumerable<AssignmentResponse>>();
			return response;
		}
		public async Task<OneOf<AssignmentResponse, Errors>> CreateAsync(AssignmentRequest request, CancellationToken cancellationToken = default)
		{
			var student = await _unitOfWork.Student.GetById(request.StudentId, cancellationToken);
			if (student is null)
				return StudentErrors.NotFound;
			var subject = await _unitOfWork.Subject.GetById(request.SubjectId, cancellationToken);
			if (subject is null)
				return SubjectErrors.NotFound;
			var existedAssignment = await _unitOfWork.Assignment.FindAllByInclude(x => x.Title == request.Title && x.MaxScore == request.MaxScore
			&& x.DueDate == request.DueDate && x.StudentId == request.StudentId && x.SubjectId == request.SubjectId);
			if (existedAssignment is not null)
				return AssignmentErrors.Duplicate;
			var assignment = request.Adapt<Assignment>();
			await _unitOfWork.Assignment.CreateAsync(assignment, cancellationToken);
			var response = assignment.Adapt<AssignmentResponse>();
			return response;
		}
		public async Task<OneOf<AssignmentResponse, Errors>> UpdateAsync(int id, AssignmentRequest request, CancellationToken cancellationToken = default)
		{
			var assignment = await _unitOfWork.Assignment.GetByIdAsync(id, cancellationToken);
			if (assignment is null)
				return AssignmentErrors.NotFound;
			var student = await _unitOfWork.Student.GetByIdAsync(request.StudentId, cancellationToken);
			if (student is null)
				return StudentErrors.NotFound;
			var subject = await _unitOfWork.Subject.GetByIdAsync(request.SubjectId, cancellationToken);
			if (subject is null)
				return SubjectErrors.NotFound;

			var newAssignment = request.Adapt(assignment);//Map data in data
			await _unitOfWork.Assignment.UpdateAsync(newAssignment, cancellationToken);
			var response = assignment.Adapt<AssignmentResponse>();
			return response;
		}
		public async Task<OneOf<Errors?>> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			var assignment = await _unitOfWork.Assignment.GetByIdAsync(id, cancellationToken);
			if (assignment is null)
				return AssignmentErrors.NotFound;

			await _unitOfWork.Assignment.DeleteAsync(assignment, cancellationToken);
			return null;
		}
	}
}
