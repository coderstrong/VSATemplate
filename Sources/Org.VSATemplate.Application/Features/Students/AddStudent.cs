using AutoMapper;
using MakeSimple.SharedKernel.Contract;
using MakeSimple.SharedKernel.Infrastructure.DTO;
using MakeSimple.SharedKernel.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Org.VSATemplate.Domain.Entities;
using Org.VSATemplate.Domain.Students.Dtos;
using Org.VSATemplate.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Org.VSATemplate.Application.Features.Students
{
    public class AddStudentCommand : StudentForCreationDto, IRequest<IResponse<StudentDto>>
    {
    }

    public class AddStudentHandler : IRequestHandler<AddStudentCommand, IResponse<StudentDto>>
    {
        private readonly IAuditRepositoryGeneric<CoreDBContext, Student> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddStudentHandler(IAuditRepositoryGeneric<CoreDBContext, Student> repository
            , IMapper mapper
            , IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IResponse<StudentDto>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);
            _repository.Insert(student);
            if (await _repository.UnitOfWork.SaveEntitiesAsync())
            {
                return new Response<StudentDto>(_mapper.Map<StudentDto>(student));
            }
            else
            {
                return null;
            }
        }
    }
}