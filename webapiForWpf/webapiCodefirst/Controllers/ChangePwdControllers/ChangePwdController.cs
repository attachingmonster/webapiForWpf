using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapiCodefirst.DAL;
using webapiCodefirst.ViewModels;

namespace webapiCodefirst.Controllers.ChangePwdControllers
{
    public class ChangePwdController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostChangePwd")]
        public ViewModelInformation ChangePwd2(ViewModelChangePwd viewModelChangePwd)
        {
            ViewModelInformation viewModelInformation = new ViewModelInformation();
            try
            {
                var user = unitOfWork.SysUserRepository.Get().Where(s => s.UserAccount.Equals(viewModelChangePwd.Account)).FirstOrDefault();
                user.UserPassword = viewModelChangePwd.NewPassword;
                unitOfWork.SysUserRepository.Update(user);//更改密码
                unitOfWork.Save();
                viewModelInformation.Message = "修改密码成功！";
                return viewModelInformation;
            }
            catch (Exception ex)
            {
                viewModelInformation.Message = ex.Message;
                return viewModelInformation;
            }
        }
    }
}
