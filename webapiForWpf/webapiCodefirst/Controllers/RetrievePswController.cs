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
    /// <summary>
    /// 重置密码
    /// </summary>
    public class RetrievePswController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostRetrievePsw")]
        public ViewModelInformation RetrievePsw(ViewModelRetrievePsw viewModelRetrievePsw)
        {
            ViewModelInformation viewModelInformation = null;
            try
            {
                viewModelInformation = new ViewModelInformation();
                var u = unitOfWork.SysUserRepository.Get().Where(s => s.UserAccount.Equals(viewModelRetrievePsw.Account)).FirstOrDefault();  //查找是否存在账号
                if (u != null)
                {
                    //把问题与答案一起加密 放在  QuestionOrAnswer 字段里               
                        //这些信息都比较敏感，所以加密后再 发到 webapi  进行匹配
                        if (u.UserAnswer.Equals(viewModelRetrievePsw.QuestionOrAnswer))
                        {                            
                                //密码怎么样，由UI去判断，减少服务端的 载荷压力，包括下面的 密码一致，2次密码是否一致，都在 UI 里决解，暂时只有账号判断在 服务端 进行                                                                           
                                if (!u.UserPassword.Equals(viewModelRetrievePsw.NewPassword))
                                {                                  
                                        u.UserPassword =viewModelRetrievePsw.NewPassword;
                                        unitOfWork.SysUserRepository.Update(u);//重置密码
                                        unitOfWork.Save();
                                        throw new Exception("密码重置成功");                                
                                }
                                else
                                {
                                    throw new Exception("新密码不能与原密码一致！");
                                }                                                      
                        }
                        else
                        {
                            throw new Exception("密码拾回问题答案错误");
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
