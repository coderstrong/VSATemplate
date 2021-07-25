using MakeSimple.SharedKernel.Contract;
using MakeSimple.SharedKernel.Infrastructure.DTO;
using MakeSimple.SharedKernel.Wrappers;
using MediatR;
using Org.VSATemplate.Domain.Entities;
using Org.VSATemplate.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Org.VSATemplate.Application.Features.Students
{
    public class DeleteStudentCommand : IRequest<IResponse<bool>>
    {
        public long StudentId { get; set; }

        public DeleteStudentCommand(long studentId)
        {
            StudentId = studentId;
        }
    }

    public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand, IResponse<bool>>
    {
        private readonly IAuditRepository<CoreDBContext, Student> _repository;

        public DeleteStudentHandler(IAuditRepository<CoreDBContext, Student> repository)
        {
            _repository = repository;
        }

        public async Task<IResponse<bool>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.StudentId);
            return new Response<bool>(await _repository.UnitOfWork.SaveEntitiesAsync());
        }
    }
}