using AutoMapper;
using MakeSimple.SharedKernel.Contract;
using MakeSimple.SharedKernel.Infrastructure.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Org.VSATemplate.Domain.Entities;
using Org.VSATemplate.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Org.VSATemplate.Application.Features.Students
{
    public class PatchStudentCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }
    }

    public class PatchStudentHandler : IRequestHandler<PatchStudentCommand, Response<bool>>
    {
        private readonly IAuditRepositoryGeneric<CoreDBContext, Student> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PatchStudentHandler(IAuditRepositoryGeneric<CoreDBContext, Student> repository
            , IMapper mapper
            , IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<bool>> Handle(PatchStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);
            _repository.Insert(student);
            if (await _repository.UnitOfWork.SaveEntitiesAsync())
            {
                return new Response<bool>(true);
            }
            else
            {
                return null;
            }
        }
    }
}