using CaseManagement.Business.Features.Signup;
using CaseManagement.Business.Common;
using CaseManagement.Business.Utility;
using CaseManagement.DataAccess.Commands;
using CaseManagement.DataAccess.Entities;

namespace CaseManagement.Business.Services
{
    public class SignupManager:BaseManager
    {
        private readonly IPersonCommandHandler _dataHandler;

        public SignupManager(IPersonCommandHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public async Task<OperationResult<List<string>>> RegisterUser(SignupCommand command)
        {

            HashHelper passwordService = new HashHelper();

            PasswordSaltHashResult storedResult = passwordService.HashedResult(command.Password);

            string personId = NewUlid();
            
            var person = new Person
            {
                Id = personId,
                Name = command.Name,

                Email = command.Email,
                Contact = Convert.ToInt64(command.Contact),
                DateOfBirth = Convert.ToDateTime(command.DateOfBirth),
                Gender = command.Gender
            };

            var user = new User
            {
                Id = NewUlid(),
                UserName = command.UserName,
                PasswordHash = storedResult.HashedPassword,
                PasswordSalt = storedResult.Salt,
                RoleId = 22,
                PersonId = personId

            };


            //OperationResultT<List<string>> dataStoredresult = new OperationResultT<List<string>>();
            var dataStoredResult = new OperationResult<List<string>>();

            var addPersonAndUserResult = await _dataHandler.CreateUserAsync(person, user);

            if (addPersonAndUserResult.Status == "Success")
            {
                //dataStoredresult.Status = addPersonAndUserResult.Status; //for success
                //dataStoredresult.Message = "Details stored successfully!";

                dataStoredResult = OperationResult<List<string>>.Success(data: [], message: "Details stored successfully!");
            }
            else
            {
                //dataStoredresult.Status = addPersonAndUserResult.Status; //if failed
                //dataStoredresult.Message = addPersonAndUserResult.Message;

                List<string> tempErrors = new List<string>() { addPersonAndUserResult.Data};

                //dataStoredresult.Data = tempErrors;

                dataStoredResult = OperationResult<List<string>>.Failed(data: tempErrors, message: addPersonAndUserResult.Message);

            }

            return dataStoredResult;

        }


    }
}
