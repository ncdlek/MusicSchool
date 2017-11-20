using MS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.BLL.Repository.Entity
{
    public class UserRepository : BaseRepository<User>
    {
        public bool CheckCredentials(string email, string password)
        {
            return table.Any(x => x.Email == email && x.Password == password);
        }

        public User FindByEmail(string email)
        {
            return table.FirstOrDefault(x => x.Email == email);
        }

        public bool ExistingUser(string email)
        {
            return table.Any(x => x.Email == email);
        }
    }
}
