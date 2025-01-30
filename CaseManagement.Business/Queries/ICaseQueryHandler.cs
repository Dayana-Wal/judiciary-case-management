using CaseManagement.DataAccess.Entities;

namespace CaseManagement.Business.Queries
{
    public interface ICaseQueryHandler
    {
        Task<int> GetCaseStatusId(string caseStatus);
        Task<int> GetCaseTypeId(string caseType);
        Task<Person> GetPersonAsync(string name, long contact);
        Task<Case?> GetExistingCase(string caseId);
    }
}