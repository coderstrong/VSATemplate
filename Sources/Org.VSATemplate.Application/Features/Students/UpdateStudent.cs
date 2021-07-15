using AutoMapper;
using MakeSimple.SharedKernel.Contract;
using MakeSimple.SharedKernel.Infrastructure.DTO;
using MakeSimple.SharedKernel.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Org.VSATemplate.Domain.Dtos.Student;
using Org.VSATemplate.Domain.Entities;
using Org.VSATemplate.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Org.VSATemplate.Application.Features.Students
{
    public class UpdateStudentCommand : StudentForUpdateDto, IRequest<IResponse<bool>>
    {
    }

    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, IResponse<bool>>
    {
        private readonly IAuditRepositoryGeneric<CoreDBContext, Student> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateStudentHandler(IAuditRepositoryGeneric<CoreDBContext, Student> repository
            , IMapper mapper
            , IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IResponse<bool>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);
            _repository.Insert(student);
            return new Response<bool>(await _repository.UnitOfWork.SaveEntitiesAsync());
        }
    }
}