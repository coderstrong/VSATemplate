using MakeSimple.SharedKernel.Infrastructure.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Org.VSATemplate.Application.Features.Students;
using Org.VSATemplate.Domain.Dtos.Student;
using System.Threading.Tasks;

namespace Org.VSATemplate.WebApi.Controllers.v1
{
    [ApiController]
    public class ClassesController : ControllerApiBase
    {
        private readonly IMediator _mediator;

        public ClassesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
            //return ResultDTO(new Response<bool>(true) { StatusCode = System.Net.HttpStatusCode.OK });
        }

        [ApiVersion("1.0")]
        [ProducesResponseType(typeof(StudentForCreationDto), 200)]
        [ProducesResponseType(typeof(bool), 400)]
        [HttpPost]
        public async Task<IActionResult> Post(StudentForCreationDto student)
        {
            var comand = new AddStudent.AddStudentCommand(student);
            var result = await _mediator.Send(comand);
            return Ok(result);
            //return ResultDTO(new Response<bool>(true) { StatusCode = System.Net.HttpStatusCode.OK });
        }

        [ApiVersion("2.0")]
        [HttpPut]
        public async Task<IActionResult> Put()
        {
            return Ok();
            //return ResultDTO(new Response<bool>(true) { StatusCode = System.Net.HttpStatusCode.OK });
        }

        [ApiVersion("2.0")]
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return Ok();
            //return ResultDTO(new Response<bool>(true) { StatusCode = System.Net.HttpStatusCode.OK });
        }
    }
}
