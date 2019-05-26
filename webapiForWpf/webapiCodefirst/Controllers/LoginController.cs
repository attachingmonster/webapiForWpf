using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapiCodefirst.DAL;
using webapiCodefirst.Methods;
using webapiCodefirst.ViewModels;

namespace webapiCodefirst.Controllers
{
    public class LoginController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostLogin")]
        public ViewModelInformation Login(ViewModelLogin viewModelLogin)
        {
            ViewModelInformation viewModelInformation = new ViewModelInformation();
            try
            {
                if (viewModelLogin.Password == null && viewModelLogin.RememberPassword == null)
                {
                    //comboboxItem判断是否记住密码
                    var sysUser = unitOfWork.SysUserRepository.Get().Where(s => s.UserAccount.Equals(viewModelLogin.Account)).FirstOrDefault();   //查找是否存在账号，存在返回账号所在对象，否则返回null
                    if (sysUser != null)
                    {
                        if (sysUser.RememberPassword.Equals("1"))
                        {
                            //记住密码返回相应的值
                            viewModelInformation.Message = "1";
                            return viewModelInformation;
                        }
                        return viewModelInformation;
                    }
                    else
                    {
                        return viewModelInformation;
                    }
                }
                else if (viewModelLogin.RememberPassword == null)
                {
                    //判断数据库中的(是否存在账号and(输入密码正确or(已记住密码and给定密码正确))逻辑是否存在 ，如果存在则返回对象，否则返回null
                    var sysUser = unitOfWork.SysUserRepository.Get().Where(s => s.UserAccount.Equals(viewModelLogin.Account) && (s.UserPassword.Equals(viewModelLogin.Password)) || (s.RememberPassword.Equals("1") && (CreateMD5.EncryptWithMD5(CreateMD5.EncryptWithMD5(s.UserAccount)).Equals(viewModelLogin.Password)))).FirstOrDefault();
                    if (sysUser != null)
                    {
                        viewModelInformation.Message = "进行登陆";
                        return viewModelInformation;
                    }
                    else
                    {
                        viewModelInformation.Message = "用户账号或密码错误";
                        return viewModelInformation;
                    }
                    
                }
                else
                {
                    var sysUser = unitOfWork.SysUserRepository.Get().Where(s => s.UserAccount.Equals(viewModelLogin.Account)).FirstOrDefault();
                    sysUser.RememberPassword = viewModelLogin.RememberPassword;
                    unitOfWork.Save();
                    //linq 多表查询得到选择的用户的角色，暂时一个账号只有一个角色
                    var UserRole = (from u in unitOfWork.SysUserRepository.Get()
                                    join ur in unitOfWork.SysUserRoleRepository.Get() on u.ID equals ur.SysUserID
                                    join r in unitOfWork.SysRoleRepository.Get() on ur.SysRoleID equals r.ID
                                    where u.UserAccount.Equals(viewModelLogin.Account)
                                    select new { RoleName = r.RoleName })
                                   .FirstOrDefault();
                    viewModelInformation.Message = UserRole.RoleName;
                    return viewModelInformation;
                }
            }
            catch (Exception ex)
            {
                viewModelInformation.Message.Equals(ex.Message);
                return viewModelInformation;
            }
        }
    }
}
