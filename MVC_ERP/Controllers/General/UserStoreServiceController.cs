using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MVC_ERP.Models;

namespace MVC_ERP
{
    public class UserStoreService : IUserStore<Priv_Users>,
    IUserPasswordStore<Priv_Users>
    {
        MSDBContext _context;
        public UserStoreService(MSDBContext context)
        {
            _context = context;
        }

        public Task CreateAsync(Priv_Users user)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(Priv_Users user)
        {
            throw new NotImplementedException();
        }
        public Task<Priv_Users> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
        public Task<Priv_Users> FindByNameAsync(string userName)
        {
            Task<Priv_Users> task =
            _context.Priv_Users.Where(apu => apu.UserName == userName)
            .FirstOrDefaultAsync();

            return task;
        }
        public Task UpdateAsync(Priv_Users user)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public Task<string> GetPasswordHashAsync(Priv_Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.Password);
        }
        public Task<bool> HasPasswordAsync(Priv_Users user)
        {
            return Task.FromResult(user.Password != null);
        }
        public Task SetPasswordHashAsync(
          Priv_Users user, string passwordHash)
        {
            throw new NotImplementedException();
        }
    }

    public class MyPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword(
          string hashedPassword, string providedPassword)
        {
            if (hashedPassword == HashPassword(providedPassword))
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
    }
}