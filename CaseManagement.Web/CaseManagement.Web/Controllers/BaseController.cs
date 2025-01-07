using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IConfiguration _configuration;

        public BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected string GetApiBaseUrl()
        {
            return _configuration["ApiSettings:BaseUrl"];
        }
    }
}
