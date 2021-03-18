using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;

namespace FileDownloader.Controllers
{  
    //[ApiController] // Can be commented in to see how this affects the responses.
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {

        private readonly ILogger<FileController> _logger;

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        [HttpGet("iactionresult")]
        [Produces("application/octet-stream", "application/json", "text/plain", "text/json")]
        [SwaggerOperation(
            Summary = "Method which demonstrates how to return mixed content types with an `IActionResult` return type",
            Description = "`Swashbuckle.AspNetCore.Annotations.SwaggerResponse` attributes are used to document the possible return codes. We see that we are unable " +
                          "to specify the `content-type` by HTTP status code. This is also not possible with the `Microsoft.AspNetCore.Mvc.ProducesResponseType` attribute")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Returns a file or json")]
        [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(object), Description = "Resource not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails), Description = "Internal error")]
        public IActionResult GetFile([FromQuery] ReturnType returnType) => returnType switch
        {
            ReturnType.File => new FileContentResult(new byte[] { 0x48, 0x45, 0x4C, 0x4C, 0x4F }, System.Net.Mime.MediaTypeNames.Application.Octet),// Plain text file containing "HELLO" return as octet stream
            ReturnType.NotFound => NotFound(),
            ReturnType.Json => new JsonResult(new { Name = "John", LastName = "Doe" }),
            ReturnType.ProblemDetails => Problem(detail: "Unknown error", statusCode: (int)HttpStatusCode.InternalServerError),
            ReturnType.PlainText => Content("Plain Text response"),
            _ => throw new NotSupportedException(),
        };
    }
}
