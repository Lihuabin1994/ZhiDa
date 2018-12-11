using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZhiDaCore.Common;
using ZhiDaCore.Entity.Identity;

namespace ZhiDaCore.IService
{
   public interface IUserService
    {
        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="AccountNo"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Task<bool> GetUser(string AccountNo,string Password);
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> AddUser(User user);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteUser(int id);
        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> UpdateUser(User user);
        /// <summary>
        /// 获取用户分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PaginatedList<User>> GetTrades(User model, int? page);
        Task<User> GetUserById(int id);
        /// <summary>
        /// 登录时使用的加密方法
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        string LoginEncrypt(string encryptString, string encryptKey);
        /// <summary>
        /// 登录时使用的加密方法
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        string LoginDecrypt(string decryptString, string encryptKey);
    }
}
