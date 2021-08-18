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
    public class AddStudentCommand : IRequest<IResponse<ClassDto>>
    {
        public ClassForCreationDto Data { get; }

        public AddStudentCommand(ClassForCreationDto data)
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
                v.Add<ClassForCreationDto>(new StudentForCreationDtoValidation());
            });
        }
    }

    public class AddStudentHandler : IRequestHandler<AddStudentCommand, IResponse<ClassDto>>
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

        public async Task<IResponse<ClassDto>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request.Data);
            _repository.Insert(student);
            if (await _repository.UnitOfWork.SaveEntitiesAsync())
            {
                return new Response<ClassDto>(_mapper.Map<ClassDto>(student));
            }
            else
            {
                return null;
            }
        }
    }
}