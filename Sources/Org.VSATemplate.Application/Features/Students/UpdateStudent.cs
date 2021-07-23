﻿using AutoMapper;
using MakeSimple.SharedKernel.Contract;
using MakeSimple.SharedKernel.Infrastructure.DTO;
using MakeSimple.SharedKernel.Wrappers;
using MediatR;
using Org.VSATemplate.Domain.Dtos.Student;
using Org.VSATemplate.Domain.Entities;
using Org.VSATemplate.Infrastructure.Database;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Org.VSATemplate.Application.Features.Students
{
    public class UpdateStudentCommand : IRequest<IResponse<bool>>
    {
        public long Id { get; }
        public StudentForUpdateDto Data { get; }

        public UpdateStudentCommand(long id, StudentForUpdateDto data)
        {
            Id = id;
            Data = data;
        }
    }

    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, IResponse<bool>>
    {
        private readonly IAuditRepository<CoreDBContext, Student> _repository;
        private readonly IMapper _mapper;

        public UpdateStudentHandler(IAuditRepository<CoreDBContext, Student> repository
            , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IResponse<bool>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _repository.FirstOrDefaultAsync(request.Id);
            if (student == null)
            {
                return new Response<bool>(HttpStatusCode.NotFound, new DataNotFoundError("Key"));
            }

            _mapper.Map(request.Data, student);
            return new Response<bool>(await _repository.UnitOfWork.SaveEntitiesAsync());
        }
    }
}