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
            if (opresult.Status == "Success") { return Ok(opresult); }
            else { return BadRequest(opresult); }
        }
        public IActionResult ToResponse(OperationResult<List<string>> opresult)
        {
            if (opresult.Status == "Success") { return Ok(opresult); }
            else { return BadRequest(opresult); }
        }
    }
}
