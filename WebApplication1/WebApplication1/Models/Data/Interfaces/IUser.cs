using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Data.Interfaces
{
    public interface IUser
    {
        List<UserModel> Users { get; }

        List<UserModel> UsersWithRate { get; }

        void AddUser(UserModel User);

        UserModel GetUserByEmail(string email);
    }
}
