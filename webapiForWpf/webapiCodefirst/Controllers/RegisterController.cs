using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapiCodefirst.DAL;
using webapiCodefirst.Methods;
using webapiCodefirst.Models;
using webapiCodefirst.ViewModels;


namespace webapiCodefirst.Controllers
{
    public class RegisterController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostRegister")]
        
        public VMregisterInfomation Register(ViewModelRegister viewmodelRegister)
        {

            VMregisterInfomation vMregisterInfomation = null ;
           try
           {
                vMregisterInfomation = new VMregisterInfomation();
                var u = unitOfWork.SysUserRepository.Get().Where(s => s.UserAccount.Equals(viewmodelRegister.Account)).FirstOrDefault();   //查找是否存在账号，存在返回账号所在对象，否则返回null
                if (u == null)    //判断账号是否存在
                {
                    var sysRole = unitOfWork.SysRoleRepository.Get().Where(s => s.RoleName.Equals(viewmodelRegister.RoleName)).FirstOrDefault();    //寻找用户所选择角色在UserRole里的实例，返回对象
                    if (sysRole != null)
                    {
                        var CurrentUser = new SysUser();
                        CurrentUser.UserAccount = viewmodelRegister.Account;
                        CurrentUser.UserPassword = CreateMD5.EncryptWithMD5(viewmodelRegister.Password);
                        CurrentUser.UserAnswer = CreateMD5.EncryptWithMD5(viewmodelRegister.Answer);
                        CurrentUser.RememberPassword = "0";
                        unitOfWork.SysUserRepository.Insert(CurrentUser);    //增加新SysUser
                        unitOfWork.Save();

                        var CurrentUserRole = new SysUserRole();
                        CurrentUserRole.SysUserID = CurrentUser.ID;
                        CurrentUserRole.SysRoleID = sysRole.ID;
                        unitOfWork.SysUserRoleRepository.Insert(CurrentUserRole);    //增加新SysUserRole
                        unitOfWork.Save();    //对更改进行保存
                        vMregisterInfomation.message = "注册成功";
                        return vMregisterInfomation;
                    }
                }
                else
                {
                    throw new Exception("用户名已存在！");
                }
           }
           catch (Exception ex)
           {
                vMregisterInfomation.message = ex.Message;
                return vMregisterInfomation;
            }

            return vMregisterInfomation;
        }
    }
}
