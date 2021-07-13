using AutoMapper;
using MakeSimple.SharedKernel.Contract;
using MediatR;
using Microsoft.AspNetCore.Http;
using Org.VSATemplate.Domain.Dtos.Student;
using Org.VSATemplate.Domain.Entities;
using Org.VSATemplate.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Org.VSATemplate.Application.Features.Students
{
    public class StudentQuery : StudentForCreationDto, IRequest<StudentDto>
    {
    }

    public class StudentQueryHandler : IRequestHandler<StudentQuery, StudentDto>
    {
        private readonly IAuditRepositoryGeneric<CoreDBContext, Student> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentQueryHandler(IAuditRepositoryGeneric<CoreDBContext, Student> repository
            , IMapper mapper
            , IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<StudentDto> Handle(StudentQuery request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);
            _repository.Insert(student);
            if (await _repository.UnitOfWork.SaveEntitiesAsync())
            {
                return _mapper.Map<StudentDto>(student);
            }
            else
            {
                return null;
            }
        }
    }
}