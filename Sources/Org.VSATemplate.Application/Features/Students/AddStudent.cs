using AutoMapper;
using FluentValidation;
using MakeSimple.SharedKernel.Contract;
using MakeSimple.SharedKernel.Infrastructure.DTO;
using MakeSimple.SharedKernel.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Org.VSATemplate.Domain.Dtos.Student;
using Org.VSATemplate.Domain.Entities;
using Org.VSATemplate.Domain.Students.Validators;
using Org.VSATemplate.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Org.VSATemplate.Application.Features.Students
{
    public class AddStudentCommand : IRequest<IResponse<StudentDto>>
    {
        public StudentForCreationDto Data { get; }

        public AddStudentCommand(StudentForCreationDto data)
        {
            Data = data;
        }
    }

    public class CreateStudentValidation : AbstractValidator<AddStudentCommand>
    {
        public CreateStudentValidation()
        {
            RuleFor(command => command.Data).SetInheritanceValidator(v =>
            {
                v.Add<StudentForCreationDto>(new StudentForCreationDtoValidation());
            });
        }
    }

    public class AddStudentHandler : IRequestHandler<AddStudentCommand, IResponse<StudentDto>>
    {
        private readonly IAuditRepository<CoreDBContext, Student> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddStudentHandler(IAuditRepository<CoreDBContext, Student> repository
            , IMapper mapper
            , IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IResponse<StudentDto>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request.Data);
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