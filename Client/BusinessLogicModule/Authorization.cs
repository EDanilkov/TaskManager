using SharedServicesModule;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicModule
{
    static class Authorization
    {
        public static async Task<bool> CheckUser(string login, string password)
        {
            IRepository DBRepository = new DBRepository();
            List<User> Users = await DBRepository.GetUsers();
            Users = Users.Where(c => string.Equals(c.Login, login) && string.Equals(c.Password, password)).ToList();
            if (Users.Count != 0)
                return true;
            else
                return false;
        } 
    }
}
