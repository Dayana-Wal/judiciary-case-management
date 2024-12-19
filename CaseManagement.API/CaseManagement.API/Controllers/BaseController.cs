using CaseManagement.Business.Common;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult ToResponse<T>(OperationResult<T> opResult) {
            if (opResult.Status == "Success") { return Ok(opResult); }
            else { return BadRequest(opResult); } 
        }

        public IActionResult ToResponse(OperationResult opResult)
        {
            if (opResult.Status == "Success") { return Ok(opResult); }
            else { return BadRequest(opResult); }
        }
    }
}
