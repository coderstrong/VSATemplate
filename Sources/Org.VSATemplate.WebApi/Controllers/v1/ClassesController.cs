using MakeSimple.SharedKernel.Infrastructure.Api;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.VSATemplate.Application.Features.Students;
using Org.VSATemplate.Domain.Dtos.Student;
using System.Threading.Tasks;

namespace Org.VSATemplate.WebApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    public class ClassesController : ControllerApiBase
    {
        private readonly IMediator _mediator;

        public ClassesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok();
            //return ResultDTO(new Response<bool>(true) { StatusCode = System.Net.HttpStatusCode.OK });
        }

        [ProducesResponseType(typeof(StudentForCreationDto), 200)]
        [ProducesResponseType(typeof(bool), 400)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(AddStudentCommand student)
        {
            var result = await _mediator.Send(student);
            return Ok(result);
            //return ResultDTO(new Response<bool>(true) { StatusCode = System.Net.HttpStatusCode.OK });
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> Put()
        {
            return Ok();
            //return ResultDTO(new Response<bool>(true) { StatusCode = System.Net.HttpStatusCode.OK });
        }

        [HttpDelete]
        [AllowAnonymous]
        public async Task<IActionResult> Delete()
        {
            return Ok();
            //return ResultDTO(new Response<bool>(true) { StatusCode = System.Net.HttpStatusCode.OK });
        }
    }
}