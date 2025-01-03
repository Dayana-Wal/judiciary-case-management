
using CaseManagement.Business.Common;
using Microsoft.AspNetCore.Mvc;
using CaseManagement.Business.Features.Admin;
using FluentValidation.Results;
using CaseManagement.Business.Service;
using CaseManagement.DataAccess.DTO;

namespace CaseManagement.API.Controllers
{
    public class AdminController : BaseController
    {
        private readonly AdminManager _adminManager;
        public AdminController(AdminManager adminManager)
        {
            _adminManager = adminManager;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] UsersQuery usersQuery)
        {
            ValidationResult validationResult = usersQuery.ValidateCommand();
            if (!validationResult.IsValid)
            {
                var validationErrors = new List<string>();
                foreach (var errors in validationResult.Errors)
                {
                    validationErrors.Add(errors.ErrorMessage);
                }
                var returnResponse = OperationResult<List<string>>.ValidationError(data: validationErrors);
                return ToResponse(returnResponse);
            }

            OperationResult<PagedList<UsersDto>> response = await _adminManager.GetUsers(usersQuery);
            return ToResponse(response);
            //return ToResponse(OperationResult.Success());
        }
    }
}
