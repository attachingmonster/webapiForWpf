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
    public class UserRoleController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostUserRole")]
        public ViewModelInformation UserRole(ViewModelLogin viewModelLogin)
        {
            ViewModelInformation viewModelInformation = null;
            try
            {
                viewModelInformation = new ViewModelInformation();
                //linq多表查询得到账号对应的角色
                var UserRole = (from u in unitOfWork.SysUserRepository.Get()
                                join ur in unitOfWork.SysUserRoleRepository.Get() on u.ID equals ur.SysUserID
                                join r in unitOfWork.SysRoleRepository.Get() on ur.SysRoleID equals r.ID
                                where u.UserAccount.Equals(viewModelLogin.Account)
                                select new { RoleName = r.RoleName }).FirstOrDefault();
                viewModelInformation.Message = UserRole.RoleName;
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
