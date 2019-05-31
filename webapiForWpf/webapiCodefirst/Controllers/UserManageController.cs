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
    public class UserManageController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpGet]
        [ActionName("GetUserManageInformation")]
        public List<ViewModelUserManage> UserManage()
        {
            var UserManageInformation = (from us in unitOfWork.SysUserRepository.Get()
                                         join ur in unitOfWork.SysUserRoleRepository.Get() on us.ID equals ur.SysUserID
                                         join r in unitOfWork.SysRoleRepository.Get() on ur.SysRoleID equals r.ID

                                         select new ViewModelUserManage { ViewUserAccount = us.UserAccount, ViewRoleName = r.RoleName, ViewRoleDec = r.RoleDec }).ToList();        
            return UserManageInformation;
        }
       
    


       






    }

}
