using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetBase.Models.Data.Interfaces
{
    public interface IUser
    {
        List<UserModel> Users { get; }

        List<UserModel> UsersWithRate { get; }

        void AddUser(UserModel User);

        void Modify(UserModel User);

        UserModel GetUserByEmail(string email);

        bool EmailIsExist(string email);
    }
}
