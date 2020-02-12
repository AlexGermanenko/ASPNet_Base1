using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.Data.DB;
using WebApplication1.Models.Data.Interfaces;

namespace WebApplication1.Models.ViewModels
{
    public class UserInfoViewModel
    {
        public UserModel AuthorizedUser { get; }
        public List<UserModel> Users { get; }

        public UserInfoViewModel(IUser context, string eMail)
        {
            Users = context.Users;
            AuthorizedUser = context.GetUserByEmail(eMail);
        }
    }
}
