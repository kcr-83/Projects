using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface IUsersRepository
    {
         Task<List<User>> GetUsersList();

        Task<User>  GetUserDetails(int usrId);
    }
}