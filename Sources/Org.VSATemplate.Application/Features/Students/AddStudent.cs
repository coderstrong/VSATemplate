using MediatR;
using Org.VSATemplate.Domain.Dtos.Student;
using System.Threading;
using System.Threading.Tasks;
using MakeSimple.SharedKernel.Contract;
using Org.VSATemplate.Infrastructure.Database;
using Org.VSATemplate.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Org.VSATemplate.Application.Features.Students
{
    public static class AddStudent
    {
        public class AddStudentCommand : IRequest<StudentDto>
        {
            public StudentForCreationDto StudentToAdd { get; set; }

            public AddStudentCommand(StudentForCreationDto studentToAdd)
            {
                StudentToAdd = studentToAdd;
            }
        }

        public class Handler : IRequestHandler<AddStudentCommand, StudentDto>
        {
            private readonly IAuditRepositoryGeneric<CoreDBContext, Student> _repository;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public Handler(IAuditRepositoryGeneric<CoreDBContext, Student> repository
                , IMapper mapper
                , IHttpContextAccessor httpContextAccessor)
            {
                _repository = repository;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<StudentDto> Handle(AddStudentCommand request, CancellationToken cancellationToken)
            {
                var student = _mapper.Map<Student>(request.StudentToAdd);
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
}
