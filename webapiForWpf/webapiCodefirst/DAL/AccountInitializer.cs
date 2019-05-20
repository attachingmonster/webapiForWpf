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
                new SysUser {ID=1, UserAccount="Tom",UserPassword="1"},
                new SysUser {ID=2, UserAccount ="Jerry",UserPassword ="2"}
            };
            sysUsers.ForEach(s => context.SysUsers.Add(s));
            context.SaveChanges();
            var sysRole = new List<SysRole>
            {
                new SysRole {RoleName ="administrators",RoleDec ="administrtors have full authorization to perform systea administration."},
               new SysRole {RoleName ="general uners",RoleDec ="general users an access the shared data they are suthorized for."}
            };
            var sysUserRole = new List<SysUserRole>
            {
                new SysUserRole {ID=1,SysUserID=1,SysRoleID=1},
               new SysUserRole {ID=2,SysUserID=2,SysRoleID=2}
            };
            sysRole.ForEach(s => context.SysRoles.Add(s));
            context.SaveChanges();
        }
    }
}