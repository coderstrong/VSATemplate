using MakeSimple.SharedKernel.Infrastructure.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Org.VSATemplate.WebApi.Apis.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        /// <summary>
        /// Gets version.
        /// </summary>
        /// <response code="200">version build returned successfully.</response>
        /// <response code="500">There was an error on the server.</response>
        [HttpGet("")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(Response<>), 500)]
        public IActionResult GetAsync()
        {
            var assembly = Assembly.GetEntryAssembly();
            return Ok(assembly.GetName().Version);
        }

        /// <summary>
        /// Gets version.
        /// </summary>
        /// <response code="200">version build returned successfully.</response>
        /// <response code="500">There was an error on the server.</response>
        [HttpGet("")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Test), 200)]
        [ProducesResponseType(typeof(Test), 500)]
        public IActionResult Get1Async()
        {
            var assembly = Assembly.GetEntryAssembly();
            return Ok(assembly.GetName().Version);
        }
    }


    public class Test
    {
        public Test()
        {

        }
        public string aaa { get; set; }
        public string bbb;
    }

}