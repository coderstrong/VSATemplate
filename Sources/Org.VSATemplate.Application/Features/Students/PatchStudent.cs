using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Org.VSATemplate.Application.Features.Students
{
    using MakeSimple.SharedKernel.Contract;
    using MakeSimple.SharedKernel.Infrastructure.DTO;
    using Org.VSATemplate.Domain.Dtos.Student;
    using Org.VSATemplate.Domain.Entities;
    using Org.VSATemplate.Domain.Students.Validators;
    using Org.VSATemplate.Infrastructure.Database;

    public class PatchStudentCommand : IRequest<Response<bool>>
    {
        public long StudentId { get; set; }
        public JsonPatchDocument<StudentForUpdateDto> PatchDoc { get; set; }

        public PatchStudentCommand(long studentId, JsonPatchDocument<StudentForUpdateDto> patchDoc)
        {
            StudentId = studentId;
            PatchDoc = patchDoc;
        }
    }

    public class PatchStudentHandler : IRequestHandler<PatchStudentCommand, Response<bool>>
    {
        private readonly IAuditRepository<CoreDBContext, Student> _repository;
        private readonly IMapper _mapper;

        public PatchStudentHandler(IAuditRepository<CoreDBContext, Student> repository
            , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(PatchStudentCommand request, CancellationToken cancellationToken)
        {
            var studentToUpdate = await _repository.FirstOrDefaultAsync(request.StudentId);
            if (studentToUpdate == null)
            {
                return new Response<bool>(HttpStatusCode.NotFound, new DataNotFoundError("Key"));
            }

            var studentToPatch = _mapper.Map<StudentForUpdateDto>(studentToUpdate);
            request.PatchDoc.ApplyTo(studentToPatch);

            var validationResults = new StudentForUpdateDtoValidation().Validate(studentToPatch);
            if (!validationResults.IsValid)
            {
                throw new ValidationException(validationResults.Errors);
            }

            _mapper.Map(studentToPatch, studentToUpdate);

            return new Response<bool>(await _repository.UnitOfWork.SaveEntitiesAsync());
        }
    }
}