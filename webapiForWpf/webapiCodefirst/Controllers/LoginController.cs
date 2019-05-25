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
        public VMregisterInfomation Login(ViewModelLogin viewModelLogin)
        {
            VMregisterInfomation vMregisterInfomation = null;
            try
            {
                vMregisterInfomation = new VMregisterInfomation();
                if (viewModelLogin.Account != "" && viewModelLogin.Password != "")//判断账号或者密码是否为空
                {
                    var sysUser = unitOfWork.SysUserRepository.Get().Where(s => s.UserAccount.Equals(viewModelLogin.Account)).FirstOrDefault();//查找是否存在账号
                    if (sysUser != null)
                    {
                        var sysUsers = unitOfWork.SysUserRepository.Get().Where(s => s.UserPassword.Equals(CreateMD5.EncryptWithMD5(viewModelLogin.Password)));//判断密码是否错误
                        if (sysUsers != null)
                        {
                            throw new Exception("登录成功！");
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
                else
                {
                    throw new Exception("账号或者密码不能为空！");
                }
            }
            catch (Exception ex)
            {
                vMregisterInfomation.Message = ex.Message;
                return vMregisterInfomation;
            }
           
        }
    }
}