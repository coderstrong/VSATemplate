using MakeSimple.SharedKernel.Contract;
using MakeSimple.SharedKernel.Wrappers;
using MediatR;
using Org.VSATemplate.Domain.Students.Dtos;
using Org.VSATemplate.Domain.Entities;
using Org.VSATemplate.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Org.VSATemplate.Application.Features.Students
{
    public class StudentQuery : IRequest<IResponse<StudentDto>>
    {
        public long Id { get; set; }
    }

    public class StudentQueryHandler : IRequestHandler<StudentQuery, IResponse<StudentDto>>
    {
        private readonly IAuditRepositoryGeneric<CoreDBContext, Student> _repository;

        public StudentQueryHandler(IAuditRepositoryGeneric<CoreDBContext, Student> repository)
        {
            _repository = repository;
        }

        public async Task<IResponse<StudentDto>> Handle(StudentQuery request, CancellationToken cancellationToken)
        {
            return await _repository.FirstOrDefaultAsync<StudentDto>(request.Id);
        }
    }
}