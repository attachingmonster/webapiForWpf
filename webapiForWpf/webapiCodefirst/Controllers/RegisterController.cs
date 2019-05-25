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
    /// <summary>
    /// 注册控制器
    /// </summary>
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
               if (viewmodelRegister.Account!= "")    //判断用户账号是否为空
               {
                   var u = unitOfWork.SysUserRepository.Get().Where(s => s.UserAccount.Equals(viewmodelRegister.Account)).FirstOrDefault();   //查找是否存在账号，存在返回账号所在对象，否则返回null
                   if (u == null)    //判断账号是否存在
                   {                     
                       if (viewmodelRegister.Password != "")    //判断密码是否为空
                       {                          
                            if (viewmodelRegister.QuestionOrAnswer!= "1" + "2" + "3" + "4" + "5")    //判断拾回密码是否为空
                            {
                                // 下面需要调试 看看 ，逻辑过程是否合理，另外角色根据具体场景，一般是默认缺省的角色，其他的角色在用户管理界面进行重新 赋予高级角色
                                var sysRole = unitOfWork.SysRoleRepository.Get().Where(s => s.RoleName.Equals(viewmodelRegister.RoleName)).FirstOrDefault();    //寻找用户所选择角色在UserRole里的实例，返回对象
                                if (sysRole != null)
                                {
                                    var CurrentUser = new SysUser();
                                    CurrentUser.UserAccount = viewmodelRegister.Account;
                                    CurrentUser.UserPassword = CreateMD5.EncryptWithMD5(viewmodelRegister.Password);
                                    CurrentUser.UserAnswer = CreateMD5.EncryptWithMD5(viewmodelRegister.QuestionOrAnswer);
                                    CurrentUser.RememberPassword = "0";
                                    unitOfWork.SysUserRepository.Insert(CurrentUser);    //增加新SysUser
                                    unitOfWork.Save();

                                    var CurrentUserRole = new SysUserRole();
                                    CurrentUserRole.SysUserID = CurrentUser.ID;
                                    CurrentUserRole.SysRoleID = sysRole.ID;
                                    unitOfWork.SysUserRoleRepository.Insert(CurrentUserRole);    //增加新SysUserRole
                                    unitOfWork.Save();    //对更改进行保存
                                    vMregisterInfomation.Message = "注册成功";
                                    return vMregisterInfomation;

                                }
                            }
                            else
                            {
                                throw new Exception("密码拾回问题答案不能为空！");
                            }                                                   
                       }
                       else
                       {
                           throw new Exception("密码不能为空！");
                       }
                   }
                   else
                   {
                       throw new Exception("用户名已存在！");
                   }
               }
               else
               {
                   throw new Exception("账号不能为空！");
               }


           }
           catch (Exception ex)
           {
                vMregisterInfomation.Message = ex.Message;
                return vMregisterInfomation;
            }

            return vMregisterInfomation;
        }
    }
}
