using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapiCodefirst.Models
{
    /// <summary>
    /// 用户角色关系
    /// </summary>
    public class SysUserRole
    {
        public int ID { get; set; }
        /// <summary>
        /// 用户外键
        /// </summary>
        public int SysUserID { get; set; }
        /// <summary>
        /// 角色外键
        /// </summary>
        public int SysRoleID { get; set; }


    }
}