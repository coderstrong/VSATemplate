using MakeSimple.SharedKernel.Contract;
using MakeSimple.SharedKernel.Infrastructure.Api;
using MakeSimple.SharedKernel.Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Org.VSATemplate.Domain.Entities;
using Org.VSATemplate.Infrastructure.Database;
using System.Threading.Tasks;

namespace Org.VSATemplate.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IAuditRepositoryGeneric<CoreDBContext, Student> _student;

        public ClassesController(IAuditRepositoryGeneric<CoreDBContext, Student> student)
        {
            _student = student;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var a = await _student.ToListAsync();
            return Ok();
            //return ResultDTO(new Response<bool>(true) { StatusCode = System.Net.HttpStatusCode.OK });
        }
    }
}
