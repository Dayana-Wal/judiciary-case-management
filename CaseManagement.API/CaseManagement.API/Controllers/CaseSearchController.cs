using CaseManagement.Business.Queries;
using CaseManagement.Web.Models;
using CaseManagement.Business.Common;
using Microsoft.AspNetCore.Mvc;
using CaseManagement.DataAccess.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CaseManagement.Business.Service;
using CaseManagement.Business;

namespace CaseManagement.API.Controllers
{
    public class CaseSearchController : BaseController
    {
        private readonly CaseSearchManager _caseSearchManager;

        public CaseSearchController(CaseSearchManager caseSearchManager)
        {
            _caseSearchManager = caseSearchManager;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCases([FromQuery] CaseSearchQuery searchQuery)
        {
            if (searchQuery == null)
            {
                return BadRequest("Invalid search query.");
            }

            // Validate the search model
            var validationResult = searchQuery.Validate();

            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                var errorResponse = OperationResult<List<string>>.ValidationError(data: validationErrors);
                return ToResponse(errorResponse);
            }

            var searchResult = await _caseSearchManager.SearchCasesAsync(new CaseSearchQuery
            {
                SearchCategory = searchQuery.SearchCategory,
                SearchValue = searchQuery.SearchValue
            });

            if (searchResult == null || searchResult.Count == 0)
            {
                var notFoundResponse = OperationResult<List<Case>>.Failed(data: null, message: "No cases found matching the search criteria.");
                return ToResponse(notFoundResponse);
            }

            var successResponse = OperationResult<List<Case>>.Success(data: searchResult, message: "Cases retrieved successfully.");
            return ToResponse(successResponse);
        }

        [HttpGet("open-cases")]
        public async Task<IActionResult> GetOpenCases()
        {
            // Retrieve open cases from the CaseManager
            var openCases = await _caseSearchManager.GetOpenCasesAsync();

            if (openCases == null)
            {
                var notFoundResponse = OperationResult<List<Case>>.Failed(data: null, message: "No open cases found.");
                return ToResponse(notFoundResponse);
            }

            var successResponse = OperationResult<List<Case>>.Success(data: openCases, message: "Open cases retrieved successfully.");
            return ToResponse(successResponse);
        }
    }
}
