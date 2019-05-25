using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapiCodefirst.DAL;
using webapiCodefirst.ViewModels;

namespace webapiCodefirst.Controllers
{
    public class ChangePwdModelController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostChangePwdModel")]
        public ViewModelInformation ChangePwd1(ViewModelChangePwd viewModelChangePwd)
        {
            ViewModelInformation viewModelInformation = new ViewModelInformation();
            try
            {
                var user = unitOfWork.SysUserRepository.Get().Where(s => s.UserAccount.Equals(viewModelChangePwd.Account)).FirstOrDefault();
                if (!(user == null))
                {
                    if (user.UserPassword.Equals(viewModelChangePwd.OldPassword))
                    {
                        viewModelInformation.Message = "判断正确";
                        return viewModelInformation;
                    }
                    else
                    {
                        throw new Exception("原密码错误！");
                    }
                }
                else 
                {
                    throw new Exception("用户账号不存在！");
                }
            } catch (Exception ex)
            {
                viewModelInformation.Message = ex.Message;
                return viewModelInformation;
            }
        }
       
    }
}
