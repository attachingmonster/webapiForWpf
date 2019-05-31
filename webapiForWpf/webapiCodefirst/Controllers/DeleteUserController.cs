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
    public class DeleteUserController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostDeleteUser")]
        public ViewModelInformation DeleteUser(ViewModelChangeRole viewModelChangeRole)
        {
            ViewModelInformation viewModelInformation = null;
            try
            {
                viewModelInformation = new ViewModelInformation();
                var sysUser = unitOfWork.SysUserRepository.Get().Where(s => s.UserAccount.Equals(viewModelChangeRole.Account)).FirstOrDefault();//找到选中用户在SysUser表里的实例
                var sysUserRole = unitOfWork.SysUserRoleRepository.Get().Where(s => s.SysUserID == sysUser.ID).FirstOrDefault();//找到选中用户ID在SysUserRole表里的实例
                unitOfWork.SysUserRepository.Delete(sysUser);//删除数据库中SysUser表相应的值
                unitOfWork.Save();//保存数据库
                unitOfWork.SysUserRoleRepository.Delete(sysUserRole);//删除数据库中SysUserRole表相应的值
                unitOfWork.Save();//保存数据库   
                throw new Exception("删除成功");
            }
            catch (Exception ex)
            {
                viewModelInformation.Message = ex.Message;
                return viewModelInformation;
            }

        }
    }
}
