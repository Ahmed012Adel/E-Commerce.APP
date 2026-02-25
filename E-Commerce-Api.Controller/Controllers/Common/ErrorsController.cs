using E_Commerce_Api.Controller.Error;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Api.Controller.Controllers.Common
{
    [ApiController]
    [Route("Errors/{Code}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ErrorsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error (int Code)
        {
            if (Code == (int)HttpStatusCode.NotFound)
            {
                var response = new ApiResponse((int) HttpStatusCode.NotFound, $"The requested endpoint: {Request.Path} is not found");
                return NotFound(response);
            }

            return StatusCode(Code, new ApiResponse(Code));
        }
    }
}
