using System;
using System.Collections.Generic;
using System.Text;
using ZhiDaCore.EFCore;
using ZhiDaCore.Entity.Identity;
using ZhiDaCore.IService;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZhiDaCore.Common;

namespace ZhiDaCore.Service
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> GetUser(string AccountNo, string Password)
        {
            var user = from t in _context.User.AsNoTracking().Where(x=>x.Account==AccountNo&&x.Password==Password) select t;

            return await user.AnyAsync();

            
        }

        public async Task<User> AddUser(User user)
        {
            user.CreateTime = DateTime.Now;
            user.ModityTime = DateTime.Now;
           await _context.AddAsync(user);
           await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUser(int id)
        {
            var user =await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();

        }

        public async Task<User> UpdateUser(User user)
        {
            _context.User.Update(user);
           await _context.SaveChangesAsync();
            return user;
        }

        public async Task<PaginatedList<User>> GetTrades(User model, int? page)
        {
            var user = from t in _context.User.AsNoTracking() select t;
            int pageSize = 14;
            return await PaginatedList<User>.CreateAsync(user, page ?? 1, pageSize);
        }
        public async Task<User> GetUserById(int id)
        {
            var user = await _context.User.AsNoTracking().Where(x=>x.ID==id).FirstOrDefaultAsync();
            return  user;
        }
        #region 登录使用的加密解密方法
        /// <summary>
        /// 登录时使用的加密方法
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public string LoginEncrypt(string encryptString, string encryptKey)
        {
            return SecurityHelper.EncryptDES(encryptString, encryptKey);
        }
        /// <summary>
        /// 登录时使用的加密方法
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public string LoginDecrypt(string decryptString, string encryptKey)
        {
            return SecurityHelper.DecryptDES(decryptString, encryptKey);
        }

      
        #endregion
    }
}
