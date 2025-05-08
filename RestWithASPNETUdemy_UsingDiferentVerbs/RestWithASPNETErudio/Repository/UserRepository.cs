using System.Security.Cryptography;
using System.Text;
using RestWithASPNETErudio.Data.VO;
using RestWithASPNETErudio.Model;
using RestWithASPNETErudio.Model.Context;

namespace RestWithASPNETErudio.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }
        public User? RefreshUserInfo(User user)
        {
            if (_context.Users.Any(u => u.Id.Equals(user.Id))) return null;

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        public object ComputeHash(string input, HashAlgorithm algorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            var builder = new StringBuilder();

            foreach (var item in hashedBytes)
            {
                builder.Append(item.ToString("x2"));
            }
            return builder.ToString();
        }

        User? IUserRepository.ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, SHA256.Create());
            return _context.Users.SingleOrDefault(u => (u.UserName == user.UserName && u.Password == pass.ToString()));
        }

        public User? ValidateCredentials(string userName)
        {
            return _context.Users.SingleOrDefault(u => (u.UserName == userName));
        }

        public bool RevokeToken(string username)
        {
            var user = _context.Users.SingleOrDefault(u => (u.UserName == username));
            if (user is null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }
    }
}
