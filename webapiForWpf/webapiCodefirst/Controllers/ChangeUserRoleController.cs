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
    public class ChangeUserRoleController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostChangeUserRole")]
        public ViewModelInformation ChangeUserRole(ViewModelChangeRole viewModelChangeRole)
        {
            ViewModelInformation viewModelInformation = null;
            try
            {
                viewModelInformation = new ViewModelInformation();
                var sysUser = unitOfWork.SysUserRepository.Get().Where(s => s.UserAccount== viewModelChangeRole.Account).FirstOrDefault();//获取在wpf的listview选中的那一行数据在sysUser的实例对象
                var sysUserRole = unitOfWork.SysUserRoleRepository.Get().Where(s => s.ID == sysUser.ID).FirstOrDefault();//获取在wpf的listview选中的那一行数据在SysUserRole的实例对象
                if (viewModelChangeRole.ChangeRoleInformation=="教师")
                {
                    var sysRoles = unitOfWork.SysRoleRepository.Get().Where(s => s.RoleName.Equals("教师")).FirstOrDefault();//更改sysUserRole表的对应关系
                    sysUserRole.SysRoleID = sysRoles.ID;
                    unitOfWork.Save();
                }
                else if (viewModelChangeRole.ChangeRoleInformation=="学生")
                {           
                    var sysRoles = unitOfWork.SysRoleRepository.Get().Where(s => s.RoleName.Equals("学生")).FirstOrDefault();//更改sysUserRole表的对应关系
                    sysUserRole.SysRoleID = sysRoles.ID;
                    unitOfWork.Save();
                }
                else
                {                   
                    var sysRoles = unitOfWork.SysRoleRepository.Get().Where(s => s.RoleName.Equals("admin")).FirstOrDefault();//更改sysUserRole表的对应关系
                    sysUserRole.SysRoleID = sysRoles.ID;
                    unitOfWork.Save();
                }
                throw new Exception("修改成功");
            }
            catch (Exception ex)
            {
                viewModelInformation.Message = ex.Message;
                return viewModelInformation;
            }

        }

    }
}
