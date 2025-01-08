using CaseManagement.Business.Features.Email;
using CaseManagement.Business.Service;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    public class EmailController : BaseController
    {
        private readonly BgService _bgService;
        public EmailController(BgService bgService) {
            _bgService = bgService;
        }

        [HttpPost("send")]
        public IActionResult SendEmail([FromBody] EmailCommand emailCommand)
        {
            _bgService.QueueEmail(emailCommand.To, emailCommand.Subject, emailCommand.Body);
            return Ok("Email queued successfully.");
        }
    }
}
