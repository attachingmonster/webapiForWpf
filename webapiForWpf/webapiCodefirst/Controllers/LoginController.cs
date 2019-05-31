using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using webapiCodefirst.DAL;
using webapiCodefirst.Methods;
using webapiCodefirst.ViewModels;

namespace webapiCodefirst.Controllers
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    public class LoginController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostLogin")]
        public ViewModelInformation Login(ViewModelLogin viewModelLogin)
        {
            ViewModelInformation viewModelInformation = null;
            try
            {
                viewModelInformation = new ViewModelInformation();
                var sysUser = unitOfWork.SysUserRepository.Get().Where(s => s.UserAccount.Equals(viewModelLogin.Account)).FirstOrDefault();//查找是否存在账号
                if (sysUser != null)
                {
                    var sysUsers = unitOfWork.SysUserRepository.Get().Where(s => s.UserAccount.Equals(viewModelLogin.Account) && (s.UserPassword.Equals(viewModelLogin.Password)) || (s.RememberPassword.Equals("1") && (CreateMD5.EncryptWithMD5(CreateMD5.EncryptWithMD5(s.UserPassword)).Equals(viewModelLogin.Password)))).FirstOrDefault();
                    if (sysUsers != null)
                    {                       
                        if(viewModelLogin.RememberPassword=="1")
                        {
                            sysUsers.RememberPassword = "1";
                            unitOfWork.Save();
                        }
                        else
                        {
                            sysUsers.RememberPassword = "0";
                            unitOfWork.Save();
                        }
                        throw new Exception("登录成功");

                    }
                    else
                    {
                        throw new Exception("密码错误！");
                    }
                }
                else
                {
                    throw new Exception("账号不存在！");
                }
            }
            catch (Exception ex)
            {
                viewModelInformation.Message = ex.Message;
                return viewModelInformation;
            }
           
        }
    }
}