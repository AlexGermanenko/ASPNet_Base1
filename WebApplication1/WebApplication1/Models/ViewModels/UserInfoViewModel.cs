using System.Collections.Generic;
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
