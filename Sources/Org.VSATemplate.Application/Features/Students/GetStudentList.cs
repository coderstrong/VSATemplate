using MakeSimple.SharedKernel.Contract;
using MakeSimple.SharedKernel.Wrappers;
using MediatR;
using Org.VSATemplate.Domain.Dtos.Student;
using Org.VSATemplate.Domain.Entities;
using Org.VSATemplate.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Org.VSATemplate.Application.Features.Students
{
    public class StudentListQuery : ClassParametersDto, IRequest<IPaginatedList<ClassDto>>
    {
    }

    public class StudentListQueryHandler : IRequestHandler<StudentListQuery, IPaginatedList<ClassDto>>
    {
        private readonly IAuditRepository<CoreDBContext, Student> _repository;

        public StudentListQueryHandler(IAuditRepository<CoreDBContext, Student> repository)
        {
            _repository = repository;
        }

        public async Task<IPaginatedList<ClassDto>> Handle(StudentListQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ToListAsync<ClassDto>(paging: request, expandSorts: request.SortOrder, expandFilters: request.Filters);
        }
    }
}