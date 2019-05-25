using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using webapiCodefirst.Models;

namespace webapiCodefirst.DAL
{
    class AccountInitializer :
        DropCreateDatabaseIfModelChanges<AccountContext>
    {
        protected override void Seed(AccountContext context)
        {
            var sysUsers = new List<SysUser>
            {
                /*
                new SysUser {ID=1, UserAccount="Tom",UserPassword="1"},
                new SysUser {ID=2, UserAccount ="Jerry",UserPassword ="2"}
                */
            };
            sysUsers.ForEach(s => context.SysUsers.Add(s));
            context.SaveChanges();
            var sysRole = new List<SysRole>
            {
                new SysRole {RoleName ="学生",RoleDec ="学生权限"},
               new SysRole {RoleName ="教师",RoleDec ="教师权限"},
               new SysRole {RoleName ="admin",RoleDec ="管理员权限"}
            };
            var sysUserRole = new List<SysUserRole>
            {
                /*
                new SysUserRole {ID=1,SysUserID=1,SysRoleID=1},
               new SysUserRole {ID=2,SysUserID=2,SysRoleID=2}
               */
            };
            sysRole.ForEach(s => context.SysRoles.Add(s));
            context.SaveChanges();
        }
    }
}