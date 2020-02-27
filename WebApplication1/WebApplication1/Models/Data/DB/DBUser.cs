using System.Collections.Generic;
using System.Linq;
using AspNetBase.Models.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AspNetBase.Models.Data.DB
{
    public class DBUser : IUser
    {
        private AppDBContext _context;

        public DBUser(AppDBContext context) 
        {
            _context = context;
        }

        public List<UserModel> Users => _context.User.ToList();

        public List<UserModel> UsersWithRate => _context.User.Include(u => u.RatesUsers).ToList();

        public UserModel GetUserByEmail(string email)
            => _context.User.Include(u => u.RatesUsers).FirstOrDefault<UserModel>(u => u.Email == email);

        public void AddUser(UserModel User)
        {
            _context.Add(User);

            _context.SaveChanges();
        }

        public void Modify(UserModel User)
        {
            _context.Update(User);

            _context.SaveChanges();
        }

        public bool EmailIsExist(string email) => Users.Any(u => u.Email == email);

        
    }
}
