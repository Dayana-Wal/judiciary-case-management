using CaseManagement.Business.Common;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult ToResponse(OperationResult opresult)
        {
            if (opresult.Status == OperationStatus.Success) { return Ok(opresult); }
            else { return BadRequest(opresult); }
        }

        public IActionResult ToResponse<T>(OperationResult<T> opresult)
        {
            if (opresult.Status == OperationStatus.Success) { return Ok(opresult); }
            else { return BadRequest(opresult); }
        }
    }
}
