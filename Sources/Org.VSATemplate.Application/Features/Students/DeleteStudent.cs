using AutoMapper;
using MakeSimple.SharedKernel.Contract;
using MakeSimple.SharedKernel.Infrastructure.DTO;
using MakeSimple.SharedKernel.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Org.VSATemplate.Domain.Entities;
using Org.VSATemplate.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Org.VSATemplate.Application.Features.Students
{
    public class DeleteStudentCommand: IRequest<IResponse<bool>>
    {
        public long Id { get; set; }
    }

    public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand, IResponse<bool>>
    {
        private readonly IAuditRepository<CoreDBContext, Student> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteStudentHandler(IAuditRepository<CoreDBContext, Student> repository
            , IMapper mapper
            , IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IResponse<bool>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);
            await _repository.DeleteAsync(student);
            return new Response<bool>(await _repository.UnitOfWork.SaveEntitiesAsync());
        }
    }
}