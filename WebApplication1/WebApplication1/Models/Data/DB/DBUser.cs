using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Data.Interfaces;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Models.Data.DB
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
    }
}
