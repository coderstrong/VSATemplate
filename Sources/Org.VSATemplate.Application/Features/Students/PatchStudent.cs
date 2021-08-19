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
    using System.Collections.Generic;
    using System.Linq;

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
        private readonly IRepository<CoreDBContext, Class> _repositoryClass;
        private readonly IMapper _mapper;

        public PatchStudentHandler(IAuditRepository<CoreDBContext, Student> repository
            , IRepository<CoreDBContext, Class> repositoryClass
            , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _repositoryClass = repositoryClass;
        }

        public async Task<Response<bool>> Handle(PatchStudentCommand request, CancellationToken cancellationToken)
        {
            var studentToUpdate = await _repository.FirstOrDefaultAsync(e => e.Id == request.StudentId, e => e.Classes);
            if (studentToUpdate == null)
            {
                return new Response<bool>(HttpStatusCode.NotFound, new DataNotFoundError("Key"));
            }
            var studentToPatch = _mapper.Map<StudentForUpdateDto>(studentToUpdate);
            studentToPatch.Classes = new List<ClassForCreationDto>();
            request.PatchDoc.ApplyTo(studentToPatch);
            
            var validationResults = new StudentForUpdateDtoValidation().Validate(studentToPatch);
            if (!validationResults.IsValid)
            {
                throw new ValidationException(validationResults.Errors);
            }
            var _class = studentToPatch.Classes.Select(e => e).ToList();
            
            foreach (var existed in studentToUpdate.Classes)
            {
                await _repositoryClass.DeleteAsync(existed.Id);
            }
            
            _mapper.Map(studentToPatch, studentToUpdate);
            studentToUpdate.Classes = null;
            foreach (var item in _class)
            {
                var a = _mapper.Map<Class>(item);
                a.Id = System.Guid.NewGuid();
                a.Student = studentToUpdate;
                _repositoryClass.Insert(a);
            }

            _repository.Update(studentToUpdate);
            return new Response<bool>(await _repository.UnitOfWork.SaveEntitiesAsync());
        }
    }
}