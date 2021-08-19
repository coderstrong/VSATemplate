using MakeSimple.SharedKernel.Infrastructure.Api;
using MakeSimple.SharedKernel.Infrastructure.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Org.VSATemplate.Application.Features.Students;
using Org.VSATemplate.Domain.Dtos.Student;
using System.Threading.Tasks;

namespace Org.VSATemplate.WebApi.Apis.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    public class StudentController : ControllerApiBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets a single Student by ID.
        /// </summary>
        /// <response code="200">Student record returned successfully.</response>
        /// <response code="400">Student has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Student.</response>
        [HttpGet("{studentId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<ClassDto>), 200)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 500)]
        public async Task<IActionResult> GetAsync(long studentId)
        {
            return ResultDTO(await _mediator.Send(new StudentQuery(studentId)));
        }

        /// <summary>
        /// Gets a list of all Students.
        /// </summary>
        /// <response code="200">Student list returned successfully.</response>
        /// <response code="400">Student has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while proccess.</response>
        /// <remarks>
        /// Requests can be narrowed down with a variety of query string values:
        /// ## Query String Parameters
        /// - **PageNumber**: An integer value that designates the page of records that should be returned.
        /// - **PageSize**: An integer value that designates the number of records returned on the given page that you would like to return. This value is capped by the internal MaxPageSize.
        /// - **SortOrder**: A comma delimited ordered list of property names to sort by. Adding a `-` before the name switches to sorting descendingly.
        /// - **Filters**: A comma delimited list of fields to filter by formatted as `{Name}{Operator}{Value}` where
        ///     - {Name} is the name of a filterable property. You can also have multiple names (for OR logic) by enclosing them in brackets and using a pipe delimiter, eg. `(LikeCount|CommentCount)>10` asks if LikeCount or CommentCount is >10
        ///     - {Operator} is one of the Operators below
        ///     - {Value} is the value to use for filtering. You can also have multiple values (for OR logic) by using a pipe delimiter, eg.`Title@= new|hot` will return posts with titles that contain the text "new" or "hot"
        ///
        ///    | Operator | Meaning                       | Operator  | Meaning                                      |
        ///    | -------- | ----------------------------- | --------- | -------------------------------------------- |
        ///    | `==`     | Equals                        |  `!@=`    | Does not Contains                            |
        ///    | `!=`     | Not equals                    |  `!_=`    | Does not Starts with                         |
        ///    | `>`      | Greater than                  |  `@=*`    | Case-insensitive string Contains             |
        ///    | `&lt;`   | Less than                     |  `_=*`    | Case-insensitive string Starts with          |
        ///    | `>=`     | Greater than or equal to      |  `==*`    | Case-insensitive string Equals               |
        ///    | `&lt;=`  | Less than or equal to         |  `!=*`    | Case-insensitive string Not equals           |
        ///    | `@=`     | Contains                      |  `!@=*`   | Case-insensitive string does not Contains    |
        ///    | `_=`     | Starts with                   |  `!_=*`   | Case-insensitive string does not Starts with |
        /// </remarks>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PaginatedList<ClassDto>), 200)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 500)]
        public async Task<IActionResult> Get([FromQuery] StudentListQuery query)
        {
            return ResultDTO(await _mediator.Send(query));
        }

        /// <summary>
        /// Creates a new Student record.
        /// </summary>
        /// <response code="200">Student created.</response>
        /// <response code="400">Student has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Student.</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<ClassDto>), 200)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] StudentForCreationDto request)
        {
            return ResultDTO(await _mediator.Send(new AddStudentCommand(request)));
        }

        /// <summary>
        /// Updates an entire existing Student.
        /// </summary>
        /// <response code="204">Student updated.</response>
        /// <response code="400">Student has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Student.</response>
        [HttpPut("{studentId}")]
        [AllowAnonymous]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Put(long studentId, [FromBody] StudentForUpdateDto request)
        {
            return ResultDTO(await _mediator.Send(new UpdateStudentCommand(studentId, request)));
        }

        /// <summary>
        /// Updates specific properties on an existing Student.
        /// </summary>
        /// <response code="204">Student updated.</response>
        /// <response code="400">Student has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Student.</response>
        [HttpPatch("{studentId}")]
        [AllowAnonymous]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Patch(long studentId, [FromBody] JsonPatchDocument<StudentForUpdateDto> patchDoc)
        {
            return ResultDTO(await _mediator.Send(new PatchStudentCommand(studentId, patchDoc)));
        }

        /// <summary>
        /// Deletes an existing Student record.
        /// </summary>
        /// <response code="204">Student deleted.</response>
        /// <response code="400">Student has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Student.</response>
        [HttpDelete("{studentId}")]
        [AllowAnonymous]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 500)]
        public async Task<IActionResult> Delete(long studentId)
        {
            return ResultDTO(await _mediator.Send(new DeleteStudentCommand(studentId)));
        }
    }
}